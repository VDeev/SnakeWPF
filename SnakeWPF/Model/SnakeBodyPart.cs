using System;
using System.Linq;

namespace SnakeWPF.Model
{
    public class SnakeBodyPart : SnakePart
    {
        #region Fields
        #endregion

        #region Constructors
        public SnakeBodyPart(double gameBoardWidthPixels, double gameBoardHeightPixels, Snake theSnake)
        {
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;

            _width = Constants.BodyWidth;
            _height = Constants.BodyHeight;

            SnakePart currentLastSnakePart;
            try
            {
                currentLastSnakePart = theSnake.TheSnakeBody.Last();
            }
            catch
            {
                currentLastSnakePart = theSnake.TheSnakeHead;
            }
            if (currentLastSnakePart.DirectionOfTravel == Direction.Up)
            {
                _xPosition = currentLastSnakePart.XPosition;
                _yPosition = currentLastSnakePart.YPosition + _height;
                _directionOfTravel = Direction.Up;

                if (CheckLocation(theSnake))
                {
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Right)
            {
                _xPosition = currentLastSnakePart.XPosition - _width;
                _yPosition = currentLastSnakePart.YPosition;
                _directionOfTravel = Direction.Right;

                if (CheckLocation(theSnake))
                {
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Down)
            {
                _xPosition = currentLastSnakePart.XPosition;
                _yPosition = currentLastSnakePart.YPosition - _height;
                _directionOfTravel = Direction.Down;

                if (CheckLocation(theSnake))
                {
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Left)
            {
                _xPosition = currentLastSnakePart.XPosition + _width;
                _yPosition = currentLastSnakePart.YPosition;
                _directionOfTravel = Direction.Left;

                if (CheckLocation(theSnake))
                {
                    return;
                }
            }
            else
            {
                throw new Exception("SnakeBodyPart(double gameBoardWidthPixels, double gameBoardHeightPixels, Snake theSnake): Unable to find valid location to grow snake.");
            }
        }
        /// <param name="snakeHead"></param>
        public SnakeBodyPart(SnakeHead snakeHead)
        {
            _gameBoardWidthPixels = snakeHead.GameBoardWidthPixels;
            _gameBoardHeightPixels = snakeHead.GameBoardHeightPixels;
            _width = snakeHead.Width;
            _height = snakeHead.Height;
            _directionOfTravel = snakeHead.DirectionOfTravel;

            if (snakeHead.DirectionOfTravel == Direction.Right)
            {
                _xPosition = snakeHead.XPosition - _width;
                _yPosition = snakeHead.YPosition;
            }
            else if (snakeHead.DirectionOfTravel == Direction.Left)
            {
                _xPosition = snakeHead.XPosition + _width;
                _yPosition = snakeHead.YPosition;
            }
            else if (snakeHead.DirectionOfTravel == Direction.Down)
            {
                _xPosition = snakeHead.XPosition;
                _yPosition = snakeHead.YPosition - _height;
            }
            else if (snakeHead.DirectionOfTravel == Direction.Up)
            {
                _xPosition = snakeHead.XPosition;
                _yPosition = snakeHead.YPosition + _height;
            }

            UpdatePosition();
        }
        /// <param name="snakePart"></param>
        public SnakeBodyPart(SnakeBodyPart snakeBodyPart)
        {
            _gameBoardWidthPixels = snakeBodyPart.GameBoardWidthPixels;
            _gameBoardHeightPixels = snakeBodyPart.GameBoardHeightPixels;
            _xPosition = snakeBodyPart.XPosition;
            _yPosition = snakeBodyPart.YPosition;
            _width = snakeBodyPart.WidthPixels;
            _height = snakeBodyPart.HeightPixels;
            _directionOfTravel = snakeBodyPart.DirectionOfTravel;
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <param name="theSnake"></param>
        private bool CheckLocation(Snake theSnake)
        {
            if (_xPosition == theSnake.TheSnakeHead.XPosition && _yPosition == theSnake.TheSnakeHead.YPosition)
            {
            }
            else
            {
                return false;
            }

            foreach (SnakeBodyPart bodyPart in theSnake.TheSnakeBody)
            {
                if (_xPosition == bodyPart.XPosition && _yPosition == bodyPart.YPosition)
                {
                }
                else
                {
                    return false;
                }
            }

            if (_xPosition - (_width / 2.0) < 0)
            {
                return false;
            }
            else if (_xPosition + (_width / 2.0) > Constants.GameBoardWidthScale)
            {
                return false;
            }
            else if (_yPosition - (_height / 2.0) < 0)
            {
                return false;
            }
            else if (_yPosition + (_height / 2.0) > Constants.GameBoardHeightScale)
            {
                return false;
            }

            return true;
        }

        

        #endregion
    }
}
