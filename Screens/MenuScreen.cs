using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GodOfDuty
{
    class MenuScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;

        private int keyboardCursorPosCounter;
        private int cursorselectionPlayedcount;

        private bool iscursorselectionPlayed;

        Random rnd = new Random();

        public bool IsPaused { get; set; }

        Texture2D _bg;
        Texture2D _strip;
        SpriteFont _font;
        private List<Vector2> menu_button_poslist;
        private List<Vector2> menu_button_scalelist;
        private List<Color> menu_button_colorlist;

        SoundEffectInstance _selected;
        SoundEffectInstance _cursorselection;

        Texture2D _KeyboardCursor;

        private Vector2 KeyboardCursorPos;

        public MenuScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {
            int rand = rnd.Next(1, 5);
            _bg = content.Load<Texture2D>("sprites/menu_" + rand);
            _strip = content.Load<Texture2D>("sprites/menu_strip");
            _font = content.Load<SpriteFont>("font/File");

            _selected = content.Load<SoundEffect>("sounds/selected_sound").CreateInstance();
            _cursorselection = content.Load<SoundEffect>("sounds/selection_sound").CreateInstance();

            menu_button_scalelist = new List<Vector2>();
            menu_button_poslist = new List<Vector2>();
            menu_button_colorlist = new List<Color>();
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");

            menu_button_scalelist.Add(Vector2.One);
            menu_button_scalelist.Add(Vector2.One);
            menu_button_scalelist.Add(Vector2.One);
            menu_button_scalelist.Add(Vector2.One);
            menu_button_scalelist.Add(Vector2.One);

            menu_button_poslist.Add(new Vector2(300, 300));
            menu_button_poslist.Add(new Vector2(300, 400));
            menu_button_poslist.Add(new Vector2(300, 500));
            menu_button_poslist.Add(new Vector2(300, 600));
            menu_button_poslist.Add(new Vector2(300, 700));

            menu_button_colorlist.Add(Color.White);
            menu_button_colorlist.Add(Color.White);
            menu_button_colorlist.Add(Color.White);
            menu_button_colorlist.Add(Color.White);
            menu_button_colorlist.Add(Color.White);

            KeyboardCursorPos = menu_button_poslist[0];

            Singleton.Instance.isKeyboardCursorActive = true;
            Singleton.Instance.isMouseActive = false;

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
            Button(1);
            Button(2);
            Button(3);
            Button(4);

        }

        private void Button(int i)
        {

            if (Singleton.Instance._currentmouse.Position.X > menu_button_poslist[i].X - _font.MeasureString("HOW TO PLAY").X / 2
                 && Singleton.Instance._currentmouse.Position.X < menu_button_poslist[i].X + _font.MeasureString("HOW TO PLAY").X / 2
                 && Singleton.Instance._currentmouse.Position.Y > menu_button_poslist[i].Y - _font.MeasureString("HOW TO PLAY").Y / 2
                 && Singleton.Instance._currentmouse.Position.Y < menu_button_poslist[i].Y + _font.MeasureString("HOW TO PLAY").Y / 2
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
                            m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                            break;
                        case 1:
                            //Start to do play selected button sound
                            _selected.Volume = Singleton.Instance.MasterSFXVolume;
                            _selected.Play();
                            //End to do play selected button sound
                            m_screenManager.ChangeScreen(new HowToPlayScreen(m_screenManager));
                            break;
                        case 2:
                            //Start to do play selected button sound
                            _selected.Volume = Singleton.Instance.MasterSFXVolume;
                            _selected.Play();
                            //End to do play selected button sound
                            m_screenManager.ChangeScreen(new AboutScreen(m_screenManager));
                            break;
                        case 3:
                            //Start to do play selected button sound
                            _selected.Volume = Singleton.Instance.MasterSFXVolume;
                            _selected.Play();
                            //End to do play selected button sound
                            m_screenManager.ChangeScreen(new CharacterScreen(m_screenManager));
                            break;
                        case 4:
                            //Start to do play selected button sound
                            _selected.Volume = Singleton.Instance.MasterSFXVolume;
                            _selected.Play();
                            //End to do play selected button sound
                            m_screenManager.Exit();
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

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                bool isFirstActive;
                if (!Singleton.Instance.isKeyboardCursorActive)
                {
                    isFirstActive = true;
                    Singleton.Instance.isKeyboardCursorActive = true;
                }
                else
                {
                    isFirstActive = false;
                    keyboardCursorPosCounter++;
                }
                if (keyboardCursorPosCounter > menu_button_poslist.Count - 1)
                    keyboardCursorPosCounter = 0;
                KeyboardCursorPos = menu_button_poslist[keyboardCursorPosCounter];
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);
                //to do play selection cursor sound
                _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;
                _cursorselection.Play();
                //End to do play selection cursor sound
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1, 1);
                bool isFirstActive;
                if (!Singleton.Instance.isKeyboardCursorActive)
                {
                    isFirstActive = true;
                    Singleton.Instance.isKeyboardCursorActive = true;
                }
                else
                {
                    isFirstActive = false;
                    keyboardCursorPosCounter--;
                }
                if (keyboardCursorPosCounter < 0)
                    keyboardCursorPosCounter = menu_button_poslist.Count - 1;
                KeyboardCursorPos = menu_button_poslist[keyboardCursorPosCounter];
                menu_button_scalelist[keyboardCursorPosCounter] = new Vector2(1.2f, 1.2f);
                //to do play selection cursor sound
                _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;
                _cursorselection.Play();
                //End to do play selection cursor sound
            }


            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey && Singleton.Instance.isKeyboardCursorActive)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                        break;

                    case 1:
                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.ChangeScreen(new HowToPlayScreen(m_screenManager));
                        break;

                    case 2:
                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.ChangeScreen(new AboutScreen(m_screenManager));
                        break;

                    case 3:
                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.ChangeScreen(new CharacterScreen(m_screenManager));
                        break;
                    case 4:
                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound
                        m_screenManager.Exit();
                        break;
                }

            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(_bg, Vector2.Zero, color: Color.White);
            spriteBatch.Draw(_strip, new Vector2(100, 0), color: Color.White);
            //spriteBatch.DrawString(_font, "TALES WAR", new Vector2(300, 200) - _font.MeasureString("TALE WARS") / 2, Color.White);


            spriteBatch.DrawString(_font, "PLAY", menu_button_poslist[0], menu_button_colorlist[0], 0, _font.MeasureString("PLAY") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "HOW  TO  PLAY", menu_button_poslist[1], menu_button_colorlist[1], 0, _font.MeasureString("HOW TO PLAY") / 2, menu_button_scalelist[1], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "ABOUT", menu_button_poslist[2], menu_button_colorlist[2], 0, _font.MeasureString("ABOUT") / 2, menu_button_scalelist[2], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "CHARACTER", menu_button_poslist[3], menu_button_colorlist[3], 0, _font.MeasureString("CHARACTER") / 2, menu_button_scalelist[3], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "EXIT", menu_button_poslist[4], menu_button_colorlist[4], 0, _font.MeasureString("EXIT") / 2, menu_button_scalelist[4], SpriteEffects.None, 0);

            if (Singleton.Instance.isKeyboardCursorActive)
            {
                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        spriteBatch.DrawString(_font, "PLAY", menu_button_poslist[0], Color.Red, 0, _font.MeasureString("PLAY") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);
                        break;

                    case 1:
                        spriteBatch.DrawString(_font, "HOW  TO  PLAY", menu_button_poslist[1], Color.Red, 0, _font.MeasureString("HOW TO PLAY") / 2, menu_button_scalelist[1], SpriteEffects.None, 0);

                        break;
                    case 2:
                        spriteBatch.DrawString(_font, "ABOUT", menu_button_poslist[2], Color.Red, 0, _font.MeasureString("ABOUT") / 2, menu_button_scalelist[2], SpriteEffects.None, 0);

                        break;
                    case 3:
                        spriteBatch.DrawString(_font, "CHARACTER", menu_button_poslist[3], Color.Red, 0, _font.MeasureString("CHARACTER") / 2, menu_button_scalelist[3], SpriteEffects.None, 0);
                        break;

                    case 4:
                        spriteBatch.DrawString(_font, "EXIT", menu_button_poslist[4], Color.Red, 0, _font.MeasureString("EXIT") / 2, menu_button_scalelist[4], SpriteEffects.None, 0);
                        break;
                }
            }
            //spriteBatch.DrawString(_font,
            //     "[Console] Res: " + Singleton.SCREENWIDTH + "x" + Singleton.SCREENHEIGHT + "    Keyboard status: " + Singleton.Instance.isKeyboardCursorActive + "    Mouse status: " + Singleton.Instance.isMouseActive + "   MousePos: " + Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y,
            //     new Vector2(1, Singleton.SCREENHEIGHT - 20),
            //     Color.White, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), 0, 0);
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