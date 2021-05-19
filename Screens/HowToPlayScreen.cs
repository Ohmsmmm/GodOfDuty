using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GodOfDuty
{
    class HowToPlayScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;

        private int keyboardCursorPosCounter;
        private int cursorselectionPlayedcount;
        //private bool isMouseActive;
        private bool iscursorselectionPlayed;

        private List<Vector2> menu_button_poslist;
        private List<Vector2> menu_button_scalelist;
        private List<Color> menu_button_colorlist;

        private Texture2D _howtoplaybg;
        private SpriteFont _font;

        private Vector2 KeyboardCursorPos;

        SoundEffectInstance _selected;
        SoundEffectInstance _cursorselection;


        public HowToPlayScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }
        public bool IsPaused { get; set; }

        public void Init(ContentManager content)
        {
            _howtoplaybg = content.Load<Texture2D>("sprites/bg_control");
            _font = content.Load<SpriteFont>("font/File");

            _selected = content.Load<SoundEffect>("sounds/selected_sound").CreateInstance();
            _cursorselection = content.Load<SoundEffect>("sounds/selection_sound").CreateInstance();

            menu_button_poslist = new List<Vector2>();
            menu_button_scalelist = new List<Vector2>();
            menu_button_colorlist = new List<Color>();

            menu_button_scalelist.Add(Vector2.One);

            menu_button_poslist.Add(new Vector2(900, 650));

            menu_button_colorlist.Add(Color.White);

            KeyboardCursorPos = menu_button_poslist[0];

            _selected.Volume = Singleton.Instance.MasterSFXVolume;
            _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;


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

            if (Singleton.Instance._currentmouse.Position.X > menu_button_poslist[0].X - _font.MeasureString("OK").X
                    && Singleton.Instance._currentmouse.Position.X < menu_button_poslist[0].X + _font.MeasureString("OK").X
                    && Singleton.Instance._currentmouse.Position.Y > menu_button_poslist[0].Y - _font.MeasureString("OK").Y
                    && Singleton.Instance._currentmouse.Position.Y < menu_button_poslist[0].Y + _font.MeasureString("OK").Y && Singleton.Instance.isMouseActive)
            //Back button
            {
                KeyboardCursorPos = menu_button_poslist[0];
                menu_button_colorlist[0] = Color.Red;
                menu_button_scalelist[0] = new Vector2(1.7f, 1.7f);
                keyboardCursorPosCounter = menu_button_poslist.Count - 1;
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
                    menu_button_colorlist[0] = Color.OrangeRed;
                    menu_button_scalelist[0] = new Vector2(1.6f, 1.6f);
                }
                else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                {
                    menu_button_colorlist[0] = Color.Red;

                    //Do when click Back button
                    if (Singleton.Instance.CurrentStage == 1)
                        m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                    else if (Singleton.Instance.CurrentStage == 0)
                        m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                    //Start to do play selected button sound
                    _selected.Volume = Singleton.Instance.MasterSFXVolume;
                    _selected.Play();
                    //End to do play selected button sound
                }
            }
            else
            {
                menu_button_colorlist[0] = Color.White;
                menu_button_scalelist[0] = new Vector2(1.5f, 1.5f);
                //Check cursor sound played
                cursorselectionPlayedcount--;
                if (cursorselectionPlayedcount == 0)
                    iscursorselectionPlayed = false;
                //End check cursor sound played
            }
            cursorselectionPlayedcount = menu_button_colorlist.Count;//Initial check cursor selection
        }

        public void HandleInput(GameTime gameTime)
        {
            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Escape) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                if (Singleton.Instance.CurrentStage == 1)
                    m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                else if (Singleton.Instance.CurrentStage == 0)
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
                        _selected.Play();
                        //End to do play selected button sound
                        if (Singleton.Instance.CurrentStage == 1)
                            m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                        else if (Singleton.Instance.CurrentStage == 0)
                            m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                        break;
                }

            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_howtoplaybg, Vector2.Zero);

            spriteBatch.DrawString(_font, "OK", menu_button_poslist[0], menu_button_colorlist[0], 0, _font.MeasureString("OK") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);

            if (Singleton.Instance.isKeyboardCursorActive)
            {
                switch (keyboardCursorPosCounter)
                {
                    case 0:
                        spriteBatch.DrawString(_font, "OK", menu_button_poslist[0], Color.Red, 0, _font.MeasureString("OK") / 2, menu_button_scalelist[0], SpriteEffects.None, 0);
                        break;
                }
            }
            spriteBatch.End();

        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void ChangeBetweenScreen()
        {
        }

        public void Dispose()
        {
        }

    }
}