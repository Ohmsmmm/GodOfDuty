using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodOfDuty
{
    class Player : GameObject
    {
        public Bullet Bullet;
        public Keys Left;
        public Keys Right;
        public Keys Fire;

        public Player(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            if (Singleton.Instance.CurrentKey.IsKeyDown(Left))
            {
                Velocity.X = -500;
            }
            if (Singleton.Instance.CurrentKey.IsKeyDown(Right))
            {
                Velocity.X = 500;
            }
            if (Singleton.Instance.CurrentKey.IsKeyDown(Fire) &&
                Singleton.Instance.CurrentKey != Singleton.Instance.PreviousKey)
            {
                var newBullet = Bullet.Clone() as Bullet;
                newBullet.Position = new Vector2(Rectangle.Width / 2 + Position.X - newBullet.Rectangle.Width / 2,
                                                Position.Y);
                newBullet.Reset();
                gameObjects.Add(newBullet);
            }

            float newX = Position.X + Velocity.X * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            newX = MathHelper.Clamp(newX, 0, Singleton.SCREENWIDTH - Rectangle.Width);
            float newY = Position.Y + Velocity.Y * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            Position = new Vector2(newX, newY);

            Velocity = Vector2.Zero;
            base.Update(gameTime, gameObjects);
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
            Position = new Vector2(62, 640);
            base.Reset();
        }
    }
}
