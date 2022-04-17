using SnakeWPF.Common;
using System;
using System.Windows.Threading;

namespace SnakeWPF.Model
{
    public class SnakeGame : NotificationBase
    {
        #region Fields

        private Snake _theSnake;
        private Cherry _theCherry;
        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private DispatcherTimer _gameTimer;
        private int _gameStepMilliSeconds;
        private int _gameLevel;
        private bool _isGameOver;
        private int _restartCountdownSeconds;
        private DispatcherTimer _restartTimer;
        
        #endregion

        #region Constructors
        public SnakeGame()
        {
            GameBoardWidthPixels = Constants.DefaultGameBoardWidthPixels;
            GameBoardHeightPixels = Constants.DefaultGameBoardHeightPixels;
            Snake.OnHitBoundary += new HitBoundary(HitBoundaryEventHandler);
            Snake.OnHitSnake += new HitSnake(HitSnakeEventHandler);
            Snake.OnEatCherry += new EatCherry(EatCherryEventHandler);

            StartNewGame();
        }

        #endregion

        #region Events
        #endregion

        #region Properties

        public double GameBoardWidthPixels
        {
            get
            {
                return (int)_gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();

                TheSnake.GameBoardWidthPixels = value;
            }
        }
        public double GameBoardHeightPixels
        {
            get
            {
                return (int)_gameBoardHeightPixels;
            }
            set
            {
                _gameBoardHeightPixels = value;
                RaisePropertyChanged();

                TheSnake.GameBoardHeightPixels = value;
            }
        }
        public Snake TheSnake
        {
            get
            {
                if (_theSnake == null)
                {
                    _theSnake = new Snake(GameBoardWidthPixels, GameBoardHeightPixels);
                }

                return _theSnake;
            }
            private set
            {
                _theSnake = value;
                RaisePropertyChanged();
            }
        }
        public Cherry TheCherry
        {
            get
            {
                if (_theCherry == null)
                {
                    _theCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);
                }

                return _theCherry;
            }
            private set
            {
                _theCherry = value;
                RaisePropertyChanged();
            }
        }
        public string TitleText
        {
            get
            {
                return "Snake " + _gameLevel + "/" + Constants.EndLevel;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return _isGameOver;
            }
            private set
            {
                _isGameOver = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGameRunning));
            }
        }
        public bool IsGameRunning
        {
            get
            {
                return !IsGameOver;
            }
        }
        public int RestartCountdownSeconds
        {
            get
            {
                return _restartCountdownSeconds;
            }
            private set
            {
                _restartCountdownSeconds = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods
        private void StartNewGame()
        {
            TheSnake = new Snake(_gameBoardWidthPixels, _gameBoardHeightPixels);
            TheCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);
            IsGameOver = false;

            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;

            _gameLevel = Constants.StartLevel;
            RaisePropertyChanged(nameof(TitleText));
            _gameStepMilliSeconds = Constants.DefaultGameStepMilliSeconds;
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            _gameTimer.Tick += new EventHandler(GameTimerEventHandler);
            _gameTimer.Start();
        }
        private void RestartGame()
        {
            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;
            _restartTimer = new DispatcherTimer();
            _restartTimer.Interval = TimeSpan.FromMilliseconds(Constants.RestartStepMilliSeconds);
            _restartTimer.Tick += new EventHandler(RestartTimerEventHandler);
            _restartTimer.Start();
        }

        private void HitBoundaryEventHandler()
        {
            IsGameOver = true;
        }
        private void HitSnakeEventHandler()
        {
            IsGameOver = true;
        }
        private void EatCherryEventHandler()
        {
            TheCherry.MoveCherry(TheSnake);

            _gameLevel++;
            RaisePropertyChanged(nameof(TitleText));
            if (_gameLevel < Constants.EndLevel)
            {
                _gameStepMilliSeconds = _gameStepMilliSeconds - Constants.DecreaseGameStepMilliSeconds;
                _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            }
            else
            {
                IsGameOver = true;
            }
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimerEventHandler(object sender, EventArgs e)
        {
            if (IsGameOver)
            {
                if (_gameTimer.IsEnabled)
                {
                    _gameTimer.Stop();
                    RestartGame();
                }
            }
            else
            {
                TheSnake.UpdateSnakeStatus(TheCherry);
            }
        }
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartTimerEventHandler(object sender, EventArgs e)
        {
            RestartCountdownSeconds--;

            if (RestartCountdownSeconds == 0)
            {
                _restartTimer.Stop();
                StartNewGame();
            }
        }
        /// <param name="direction"></param>
        public void ProcessKeyboardEvent(Direction direction)
        {
            TheSnake.SetSnakeDirection(direction);
        }

        #endregion
    }
}