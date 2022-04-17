using System;
using System.Collections.ObjectModel;

namespace SnakeWPF.Model
{
    public class SnakeHead : SnakePart
    {
        #region Fields
        #endregion

        #region Constructors
        public SnakeHead(double gameBoardWidthPixels, double gameBoardHeightPixels, double initialXPosition, double initialYPosition, Direction direction)
            : base(gameBoardWidthPixels, gameBoardHeightPixels, direction)
        {
            _xPosition = initialXPosition;
            _yPosition = initialYPosition;
            _width = Constants.HeadWidth;
            _height = Constants.HeadHeight;
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        public bool HitBoundary()
        {
            if (_xPosition - (_width / 2.0) < 0)
            {
                return true;
            }
            else if (_xPosition + (_width / 2.0) > Constants.GameBoardWidthScale)
            {
                return true;
            }
            else if (_yPosition - (_height / 2.0) < 0)
            {
                return true;
            }
            else if (_yPosition + (_height / 2.0) > Constants.GameBoardHeightScale)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HitSelf(ObservableCollection<SnakeBodyPart> snakeBody)
        {
            foreach (SnakeBodyPart bodyPart in snakeBody)
            {
                if (_xPosition == bodyPart.XPosition && _yPosition == bodyPart.YPosition)
                {
                    return true;
                }
            }

            return false;
        }
        /// <param name="cherry"></param>
        public bool EatCherry(Cherry cherry)
        {
            double xDiff = Math.Abs(_xPosition - cherry.XPosition);
            double yDiff = Math.Abs(_yPosition - cherry.YPosition);

            if (xDiff < _width && yDiff < _height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
