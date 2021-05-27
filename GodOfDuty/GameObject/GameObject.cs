using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodOfDuty.gameObject {
   public class GameObject {
       
            Texture2D _texture;
            public Vector2 position;
            public Color color;
            public float Rotation;
            public Vector2 Scale;
            public float Velocity;
            public string Name;
            public bool IsActive;
            Rectangle hitBox;
            private int health;
            private int walkSlot;
            private int power;


        public GameObject(Texture2D texture) {
                _texture = texture;
                position = Vector2.Zero;
                Scale = Vector2.One;
                Rotation = 0f;
                IsActive = true;
                hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width - 30, texture.Height - 30);
            }

            public virtual void Update(GameTime gameTime, List<GameObject> gameObjects) {

            }

            public virtual void Draw(SpriteBatch spriteBatch) {

            }

            public virtual void Reset() {

            }

           public virtual void Action() {

            }

        public Vector2 Position {
            get { return position; }
            set { position = value; hitBoxPosition(position); }
        }

        public void hitBoxPosition(Vector2 position) {
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }

        public Rectangle getRect {
            get { return hitBox; }
            set { hitBox = value; }
        }
        public int Health {
            get { return health; }
            set { health = value; }
        }
        public int WalkSlot {
            get { return walkSlot; }
            set { walkSlot = value; }

        }

        public int Power {
            get { return power; }
            set { power = value; }

        }


    }

}
