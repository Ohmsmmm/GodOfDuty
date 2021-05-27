using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GodOfDuty.gameObject;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace GodOfDuty {

    public class MainGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball; Vector2 ball2pos = Vector2.Zero; // The position of the ball in 2D space (X,Y)
        Texture2D ball2; Vector2 ball1pos = Vector2.Zero;
        Texture2D zeus;
        Texture2D shiva;
        Texture2D shivaDead, zeusDead;
        Texture2D select, target;
        Texture2D background, heart;
        GameObject player1, player2,item1,item2;
        Vector2 coor, virtualPos;
        Texture2D rect, virtualBox, virtualShoot;
        Random sideRand,itemRand;
        int side;
        KeyboardState kBState;
        SpriteFont gameFont;
        
        
        GameObject zeusBall, shivaBall;
        List<GameObject> gameObjects;
        float[] leftAngle,rightAngle;
        bool _isDecreaseHealth, canWalk;
        
        

        

        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            // Set the window height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize() {
            Window.Title = "God Of Catapult";
            gameObjects = new List<GameObject>();
            Singleton.Instance.timer = 0;
            Singleton.Instance.leftSideMove = 2;
            Singleton.Instance.rightSideMove = 2;
            sideRand = new Random();
            itemRand = new Random();
            leftAngle = new float[5];
            rightAngle = new float[5];
          

            rect = new Texture2D(graphics.GraphicsDevice, 30, 450);
            virtualBox = new Texture2D(graphics.GraphicsDevice, 150, 190);
            virtualShoot = new Texture2D(graphics.GraphicsDevice, 150, 190);
            Color[] data = new Color[30 * 450];
            Color[] color = new Color[150 * 190];
            Color[] color2 = new Color[150 * 190];
  
            rect.SetData(data);
            for (int i = 0; i < color.Length; ++i) {
                color[i] = Color.Green;
            }
            virtualBox.SetData(color);

            for (int i = 0; i < color.Length; ++i) {
                color2[i] = Color.Red;
            }
            virtualShoot.SetData(color2);

            coor = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.GraphicsDevice.Viewport.Height - 400);

            //Initialize Partition of Area
            for (int i = 0; i < Singleton.Instance.leftArea.Length; i++) {
                Singleton.Instance.leftArea[i] = ((graphics.PreferredBackBufferWidth / 2) / 5) * i;
                Singleton.Instance.rightArea[i] = (((graphics.PreferredBackBufferWidth / 2) / 5) * (i + 5));
            }
            side = sideRand.Next(0, 2);
            
            

            switch (side) {
                case 0:
                    Singleton.Instance.isLeftTurn  = true;
                    break;
                case 1:
                    Singleton.Instance.isRightTurn = true;
                    break;
            }

            this.graphics.PreferredBackBufferWidth  = 1600;
            this.graphics.PreferredBackBufferHeight = 1300;
            graphics.ApplyChanges();


            base.Initialize();
        }


        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("Bullet_Shiva");
            ball2 = Content.Load<Texture2D>("Bullet_Zeus");
            gameFont = Content.Load<SpriteFont>("gfont");
            background = Content.Load<Texture2D>("State");

            zeus = Content.Load<Texture2D>("Zeus");
            shiva = Content.Load<Texture2D>("Chiva");
            heart = Content.Load<Texture2D>("heart");

            shivaDead = Content.Load<Texture2D>("Shiva_Dead");
            zeusDead = Content.Load<Texture2D>("Zeus_Dead");

            select = Content.Load<Texture2D>("Select");
            target = Content.Load<Texture2D>("Target");




            player1 = new Player(shiva, heart) {
                Name = "Player1",
                Health = 3,
                WalkSlot = 2,
                Power = 1,
                getRect = new Rectangle((int)Singleton.Instance.leftArea[2], 920, 183, 183)

        };
          
            player2 = new Player(zeus, heart) {
                Name = "Player2",
                Health = 3,
                WalkSlot = 2,
                Power = 1,
                getRect = new Rectangle((int)Singleton.Instance.rightArea[2], 920, 183, 183)
            };

            gameObjects.Add(player1);
            gameObjects.Add(player2);

            Reset();
            Singleton.Instance.gameState = Singleton.GameState.ISPLAYING;




        }

        protected override void UnloadContent() {

        }


        double vi, t = 0; // vi - initial velocity | t - time
        double g = 520; // pixels per second squared | gravitational acceleration
        int keyState = 0;
       
        double v, vx, vy, alpha, t2 = 0;
        //----------------------------------------------------------------------//
        protected override void Update(GameTime gameTime) {
            

            if(Singleton.Instance.timer > 0)
                Singleton.Instance.timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if(player1.Health <= 0) {
                this.player1.IsActive = false;
                Singleton.Instance.gameState = Singleton.GameState.PLAYER2_WIN;     
            }
            if (player2.Health <= 0) {
                this.player2.IsActive = false;
                Singleton.Instance.gameState = Singleton.GameState.PLAYER1_WIN;
            }

            if (Singleton.Instance.gameState == Singleton.GameState.ISPLAYING) {

                // Allows the game to exit
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();

                foreach(GameObject g in gameObjects) {
                    g.Update(gameTime, gameObjects);

                }
                
                // Swap Turn
                if (shivaBall.Position.Y > graphics.GraphicsDevice.Viewport.Height - ball.Height) {
                    
                    shivaBall.Position = new Vector2(shivaBall.Position.X, graphics.GraphicsDevice.Viewport.Height - ball.Height);
                    Singleton.Instance.kState = 0;
                    t2 = 0;
                    Singleton.Instance.isRightTurn = false;
                    Singleton.Instance.isLeftTurn = true;
                    Reset();
                }

                if (zeusBall.Position.Y > graphics.GraphicsDevice.Viewport.Height - ball2.Height) {
                    
                    zeusBall.Position = new Vector2( zeusBall.Position.X, graphics.GraphicsDevice.Viewport.Height - ball2.Height);
                    Singleton.Instance.kState = 0;
                    t2 = 0;
                    Singleton.Instance.isLeftTurn = false;
                    Singleton.Instance.isRightTurn = true;
                    Reset();
                    
                }
                


            }


            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;

            gameObjects.RemoveAll(g => g.IsActive == false);
            base.Update(gameTime);
        }

        public void Reset() {

            Singleton.Instance.turnCount++;
            zeusBall = new Ball(ball) {
                Name = "zeusBall"

            };
            shivaBall = new Ball(ball2) {
                Name = "shivaBall"

            };
            gameObjects.Add(zeusBall);
            gameObjects.Add(shivaBall);

            virtualPos = Vector2.Zero;
            Singleton.Instance.isRightMove = false;
            Singleton.Instance.isLeftMove = false;
            Singleton.Instance.leftSideMove = 2;
            Singleton.Instance.rightSideMove = 2;
            Singleton.Instance.count = 0;
            Singleton.Instance.timer = 10;
            canWalk = true;
            Singleton.Instance.ballVisible = false;
            Singleton.Instance.ball2Visible = false;
            Singleton.Instance.virtualVisible = false;
            Singleton.Instance.virtualShootVisible = false;
            _isDecreaseHealth = false;
            Singleton.Instance.rightChooseShoot = false;
            Singleton.Instance.leftChooseShoot  = false;
            Singleton.Instance.leftSideShoot = 2;
            Singleton.Instance.rightSideShoot = 2;
            player1.WalkSlot++;
            player2.WalkSlot++;


            //set ball position according to player position this is open to change screen resolution.
            ball2pos = player2.Position;
            ball1pos = player1.Position;

            foreach (GameObject g in gameObjects) {
                g.Reset();
            }


        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);


            //Draw if not collide
            if (!isCollision(zeusBall,player2, player1.Power)) {
                if (this.IsActive)
                {
                    player2.Draw(spriteBatch);
                }
            }


            if (!isCollision(shivaBall, player1, player2.Power)) {
                if (this.IsActive)
                {
                    player1.Draw(spriteBatch);

                }

            }

                spriteBatch.DrawString(gameFont, "" + (Math.Floor(Singleton.Instance.timer) +1), new Vector2(graphics.PreferredBackBufferWidth / 2, 20), Color.Black);
   
            if (Singleton.Instance.ballVisible) {
                zeusBall.Draw(spriteBatch);
                
            }
            else if (Singleton.Instance.ball2Visible){
                shivaBall.Draw(spriteBatch);
                
            }
            
           
            if (Singleton.Instance.virtualVisible) {
                if (Singleton.Instance.isLeftTurn) {
                    spriteBatch.DrawString(gameFont, "Zeus Move", new Vector2((graphics.PreferredBackBufferWidth / 2) - 120, 150), Color.Black);
                    spriteBatch.Draw(select, new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideMove] , 550), Color.White);
                }
                else if (Singleton.Instance.isRightTurn) {
                    spriteBatch.DrawString(gameFont, "Shiva Move", new Vector2((graphics.PreferredBackBufferWidth / 2) - 120, 150), Color.Black);
                    spriteBatch.Draw(select, new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideMove], 550), Color.White);
                }
                 
            }
            if (Singleton.Instance.virtualShootVisible) {
                if (Singleton.Instance.isLeftMove && !Singleton.Instance.rightChooseShoot) {
                    spriteBatch.DrawString(gameFont, "Zeus Attack", new Vector2((graphics.PreferredBackBufferWidth / 2) - 120, 150), Color.Black);
                    spriteBatch.Draw(target, new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideShoot], 750), Color.White);
                }
                else if (Singleton.Instance.isRightMove && !Singleton.Instance.leftChooseShoot) {
                    spriteBatch.DrawString(gameFont, "Shiva Attack", new Vector2((graphics.PreferredBackBufferWidth / 2) - 120, 150), Color.Black);
                    spriteBatch.Draw(target, new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideShoot], 750), Color.White);
                }
            }

            if (Singleton.Instance.gameState == Singleton.GameState.PLAYER1_WIN)
            {
                //TODO When Player1 win the game...
                spriteBatch.DrawString(gameFont, "Shiva Win", new Vector2((graphics.PreferredBackBufferWidth / 2) - 120, 150), Color.Black);
                spriteBatch.Draw(zeusDead, this.player2.Position, Color.White);

            }
            if (Singleton.Instance.gameState == Singleton.GameState.PLAYER2_WIN)
            {
                //TODO When Player2 win the game...
                spriteBatch.DrawString(gameFont, "Zeus Win", new Vector2((graphics.PreferredBackBufferWidth / 2) - 120, 150), Color.Black);
                spriteBatch.Draw(shivaDead, this.player1.Position, Color.White);


            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

       public bool isCollision(GameObject obj1, GameObject obj2,int power) {
            if (obj1.getRect.Intersects(obj2.getRect) && _isDecreaseHealth == false) {
                obj2.Health -= power;
                obj1 = null;
             
                _isDecreaseHealth = true;

                return true;
            }
            else {
                return false;
            }
        }
    }
}
