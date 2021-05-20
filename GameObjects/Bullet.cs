using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GodOfDuty
{
    class Bullet : GameObject
    {
        public float DistanceMoved;

        public Bullet(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            DistanceMoved += Math.Abs(Velocity.Y * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond);
            Position = Position + Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            foreach (GameObject s in gameObjects)
            {
                if (Name.Equals("BulletPlayer"))
                {
                    if (IsTouching(s) && (s.Name.Equals("Enemy") || s.Name.Equals("BulletEnemy")))
                    {
                        s.IsActive = false;
                        if (s is Enemy)
                        {
                            Singleton.Instance.Score += (s as Enemy).Score;
                            Singleton.Instance.InvaderLeft--;
                        }
                        IsActive = false;
                    }
                }
                else if (Name.Equals("BulletEnemy"))
                {
                    if (IsTouching(s) && (s.Name.Equals("Player")))
                    {
                        s.Reset();
                        IsActive = false;
                        Singleton.Instance.Life--;
                        Singleton.Instance.CurrentGameState = Singleton.GameState.StartNewLife;
                    }
                }
            }

            if (DistanceMoved >= Singleton.SCREENHEIGHT) IsActive = false;

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
            DistanceMoved = 0;
            base.Reset();
        }
    }
}
