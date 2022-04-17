using SnakeWPF.Common;
using SnakeWPF.Model;
using System.Windows.Input;

namespace SnakeWPF.ViewModel
{
    public class SnakeViewModel : NotificationBase
    {

        #region Fields
        #endregion

        #region Constructors
        public SnakeViewModel()
        {
            SnakeGameLogic = new SnakeGame();

            UpKeyPressedCommand = new DelegateCommand(OnUpKeyPressed);
            RightKeyPressedCommand = new DelegateCommand(OnRightKeyPressed);
            DownKeyPressedCommand = new DelegateCommand(OnDownKeyPressed);
            LeftKeyPressedCommand = new DelegateCommand(OnLeftKeyPressed);
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        public SnakeGame SnakeGameLogic { get; }
        public ICommand UpKeyPressedCommand
        {
            get;
            private set;
        }
        public ICommand RightKeyPressedCommand
        {
            get;
            private set;
        }

        public ICommand DownKeyPressedCommand
        {
            get;
            private set;
        }
        public ICommand LeftKeyPressedCommand
        {
            get;
            private set;
        }

        #endregion

        #region Methods
        /// <param name="arg"></param>
        private void OnUpKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Up);
            }
        }
        /// <param name="arg"></param>
        private void OnRightKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Right);
            }
        }
        /// <param name="arg"></param>
        private void OnDownKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Down);
            }
        }
        /// <param name="arg"></param>
        private void OnLeftKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Left);
            }
        }

        #endregion
    }
}
