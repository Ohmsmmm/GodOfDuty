using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodOfDuty
{
    class AboutScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;
  
        private int keyboardCursorPosCounter;
        private int cursorselectionPlayedcount;

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

        public AboutScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            _bg = content.Load<Texture2D>("sprites/about");
            _font = content.Load<SpriteFont>("font/File");




            _selected = content.Load<SoundEffect>("sounds/selected_sound").CreateInstance();
            _cursorselection = content.Load<SoundEffect>("sounds/selection_sound").CreateInstance();

            menu_button_scalelist = new List<Vector2>();
            menu_button_poslist = new List<Vector2>();
            menu_button_colorlist = new List<Color>();
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");

            menu_button_scalelist.Add(Vector2.One);

            menu_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2, 700));

            menu_button_colorlist.Add(Color.White);

            KeyboardCursorPos = menu_button_poslist[0];

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



            //Mouse and Keyboard Detect
            if (Singleton.Instance._currentmouse.Position != Singleton.Instance._previousmouse.Position || Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed || !Singleton.Instance.isKeyboardCursorActive)
            {
                Singleton.Instance.isMouseActive = true;
                Singleton.Instance.isKeyboardCursorActive = false;
            }
            else Singleton.Instance.isMouseActive = false;
            //End Mouse and Keyboard Detect
            cursorselectionPlayedcount = menu_button_colorlist.Count;//Initial check cursor selection
            Button(0);



        }

        private void Button(int i)
        {

            if (Singleton.Instance._currentmouse.Position.X > menu_button_poslist[i].X - _font.MeasureString("CONTROL").X / 2
                 && Singleton.Instance._currentmouse.Position.X < menu_button_poslist[i].X + _font.MeasureString("CONTROL").X / 2
                 && Singleton.Instance._currentmouse.Position.Y > menu_button_poslist[i].Y - _font.MeasureString("CONTROL").Y / 2
                 && Singleton.Instance._currentmouse.Position.Y < menu_button_poslist[i].Y + _font.MeasureString("CONTROL").Y / 2
                && Singleton.Instance.isMouseActive)

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
                            //Start to do play selected button sound
                            _selected.Volume = Singleton.Instance.MasterSFXVolume;
                            _selected.Play();
                            //End to do play selected button sound
                            m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                            break;
                        case 1:
                            //Start to do play selected button sound
                            _selected.Volume = Singleton.Instance.MasterSFXVolume;
                            _selected.Play();
                            //End to do play selected button sound
                            if (Singleton.Instance.CurrentStage >= 2)
                                m_exitGame = true;
                            else
                                m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                            break;

                    }

                }
            }
            else if (!Singleton.Instance.isKeyboardCursorActive)
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

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Escape) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                //to do play selection cursor sound
                if (!Singleton.Instance.isKeyboardCursorActive) _cursorselection.Play();
                //End to do play selection cursor sound
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                Singleton.Instance.isKeyboardCursorActive = true;
                keyboardCursorPosCounter++;
                if (keyboardCursorPosCounter > menu_button_poslist.Count - 1)
                    keyboardCursorPosCounter = 0;
                KeyboardCursorPos = menu_button_poslist[keyboardCursorPosCounter];
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);

            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                //to do play selection cursor sound
                if (!Singleton.Instance.isKeyboardCursorActive) _cursorselection.Play();
                //End to do play selection cursor sound
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                Singleton.Instance.isKeyboardCursorActive = true;
                keyboardCursorPosCounter--;
                if (keyboardCursorPosCounter < 0)
                    keyboardCursorPosCounter = menu_button_poslist.Count - 1;
                KeyboardCursorPos = menu_button_poslist[keyboardCursorPosCounter];
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);

            }


            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        //Start to do play selected button sound
                        _selected.Volume = Singleton.Instance.MasterSFXVolume;
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                        break;

                    case 1:
                        //Start to do play selected button sound
                        _selected.Volume = Singleton.Instance.MasterSFXVolume;
                        _selected.Play();
                        //End to do play selected button sound
                        if (Singleton.Instance.CurrentStage >= 2)
                            m_exitGame = true;
                        else
                        {
                            m_screenManager.ChangeScreen(new UpgradeScreen(m_screenManager));
                        }
                        break;
                }

            }


        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, Vector2.Zero, color: Color.White);


            spriteBatch.DrawString(_font, "BACK", menu_button_poslist[0], menu_button_colorlist[0], 0, _font.MeasureString("BACK") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);





            if (Singleton.Instance.isKeyboardCursorActive)
            {
                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        spriteBatch.DrawString(_font, "BACK", menu_button_poslist[0], Color.Red, 0, _font.MeasureString("BACK") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);
                        break;
                }
            }

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