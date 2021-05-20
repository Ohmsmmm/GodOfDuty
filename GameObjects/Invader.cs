using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GodOfDuty
{
    class Invader : Enemy
    {
        public Bullet Bullet;
        public enum Direction
        {
            Left,
            Right
        }
        public Direction MovingDirection;
        public float Speed;
        public float MovedDistance;

        public Invader(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> sprites)
        {
            if (MovingDirection == Direction.Left)
            {
                Velocity.X = -Speed;
            }
            else
            {
                Velocity.X = Speed;
            }

            float movingThisLoop = Velocity.X * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            MovedDistance += Math.Abs(movingThisLoop);
            float newX = Position.X + movingThisLoop;

            float newY = Position.Y;

            if (MovedDistance >= Singleton.SCREENWIDTH - Singleton.INVADERHORDEWIDTH)
            {
                if (MovingDirection == Direction.Left) MovingDirection = Direction.Right;
                else MovingDirection = Direction.Left;

                MovedDistance = 0;

                newY += 30;
                Speed *= 1.2f;

                if (newY >= 600) Singleton.Instance.CurrentGameState = Singleton.GameState.GameOver;
            }

            Position = new Vector2(newX, newY);

            if (Singleton.Instance.Random.Next(10000) <= 0)
            {
                var newBullet = Bullet.Clone() as Bullet;
                newBullet.Position = new Vector2(Rectangle.Width / 2 + Position.X - newBullet.Rectangle.Width / 2,
                                                Position.Y);
                newBullet.Reset();
                sprites.Add(newBullet);
            }

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                            Position,
                            Viewport,
                            Color.White);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            Speed = 20;
            MovedDistance = 0;
            MovingDirection = Direction.Right;
            base.Reset();
        }
    }
}
