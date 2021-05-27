using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty.gameObject {
   public class Ball : GameObject {
        Texture2D texture;
        double vi, t = 0; // vi - initial velocity | t - time
        double g = 520; // pixels per second squared | gravitational acceleration
        int keyState = 0;
        int kState = 0;
        double v = -820, vx, vy, alpha, t2 = 0;
        //----------------------------------------------------------------------//



        public Ball(Texture2D texture) : base(texture) {
            this.texture = texture;
           
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects) {
            if (Singleton.Instance.leftChooseShoot) {
                alpha = Singleton.Instance.shootPosRight[Singleton.Instance.rightSideShoot];


                
                vx = v * Math.Cos(alpha);
                vy = v * Math.Sin(alpha);
            }

            else if(Singleton.Instance.rightChooseShoot) {
                alpha = Singleton.Instance.shootPosLeft[Singleton.Instance.leftSideShoot];
                vx = v * Math.Cos(alpha);
                vy = v * Math.Sin(alpha);
            }

            //Projectile Part
            if (Singleton.Instance.kState == 1) {

                if (this.Name.Equals("zeusBall")) {

                    this.Position = new Vector2((float)((vx * -1) * t2) + gameObjects.Single(s => s.Name.Equals("Player1")).Position.X, (float)(vy * t2 + g * t2 * t2 / 2) + (gameObjects.Single(s => s.Name.Equals("Player2")).Position.Y) - texture.Height);

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                }
                Singleton.Instance.ballVisible = true;
            }

            //Right to left side
            if (Singleton.Instance.kState == 2) {
                if (this.Name.Equals("shivaBall")) {
                    this.Position = new Vector2((float)((vx) * t2) + gameObjects.Single(s => s.Name.Equals("Player2")).Position.X, (float)(vy * t2 + g * t2 * t2 / 2) + (gameObjects.Single(s => s.Name.Equals("Player1")).Position.Y) - texture.Height);

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                }
               
                Singleton.Instance.ball2Visible = true;
            }

           
           




        }
        public override void Draw(SpriteBatch spriteBatch) {
            
           
           
                spriteBatch.Draw(texture, this.getRect, Color.White);

            
            
        }

        public override void Reset() {
            base.Reset();
        }

       
    }
}
