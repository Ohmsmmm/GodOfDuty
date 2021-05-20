using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GodOfDuty
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<GameObject> _gameObjects;
        int _numObject;

        SpriteFont _font;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Singleton.SCREENWIDTH;
            _graphics.PreferredBackBufferHeight = Singleton.SCREENHEIGHT;
            _graphics.ApplyChanges();

            _gameObjects = new List<GameObject>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("GameFont");

            Reset();
        }

        protected override void Update(GameTime gameTime)
        {
            Singleton.Instance.CurrentKey = Keyboard.GetState();
            _numObject = _gameObjects.Count;

            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.StartNewLife:
                    Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
                    break;
                case Singleton.GameState.GamePlaying:
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (_gameObjects[i].IsActive) _gameObjects[i].Update(gameTime, _gameObjects);
                    }
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (!_gameObjects[i].IsActive)
                        {
                            _gameObjects.RemoveAt(i);
                            i--;
                            _numObject--;
                        }
                    }

                    if (Singleton.Instance.InvaderLeft <= 0)
                    {
                        ResetEnemies();

                        foreach (GameObject s in _gameObjects)
                        {
                            if (s is Enemy)
                            {
                                s.Reset();
                            }
                        }
                    }
                    if (Singleton.Instance.Life <= 0) Singleton.Instance.CurrentGameState = Singleton.GameState.GameOver;
                    break;
                case Singleton.GameState.GameOver:
                    if (!Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) && Singleton.Instance.CurrentKey.GetPressedKeys().Length > 0)
                    {
                        //any keys pressed to start
                        Reset();
                        Singleton.Instance.CurrentGameState = Singleton.GameState.StartNewLife;
                    }
                    break;
            }

            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(_spriteBatch);
            }

            Vector2 fontSize = _font.MeasureString("Score : " + Singleton.Instance.Score.ToString());
            _spriteBatch.DrawString(_font,
                "Score : " + Singleton.Instance.Score.ToString(),
                new Vector2((Singleton.SCREENWIDTH / 2 - fontSize.X) / 2, 30),
                Color.White);

            fontSize = _font.MeasureString("Life : " + Singleton.Instance.Life.ToString());
            _spriteBatch.DrawString(_font,
                "Life : " + Singleton.Instance.Life.ToString(),
                new Vector2((Singleton.SCREENWIDTH / 2 - fontSize.X) / 2 + Singleton.SCREENWIDTH / 2, 30),
                Color.White);

            if (Singleton.Instance.CurrentGameState == Singleton.GameState.GameOver)
            {
                fontSize = _font.MeasureString("Game Over");
                _spriteBatch.DrawString(_font,
                    "Game Over",
                    new Vector2((Singleton.SCREENWIDTH - fontSize.X) / 2 - 2,
                    (Singleton.SCREENHEIGHT - fontSize.Y) / 2 - 2),
                    Color.White);
                _spriteBatch.DrawString(_font,
                    "Game Over",
                    new Vector2((Singleton.SCREENWIDTH - fontSize.X) / 2 + 2,
                    (Singleton.SCREENHEIGHT - fontSize.Y) / 2 - 2),
                    Color.White);
                _spriteBatch.DrawString(_font,
                    "Game Over",
                    new Vector2((Singleton.SCREENWIDTH - fontSize.X) / 2 + 2,
                    (Singleton.SCREENHEIGHT - fontSize.Y) / 2 + 2),
                    Color.White);
                _spriteBatch.DrawString(_font,
                    "Game Over",
                    new Vector2((Singleton.SCREENWIDTH - fontSize.X) / 2 - 2,
                    (Singleton.SCREENHEIGHT - fontSize.Y) / 2 + 2),
                    Color.White);
                _spriteBatch.DrawString(_font,
                    "Game Over",
                    new Vector2((Singleton.SCREENWIDTH - fontSize.X) / 2,
                    (Singleton.SCREENHEIGHT - fontSize.Y) / 2),
                    Color.Red);
            }


            _spriteBatch.End();
            _graphics.BeginDraw();

            base.Draw(gameTime);
        }

        protected void Reset()
        {
            Singleton.Instance.Score = 0;
            Singleton.Instance.Life = 3;

            Singleton.Instance.CurrentGameState = Singleton.GameState.StartNewLife;

            Singleton.Instance.Random = new System.Random();

            Texture2D spaceInvaderTexture = this.Content.Load<Texture2D>("SpaceInvaderSheet");

            _gameObjects.Clear();
            _gameObjects.Add(new Player(spaceInvaderTexture)
            {
                Name = "Player",
                Viewport = new Rectangle(54, 30, 51, 30),
                Position = new Vector2(62, 640),
                Left = Keys.Left,
                Right = Keys.Right,
                Fire = Keys.Space,
                Bullet = new Bullet(spaceInvaderTexture)
                {
                    Name = "BulletPlayer",
                    Viewport = new Rectangle(216, 36, 3, 24),
                    Velocity = new Vector2(0, -600f)
                }
            });

            ResetEnemies();

            foreach (GameObject s in _gameObjects)
            {
                s.Reset();
            }

        }

        public void ResetEnemies()
        {
            Texture2D spaceInvaderTexture = this.Content.Load<Texture2D>("SpaceInvaderSheet");

            Singleton.Instance.InvaderLeft = 55;

            Invader newInvader30 = new Invader(spaceInvaderTexture)
            {
                Name = "Enemy",
                Viewport = new Rectangle(78, 0, 30, 30),
                Score = 30,
                Bullet = new Bullet(spaceInvaderTexture)
                {
                    Name = "BulletEnemy",
                    Viewport = new Rectangle(231, 36, 9, 21),
                    Velocity = new Vector2(0, 600f)
                }
            };
            Invader newInvader20 = new Invader(spaceInvaderTexture)
            {
                Name = "Enemy",
                Viewport = new Rectangle(0, 0, 39, 30),
                Score = 20,
                Bullet = new Bullet(spaceInvaderTexture)
                {
                    Name = "BulletEnemy",
                    Viewport = new Rectangle(231, 36, 9, 21),
                    Velocity = new Vector2(0, 600f)
                }
            };
            Invader newInvader10 = new Invader(spaceInvaderTexture)
            {
                Name = "Enemy",
                Viewport = new Rectangle(138, 0, 42, 30),
                Score = 10,
                Bullet = new Bullet(spaceInvaderTexture)
                {
                    Name = "BulletEnemy",
                    Viewport = new Rectangle(231, 36, 9, 21),
                    Velocity = new Vector2(0, 600f)
                }
            };


            for (int i = 0; i < 11; i++)
            {
                var clone = newInvader30.Clone() as Invader;
                clone.Position = new Vector2(Singleton.INVADERHORDEWIDTH / 11 * i +
                 (Singleton.INVADERHORDEWIDTH / 11 - newInvader30.Rectangle.Width) / 2, 130);
                _gameObjects.Add(clone);
            }
            for (int i = 0; i < 11; i++)
            {
                var clone = newInvader20.Clone() as Invader;
                clone.Position = new Vector2(Singleton.INVADERHORDEWIDTH / 11 * i +
                 (Singleton.INVADERHORDEWIDTH / 11 - newInvader20.Rectangle.Width) / 2, 160);
                _gameObjects.Add(clone);
            }
            for (int i = 0; i < 11; i++)
            {
                var clone = newInvader20.Clone() as Invader;
                clone.Position = new Vector2(Singleton.INVADERHORDEWIDTH / 11 * i +
                 (Singleton.INVADERHORDEWIDTH / 11 - newInvader20.Rectangle.Width) / 2, 190);
                _gameObjects.Add(clone);
            }
            for (int i = 0; i < 11; i++)
            {
                var clone = newInvader10.Clone() as Invader;
                clone.Position = new Vector2(Singleton.INVADERHORDEWIDTH / 11 * i +
                 (Singleton.INVADERHORDEWIDTH / 11 - newInvader10.Rectangle.Width) / 2, 220);
                _gameObjects.Add(clone);
            }
            for (int i = 0; i < 11; i++)
            {
                var clone = newInvader10.Clone() as Invader;
                clone.Position = new Vector2(Singleton.INVADERHORDEWIDTH / 11 * i +
                 (Singleton.INVADERHORDEWIDTH / 11 - newInvader10.Rectangle.Width) / 2, 250);
                _gameObjects.Add(clone);
            }
        }
    }
}
