using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodOfDuty.gameObject {
    class HealthItem : GameObject {
        Texture2D texture;
        int health =1;

        public HealthItem(Texture2D texture) : base(texture) {
            this.texture = texture;

        }
        public void Action(GameObject obj1, GameObject obj2) {
           
                if (obj1.getRect.Intersects(obj2.getRect)) {
                    obj2.Health += health;
                    obj1.IsActive = false;
              
                   Console.WriteLine(obj2.Name + " "+ obj2.Health );
                   
                }
               
                
            
        }

       

        public override void Draw(SpriteBatch spriteBatch) {
            if(this.IsActive)
            spriteBatch.Draw(texture, this.Position, Color.White);
        }

        public override void Reset() {
            
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects) {
            foreach (GameObject g in gameObjects) {
                if (g.Name.Equals("Player1") || g.Name.Equals("Player2")) {
                    Action(this, g);
                }
            }
        }
    }
}
