using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Threading;
using SnakeWPF.Common;

namespace SnakeWPF.Model
{
    public class Snake : NotificationBase
    {
        #region Fields

        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private ObservableCollection<SnakeBodyPart> _snakeBody;
        private volatile bool _updatingSnake;
        private static object _itemsLock = new object();

        #endregion

        #region Constructors
        public Snake(double gameBoardWidthPixels, double gameBoardHeightPixels)
        {
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;
            TheSnakeHead = new SnakeHead(gameBoardWidthPixels, gameBoardHeightPixels, Constants.DefaultXposition, Constants.DefaultYposition, Constants.DefaultDirection);
            TheSnakeEye = new SnakeEye(gameBoardWidthPixels, gameBoardHeightPixels, Constants.DefaultXposition, Constants.DefaultYposition, Constants.DefaultDirection);
            _snakeBody = new ObservableCollection<SnakeBodyPart>();
            BindingOperations.EnableCollectionSynchronization(_snakeBody, _itemsLock);
            _updatingSnake = false;
        }

        #endregion
        
        #region Events

        public static event HitBoundary OnHitBoundary;
        public static event HitSnake OnHitSnake;
        public static event EatCherry OnEatCherry;

        #endregion

        #region Properties
        public double GameBoardWidthPixels
        {
            get
            {
                return _gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();

                TheSnakeHead.GameBoardWidthPixels = value;
                TheSnakeEye.GameBoardWidthPixels = value;
                foreach (SnakeBodyPart bodyPart in _snakeBody)
                {
                    bodyPart.GameBoardWidthPixels = value;
                }
            }
        }
        public double GameBoardHeightPixels
        {
            get
            {
                return _gameBoardHeightPixels;
            }
            set
            {
                _gameBoardHeightPixels = value;
                RaisePropertyChanged();
                TheSnakeHead.GameBoardHeightPixels = value;
                TheSnakeEye.GameBoardHeightPixels = value;
                foreach (SnakeBodyPart bodyPart in _snakeBody)
                {
                    bodyPart.GameBoardHeightPixels = value;
                }
            }
        }
        public SnakeHead TheSnakeHead { get; }
        public SnakeEye TheSnakeEye { get; }

        public ObservableCollection<SnakeBodyPart> TheSnakeBody
        {
            get
            {
                if (_snakeBody == null)
                {
                    _snakeBody = new ObservableCollection<SnakeBodyPart>();
                }

                return _snakeBody;
            }
        }

        #endregion

        #region Methods
        public void UpdateSnakeStatus(Cherry theCherry)
        {
            while (_updatingSnake)
            {
                Thread.Sleep(50);
            }

            _updatingSnake = true;

            TheSnakeHead.UpdatePosition(); 
            TheSnakeEye.UpdatePosition(); 
            Direction previousDirection;
            Direction nextDirection = TheSnakeHead.DirectionOfTravel;
            foreach (SnakeBodyPart bodyPart in _snakeBody)
            {
                bodyPart.UpdatePosition();
                previousDirection = bodyPart.DirectionOfTravel;
                bodyPart.DirectionOfTravel = nextDirection;
                nextDirection = previousDirection;
            }
            
            if (TheSnakeHead.HitBoundary())
            {
                OnHitBoundary?.Invoke();
            }

            if (TheSnakeHead.HitSelf(_snakeBody))
            {
                OnHitSnake?.Invoke();
            }

            if (TheSnakeHead.EatCherry(theCherry))
            {
                SnakeBodyPart snakeBodyPart = new SnakeBodyPart(_gameBoardWidthPixels, _gameBoardHeightPixels, this);
                _snakeBody.Add(snakeBodyPart);

                OnEatCherry?.Invoke();
            }

            _updatingSnake = false;
        }
        /// <param name="direction"></param>
        public void SetSnakeDirection(Direction direction)
        {
            while (_updatingSnake)
            {
                Thread.Sleep(50);
            }

            _updatingSnake = true;
            TheSnakeHead.DirectionOfTravel = direction;
            TheSnakeEye.DirectionOfTravel = direction;
            _updatingSnake = false;
        }
        #endregion
    }
}
