using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GodOfDuty
{
    class LoseScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;
        private bool isKeyboardCursorActive;
        private int keyboardCursorPosCounter;
        private int cursorselectionPlayedcount;
        private bool isMouseActive;
        private bool iscursorselectionPlayed;

        private List<Vector2> menu_button_poslist;
        private List<Vector2> menu_button_scalelist;
        private List<Color> menu_button_colorlist;
        private Vector2 KeyboardCursorPos;

        SoundEffectInstance _selected;
        SoundEffectInstance _cursorselection;




        public bool IsPaused { get; set; }

        GameObject _character;
        List<GameObject> _gameObjects;

        Texture2D _KeyboardCursor;
        Texture2D _bg;
        Texture2D _selectedChar;
        SpriteFont _font;

        public LoseScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            _bg = content.Load<Texture2D>("sprites/bg_lose");
            _font = content.Load<SpriteFont>("font/File");

            

            switch (Singleton.Instance.CurrentHero)
            {
                case "zeus":

                    _selectedChar = content.Load<Texture2D>("sprites/sheet_zeus");
                    _character = new GameObject(null,
                                   null,
                                   new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                       {
                                            { "Die", new Animation(_selectedChar, new Rectangle(200,1250,200,250),1) }
                                       }),
                                   null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2),
                        HP = 0,
                        IsActive = false
                    };

                    break;

                case "thor":

                    _selectedChar = content.Load<Texture2D>("sprites/sheet_thor");
                    _character = new GameObject(null,
                                            null,
                                            new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                                {
                                           { "Die", new Animation(_selectedChar, new Rectangle(200,1250,200,250),1) }
                                                }),
                                            null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT / 2),
                        HP = 0,
                        IsActive = false
                    };

                    break;
            }

            _selected = content.Load<SoundEffect>("sounds/selected_sound").CreateInstance();
            _cursorselection = content.Load<SoundEffect>("sounds/selection_sound").CreateInstance();

            menu_button_scalelist = new List<Vector2>();
            menu_button_poslist = new List<Vector2>();
            menu_button_colorlist = new List<Color>();
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");

            menu_button_scalelist.Add(Vector2.One);
            menu_button_scalelist.Add(Vector2.One);


            menu_button_poslist.Add(new Vector2(400, 600));
            menu_button_poslist.Add(new Vector2(800, 600));


            menu_button_colorlist.Add(Color.White);
            menu_button_colorlist.Add(Color.White);


            KeyboardCursorPos = menu_button_poslist[0];
            isKeyboardCursorActive = false;

            isMouseActive = false;

            _selected.Volume = Singleton.Instance.MasterSFXVolume;
            _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;


        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Update(GameTime gameTime)
        {
            Singleton.Instance._previouskey = Singleton.Instance._currentkey;
            Singleton.Instance._currentkey = Keyboard.GetState();

            Singleton.Instance._previousmouse = Singleton.Instance._currentmouse;
            Singleton.Instance._currentmouse = Mouse.GetState();

            _character.Update(gameTime, _gameObjects);


            //Mouse and Keyboard Detect
            if (Singleton.Instance._currentmouse.Position != Singleton.Instance._previousmouse.Position || Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed || !isKeyboardCursorActive)
            {
                isMouseActive = true;
                isKeyboardCursorActive = false;
            }
            else isMouseActive = false;
            //End Mouse and Keyboard Detect
            cursorselectionPlayedcount = menu_button_colorlist.Count;//Initial check cursor selection
            Button(0);
            Button(1);


        }

        private void Button(int i)
        {

            if (Singleton.Instance._currentmouse.Position.X > menu_button_poslist[i].X - _font.MeasureString("CONTROL").X / 2
                 && Singleton.Instance._currentmouse.Position.X < menu_button_poslist[i].X + _font.MeasureString("CONTROL").X / 2
                 && Singleton.Instance._currentmouse.Position.Y > menu_button_poslist[i].Y - _font.MeasureString("CONTROL").Y / 2
                 && Singleton.Instance._currentmouse.Position.Y < menu_button_poslist[i].Y + _font.MeasureString("CONTROL").Y / 2
                && isMouseActive)

            {
                menu_button_scalelist[i] = new Vector2(1.2f, 1.2f);
                menu_button_colorlist[i] = Color.Red;
                KeyboardCursorPos = menu_button_poslist[i];
                keyboardCursorPosCounter = i;
                //Start to do play selection cursor sound
                cursorselectionPlayedcount++;
                //_cursorselection.IsLooped = false;
                _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;

                if (!iscursorselectionPlayed && cursorselectionPlayedcount > 0)
                {
                    _cursorselection.Play();
                    iscursorselectionPlayed = true;
                }

                //End to do play selection cursor sound
                if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                {
                    menu_button_scalelist[i] = new Vector2(1.1f, 1.1f);
                    menu_button_colorlist[i] = Color.OrangeRed;
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    switch (i)
                    {
                        case 0:
                            Singleton.Instance.CurrentStage = 0;
                            Singleton.Instance.CurrentHero = "";
                            //Start to do play selected button sound
                            _selected.Play();
                            //End to do play selected button sound
                            m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                            break;
                        case 1:
                            //Start to do play selected button sound
                            _selected.Play();
                            //End to do play selected button sound

                            m_exitGame = true;

                            break;

                    }

                }
            }
            else if (!isKeyboardCursorActive)
            {
                menu_button_scalelist[i] = Vector2.One;
                menu_button_colorlist[i] = Color.White;
                //Check cursor sound played
                cursorselectionPlayedcount--;
                if (cursorselectionPlayedcount == 0)
                    iscursorselectionPlayed = false;
                //End check cursor sound played
            }
        }

        public void HandleInput(GameTime gameTime)
        {

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Right) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter++;
                if (keyboardCursorPosCounter > menu_button_poslist.Count - 1)
                    keyboardCursorPosCounter = 0;
                KeyboardCursorPos = menu_button_poslist[keyboardCursorPosCounter];
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);
                //to do play selection cursor sound
                _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;
                _cursorselection.Play();
                //End to do play selection cursor sound
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                isKeyboardCursorActive = true;
                keyboardCursorPosCounter--;
                if (keyboardCursorPosCounter < 0)
                    keyboardCursorPosCounter = menu_button_poslist.Count - 1;
                KeyboardCursorPos = menu_button_poslist[keyboardCursorPosCounter];
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);
                //to do play selection cursor sound
                _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;
                _cursorselection.Play();
                //End to do play selection cursor sound
            }


            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        //Start to do play selected button sound
                        Singleton.Instance.CurrentStage = 0;
                        Singleton.Instance.CurrentHero = "";
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                        break;

                    case 1:
                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound
                        m_exitGame = true;
                        break;

                }

            }


        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, Vector2.Zero, color: Color.White);


            spriteBatch.DrawString(_font, "MENU", menu_button_poslist[0], menu_button_colorlist[0], 0, _font.MeasureString("MENU") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);

            spriteBatch.DrawString(_font, "YOU LOSE!!!", new Vector2(Singleton.SCREENWIDTH / 2, 200), Color.White, 0, _font.MeasureString("YOU LOSE!!!") / 2, 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "EXIT", menu_button_poslist[1], menu_button_colorlist[1], 0, _font.MeasureString("EXIT") / 2, menu_button_scalelist[1], SpriteEffects.None, 0);




            if (isKeyboardCursorActive)
            {
                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        spriteBatch.DrawString(_font, "MENU", menu_button_poslist[0], Color.Red, 0, _font.MeasureString("MENU") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);
                        break;

                    case 1:
                        spriteBatch.DrawString(_font, "EXIT", menu_button_poslist[1], Color.Red, 0, _font.MeasureString("EXIT") / 2, menu_button_scalelist[1], SpriteEffects.None, 0);
                        break;
                }
            }

            _character.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void ChangeBetweenScreen()
        {
            if (m_exitGame)
            {
                m_screenManager.Exit();
            }
        }

        public void Dispose()
        {

        }
    }
}