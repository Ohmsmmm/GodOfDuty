using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodOfDuty.gameObject{
  public  class Player : GameObject {
        Vector2 position= Vector2.Zero;
        Texture2D texture, heart;
        Rectangle hitBox;
        AnimatedSprite animated;
        public bool ability = false;
        public int turn = 0;
        
       

        public Player(Texture2D texture, Texture2D heart) : base(texture) {
            this.texture = texture;
            this.heart = heart;
            this.animated = new AnimatedSprite(this.texture,3,4);
            

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects) {
            
            //Turn Left To Shoot
            if (Singleton.Instance.isLeftTurn) {
                //Console.WriteLine("LeftTurn");

                //right will move first
                Singleton.Instance.CurrentKey = Keyboard.GetState();

                Singleton.Instance.virtualVisible = true;

                if (!Singleton.Instance.isRightMove) {

                    if ( this.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                        if (Singleton.Instance.rightSideMove > 0 && WalkSlot + (Singleton.Instance.count - 1) >= 0) {
                            Singleton.Instance.rightSideMove--;
                            Singleton.Instance.count--;

                        }

                    }

                   if (this.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) ) {

                        if (Singleton.Instance.rightSideMove < 4 && this.WalkSlot - (Singleton.Instance.count + 1) >= 0) {
                            Singleton.Instance.rightSideMove++;
                            Singleton.Instance.count++;

                        }

                    }

                    if ( this.Name.Equals("Player2") && (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) || Singleton.Instance.timer <= 0)) {
                        //After right move
                        Singleton.Instance.isRightMove = true;
                        Singleton.Instance.timer = 10;
                        if (Singleton.Instance.count < 0) {
                            this.WalkSlot -= Singleton.Instance.count * -1;
                        }
                        else {
                            this.WalkSlot -= Singleton.Instance.count;
                        }
                    }
                }



                
                //Left Side Can Shoot Now
                else if (Singleton.Instance.isRightMove) {
                    
                    Singleton.Instance.virtualVisible = false;
                   
                    if (!Singleton.Instance.leftChooseShoot) {
                        Singleton.Instance.virtualShootVisible = true;


                        if (this.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) ) {
                           
                            if (Singleton.Instance.rightSideShoot > 0) {
                                Singleton.Instance.rightSideShoot--;

                            }

                        }

                        if (this.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) ) {

                            if (Singleton.Instance.rightSideShoot < 4) {
                                Singleton.Instance.rightSideShoot++;
                            }

                        }

                        if (this.Name.Equals("Player1") &&  ((Singleton.Instance.CurrentKey.IsKeyDown(Keys.Up) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) || Singleton.Instance.timer <= 0) ) {
                            //After left move
                            Singleton.Instance.leftChooseShoot = true;
                            Singleton.Instance.virtualShootVisible = false;
                            Singleton.Instance.kState = 1;
                            gameObjects.Single(s => s.Name.Equals("Player2")).Position = new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideMove], 920 - 170);

                        }

                    }
                }
                if (Singleton.Instance.timer <= 0) {
                    Singleton.Instance.isRightMove = true;
                    Singleton.Instance.timer = 10;
                }



            }

          //============ Turn Right To Shoot ================ 
            else if(Singleton.Instance.isRightTurn){

                
                Singleton.Instance.virtualVisible = true;

                //Left will move first
                Singleton.Instance.CurrentKey = Keyboard.GetState();

                if (!Singleton.Instance.isLeftMove) {
                    

                    if (this.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                        Console.WriteLine(this.Name);

                        if (Singleton.Instance.leftSideMove > 0 && this.WalkSlot + (Singleton.Instance.count - 1) >= 0) {
                            

                            Singleton.Instance.leftSideMove--;
                            Singleton.Instance.count--;
                           
                        }


                    }
                    

                    if (this.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                        Console.WriteLine(this.Name);

                        if (Singleton.Instance.leftSideMove < 4 && this.WalkSlot - (Singleton.Instance.count + 1) >= 0) {

                            Singleton.Instance.leftSideMove++;
                            Singleton.Instance.count++;
                        }

                    }

                    if (this.Name.Equals("Player1") && (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) || Singleton.Instance.timer <= 0)) {
                        //After left move
                        Singleton.Instance.isLeftMove = true;
                        Singleton.Instance.timer = 10;

                        if (Singleton.Instance.count < 0) {
                            this.WalkSlot -= Singleton.Instance.count * -1;
                        }
                        else {
                            this.WalkSlot -= Singleton.Instance.count;
                        }
                        Console.WriteLine(this.Name);
                    }

                    
                }


                else if (Singleton.Instance.isLeftMove) {
                    
                    Singleton.Instance.virtualVisible = false;

                    //Right Side Can Shoot Now

                    if (!Singleton.Instance.rightChooseShoot) {
                        
                        Singleton.Instance.virtualShootVisible = true;

                        if (this.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (Singleton.Instance.leftSideShoot > 0) {
                                Singleton.Instance.leftSideShoot--;

                            }

                        }

                        if (this.Name.Equals("Player2") &&  Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                            
                            if (Singleton.Instance.leftSideShoot < 4) {
                                Singleton.Instance.leftSideShoot++;
                            }

                        }

                        if ((this.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Up) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) || Singleton.Instance.timer <= 0) {
                            //After left move                            
                            Singleton.Instance.rightChooseShoot = true;
                            Singleton.Instance.virtualShootVisible = false;
                            Singleton.Instance.kState = 2;
                            gameObjects.Single(s => s.Name.Equals("Player1")).Position = new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideMove], 920 - 170);


                        }

                    }

                }
                if (Singleton.Instance.timer <= 0) {
                    Singleton.Instance.isLeftMove = true;
                    Singleton.Instance.timer = 10;
                }




            }
            animated.Update(gameTime);



        }

        public override void Draw(SpriteBatch spriteBatch) {

            //spriteBatch.Draw(texture, this.Position, Color.White);
            if (this.IsActive)
            {
                animated.Draw(spriteBatch, this.Position, 2);

            }

            //Draw Heart
            if (this.Name.Equals("Player1")) {
                for (int i = 0; i < this.Health; i++) {
                    spriteBatch.Draw(heart, new Vector2(3 + i * heart.Width, 3), Color.White);
                }
               
            }


            else {
                for (int i = 0; i < this.Health; i++) {
                    spriteBatch.Draw(heart, new Vector2((1600 - heart.Width) - (i * heart.Width), 3), Color.Blue * 0.9f);
                }

            }
           
        }

        public override void Reset() {

           
            if (this.Name.Equals("Player1")) {
                //Left Player
               this.Position = new Vector2(Singleton.Instance.leftArea[2], 920 - 170);
            }
            else {
                //Right Player
                this.Position = new Vector2(Singleton.Instance.rightArea[2], 920 - 170);
            }
            
        }







    }
}
