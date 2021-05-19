using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GodOfDuty
{
    class UpgradeScreen : IGameScreen
    {
        private readonly IGameScreenManager m_screenManager;
        private bool m_exitGame;
        //private bool isKeyboardCursorActive;
        private int keyboardCursorPosCounter;
        private Keycursorstate keycursorstate;
        private int cursorselectionPlayedcount;
        //private bool isMouseActive;
        private bool iscursorselectionPlayed;
        private enum Keycursorstate
        {
            Skill,
            Navigation,
            Size
        }

        public bool IsPaused { get; set; }

        private GameObject selectedChar;
        private List<GameObject> _gameObjects;
        private List<Vector2> skill_button_poslist;
        private List<Vector2> nav_button_poslist;
        private List<Vector2> skill_button_scalelist;
        private List<Vector2> nav_button_scalelist;
        private List<Texture2D> skill_button_texturelist;
        private List<Color> nav_button_colorlist;
        private List<Vector2> _addskill_button_scalelist;
        private List<String> nav_button_name_list;

        private String[,] _skillDes;



        private int skillPoint;

        private int previous_level_sk1;
        private int previous_level_sk2;
        private int previous_level_sk3;


        //ContentManager content;
        SoundEffectInstance _selected;
        SoundEffectInstance _selected1;
        SoundEffectInstance _cursorselection;

        Texture2D _bg;
        Texture2D _bg2;
        SpriteFont _font;
        Texture2D _KeyboardCursor;
        Texture2D _selectedChar;
        Texture2D _skillwindow;
        Texture2D _addskill;

        private Vector2 KeyboardCursorPos;
        public UpgradeScreen(IGameScreenManager screenManager)
        {
            m_screenManager = screenManager;
        }


        public void Init(ContentManager content)
        {


            if (Singleton.Instance.CurrentStage >= 2)
            {
                Singleton.Instance.CurrentStage = 0;
                m_screenManager.ChangeScreen(new WinScreen(m_screenManager));
            }

            _gameObjects = new List<GameObject>();
            skill_button_texturelist = new List<Texture2D>();
            skill_button_scalelist = new List<Vector2>();
            skill_button_poslist = new List<Vector2>();
            nav_button_scalelist = new List<Vector2>();
            nav_button_poslist = new List<Vector2>();
            nav_button_colorlist = new List<Color>();
            _addskill_button_scalelist = new List<Vector2>();
            nav_button_name_list = new List<string>();
            _skillDes = new string[3, 3];


            _bg2 = content.Load<Texture2D>("sprites/fram1");
            _font = content.Load<SpriteFont>("font/File");
            _KeyboardCursor = content.Load<Texture2D>("sprites/hitbox");
            _skillwindow = content.Load<Texture2D>("sprites/newmainskill");
            _addskill = content.Load<Texture2D>("sprites/newplus");

            _selected = content.Load<SoundEffect>("sounds/selected_sound").CreateInstance();
            _selected1 = content.Load<SoundEffect>("sounds/selected_sound").CreateInstance();
            _cursorselection = content.Load<SoundEffect>("sounds/selection_sound").CreateInstance();


            switch (Singleton.Instance.CurrentHero)
            {
                case "zeus":
                    _bg = content.Load<Texture2D>("sprites/z1");
                    _selectedChar = content.Load<Texture2D>("sprites/sheet_zeus");
                    selectedChar = new GameObject(null,
                                   null,
                                   new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                       {
                                            { "Idle", new Animation(_selectedChar, new Rectangle(0,0,400,240),2) }
                                       }),
                                   null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2 - 400, Singleton.SCREENHEIGHT / 2),
                        HP = 1,
                        IsActive = false
                    };
                    _gameObjects.Add(selectedChar);

                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_zeus_1"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_zeus_2"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_zeus_3"));

                    _skillDes[0, 0] = "lv.1  +2 damage";
                    _skillDes[0, 1] = "lv.2  +3 damage";
                    _skillDes[0, 2] = "lv.3  +4 damage";

                    _skillDes[1, 0] = "lv.1  +2 damage";
                    _skillDes[1, 1] = "lv.2  +4 damage";
                    _skillDes[1, 2] = "lv.3  +6 damage";

                    _skillDes[2, 0] = "lv.1  30% chance";
                    _skillDes[2, 1] = "lv.2  40% chance";
                    _skillDes[2, 2] = "lv.3  50% chance";
                    break;

                case "thor":
                    _bg = content.Load<Texture2D>("sprites/t1");
                    _selectedChar = content.Load<Texture2D>("sprites/sheet_thor");
                    selectedChar = new GameObject(null,
                                            null,
                                            new CharacterGraphicComponent(content, new Dictionary<string, Animation>()
                                                {
                                            { "Idle", new Animation(_selectedChar, new Rectangle(0,0,400,240),2) }
                                                }),
                                            null)
                    {
                        Position = new Vector2(Singleton.SCREENWIDTH / 2 - 400, Singleton.SCREENHEIGHT / 2),
                        HP = 1,
                        IsActive = false
                    };
                    _gameObjects.Add(selectedChar);

                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_thor_1"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_thor_2"));
                    skill_button_texturelist.Add(content.Load<Texture2D>("sprites/skill_thor_3"));

                    _skillDes[0, 0] = "lv.1  +1 armor";
                    _skillDes[0, 1] = "lv.2  +2 armor";
                    _skillDes[0, 2] = "lv.3  +3 armor";

                    _skillDes[1, 0] = "lv.1  +25% size";
                    _skillDes[1, 1] = "lv.2  +50% size";
                    _skillDes[1, 2] = "lv.3  +100% size";

                    _skillDes[2, 0] = "lv.1  10% chance";
                    _skillDes[2, 1] = "lv.2  20% chance";
                    _skillDes[2, 2] = "lv.3  30% chance";
                    break;
            }

            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);
            skill_button_scalelist.Add(Vector2.One);

            skill_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 - 50, Singleton.SCREENHEIGHT / 2));
            skill_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 + 200, Singleton.SCREENHEIGHT / 2));
            skill_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 + 450, Singleton.SCREENHEIGHT / 2));

            _addskill_button_scalelist.Add(Vector2.One);
            _addskill_button_scalelist.Add(Vector2.One);
            _addskill_button_scalelist.Add(Vector2.One);

            nav_button_poslist.Add(new Vector2(100, Singleton.SCREENHEIGHT - 100));
            nav_button_poslist.Add(new Vector2(300, Singleton.SCREENHEIGHT - 100));
            nav_button_poslist.Add(new Vector2(Singleton.SCREENWIDTH / 2 + 200, Singleton.SCREENHEIGHT - 100));

            nav_button_scalelist.Add(new Vector2(1.5f, 1.5f));
            nav_button_scalelist.Add(new Vector2(1.5f, 1.5f));
            nav_button_scalelist.Add(new Vector2(1.5f, 1.5f));

            nav_button_colorlist.Add(Color.White);
            nav_button_colorlist.Add(Color.White);
            nav_button_colorlist.Add(Color.White);

            nav_button_name_list.Add("Back");
            nav_button_name_list.Add("START");
            nav_button_name_list.Add("Reset skill");

            KeyboardCursorPos = skill_button_poslist[0];

            skillPoint = 2;

            keycursorstate = Keycursorstate.Skill;

            _selected.Volume = Singleton.Instance.MasterSFXVolume;
            _selected1.Volume = Singleton.Instance.MasterSFXVolume;
            _cursorselection.Volume = Singleton.Instance.MasterSFXVolume;

            //Console.WriteLine("Upgrade Screen stage " + Singleton.Instance.CurrentStage);
            //Console.WriteLine("Hero value: " + Singleton.Instance.CurrentHero);
            //Console.WriteLine("Stage value: " + Singleton.Instance.CurrentStage);
            //Console.WriteLine("skill 1 value: " + Singleton.Instance.level_sk1);
            //Console.WriteLine("skill 2 value: " + Singleton.Instance.level_sk2);
            //Console.WriteLine("skill 3 value: " + Singleton.Instance.level_sk3);
            //Console.WriteLine("Previous skill 1 value: " + Singleton.Instance.previous_level_sk1);
            //Console.WriteLine("Previous skill 2 value: " + Singleton.Instance.previous_level_sk2);
            //Console.WriteLine("Previous skill 3 value: " + Singleton.Instance.previous_level_sk3);
            //Console.WriteLine("Keyboard status: " + Singleton.Instance.isKeyboardCursorActive);
            //Console.WriteLine("Mouse status: " + Singleton.Instance.isMouseActive);
            //Console.WriteLine("BGM Value: " + Singleton.Instance.MasterBGMVolume);
            //Console.WriteLine("SFX Value: " + Singleton.Instance.MasterSFXVolume);
            //Console.WriteLine("----------------------------------------------------------------------------------------------------");

            previous_level_sk1 = Singleton.Instance.level_sk1;
            previous_level_sk2 = Singleton.Instance.level_sk2;
            previous_level_sk3 = Singleton.Instance.level_sk3;
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
            cursorselectionPlayedcount = nav_button_poslist.Count + skill_button_poslist.Count;//Initial check cursor selection

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

            //Start skill1-3 button loop
            for (int i = 0; i < skill_button_poslist.Count; i++)
            {
                if (Singleton.Instance._currentmouse.Position.X > skill_button_poslist[i].X - _skillwindow.Width / 2
                && Singleton.Instance._currentmouse.Position.X < skill_button_poslist[i].X + _skillwindow.Width / 2
                && Singleton.Instance._currentmouse.Position.Y > skill_button_poslist[i].Y - _skillwindow.Height / 2
                && Singleton.Instance._currentmouse.Position.Y < skill_button_poslist[i].Y + _skillwindow.Height / 2 && Singleton.Instance.isMouseActive)
                //skill1-3 button
                {
                    skill_button_scalelist[i] = new Vector2(1.2f, 1.2f);
                    _addskill_button_scalelist[i] = new Vector2(1.2f, 1.2f);
                    KeyboardCursorPos = skill_button_poslist[i];
                    keycursorstate = Keycursorstate.Skill;
                    keyboardCursorPosCounter = i;

                    //Start to do play selection cursor sound
                    cursorselectionPlayedcount++;
                    if (!iscursorselectionPlayed && cursorselectionPlayedcount > 0)
                    {
                        _cursorselection.Play();
                        iscursorselectionPlayed = true;
                    }
                    //End to do play selection cursor sound

                    if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Pressed)
                    {
                        _addskill_button_scalelist[i] = new Vector2(1.1f, 1.1f);
                    }
                    else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                    {
                        //Do when click skill3 button
                        SkillCommandList(i);
                    }
                }
                else
                {
                    skill_button_scalelist[i] = Vector2.One;
                    _addskill_button_scalelist[i] = Vector2.One;
                    //Check cursor sound played
                    cursorselectionPlayedcount--;
                    if (cursorselectionPlayedcount == 0)
                        iscursorselectionPlayed = false;
                    //End check cursor sound played
                }
            }
            //End skill1-3 button loop

            //Start do back, start, Reset skill button loop
            for (int i = 0; i < nav_button_name_list.Count; i++)
            {
                if (Singleton.Instance._currentmouse.Position.X > nav_button_poslist[i].X - _font.MeasureString(nav_button_name_list[i]).X
                && Singleton.Instance._currentmouse.Position.X < nav_button_poslist[i].X + _font.MeasureString(nav_button_name_list[i]).X
                && Singleton.Instance._currentmouse.Position.Y > nav_button_poslist[i].Y - _font.MeasureString(nav_button_name_list[i]).Y
                && Singleton.Instance._currentmouse.Position.Y < nav_button_poslist[i].Y + _font.MeasureString(nav_button_name_list[i]).Y && Singleton.Instance.isMouseActive)

                {
                    KeyboardCursorPos = nav_button_poslist[i];
                    nav_button_colorlist[i] = Color.Red;
                    nav_button_scalelist[i] = new Vector2(1.7f, 1.7f);
                    keycursorstate = Keycursorstate.Navigation;
                    keyboardCursorPosCounter = skill_button_poslist.Count + i;

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
                        nav_button_colorlist[i] = Color.OrangeRed;
                        nav_button_scalelist[i] = new Vector2(1.6f, 1.6f);

                    }
                    else if (Singleton.Instance._currentmouse.LeftButton == ButtonState.Released && Singleton.Instance._previousmouse.LeftButton == ButtonState.Pressed)
                    {
                        nav_button_colorlist[i] = Color.Red;
                        //Do when click Reset skill button
                        NavCommandList(i);
                    }
                }
                else
                {
                    nav_button_colorlist[i] = Color.White;
                    nav_button_scalelist[i] = new Vector2(1.5f, 1.5f);
                    //Check cursor sound played
                    cursorselectionPlayedcount--;
                    if (cursorselectionPlayedcount == 0)
                        iscursorselectionPlayed = false;
                    //End check cursor sound played
                }
            }
            //End do back, start, Reset skill button loop

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(gameTime, _gameObjects);
            }


        }

        public void HandleInput(GameTime gameTime)
        {

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Right) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
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
                switch (keycursorstate)
                {
                    case Keycursorstate.Skill:
                        if (keyboardCursorPosCounter > skill_button_poslist.Count - 1)
                        {
                            keyboardCursorPosCounter = 0;
                        }
                        KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                        /*                        Console.WriteLine("Skill Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                                                Console.WriteLine("State = " + (int)keycursorstate);*/

                        break;
                    case Keycursorstate.Navigation:

                        if (keyboardCursorPosCounter >= nav_button_poslist.Count + skill_button_poslist.Count)
                        {
                            keyboardCursorPosCounter = skill_button_poslist.Count;
                        }
                        KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                        /*                        Console.WriteLine("Nav Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + (nav_button_poslist.Count + skill_button_poslist.Count));
                                                Console.WriteLine("State = " + (int)keycursorstate);*/

                        break;
                }
                _cursorselection.Play();
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Left) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
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
                switch (keycursorstate)
                {
                    case Keycursorstate.Skill:
                        if (keyboardCursorPosCounter < 0)
                        {
                            keyboardCursorPosCounter = skill_button_poslist.Count - 1;
                        }
                        KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                        /*                        Console.WriteLine("Skill Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                                                Console.WriteLine("State = " + (int)keycursorstate);*/

                        break;
                    case Keycursorstate.Navigation:

                        if (keyboardCursorPosCounter < skill_button_poslist.Count)
                        {
                            keyboardCursorPosCounter = nav_button_poslist.Count + skill_button_poslist.Count - 1;
                        }
                        KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                        /*                        Console.WriteLine("Nav Right key Pos: " + keyboardCursorPosCounter + "Real pos: " + ((nav_button_poslist.Count - 1) + (skill_button_poslist.Count - 1)));
                                                Console.WriteLine("State = " + (int)keycursorstate);*/

                        break;
                }
                _cursorselection.Play();

            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Down) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                bool isFirstActive;
                if (!Singleton.Instance.isKeyboardCursorActive)
                {
                    isFirstActive = true;
                    Singleton.Instance.isKeyboardCursorActive = true;
                }
                else
                {
                    isFirstActive = false;
                }
                switch (keycursorstate)
                {
                    case Keycursorstate.Skill:
                        if (!isFirstActive)
                        {
                            keyboardCursorPosCounter = nav_button_poslist.Count + skill_button_poslist.Count - 1;
                            KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                            keycursorstate = Keycursorstate.Navigation;
                        }
                        _cursorselection.Play();
                        /*                        Console.WriteLine("Nav Down key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                                                Console.WriteLine("State = " + (int)keycursorstate);*/
                        break;
                    case Keycursorstate.Navigation:
                        if (!isFirstActive)
                        {
                            keyboardCursorPosCounter = 0;
                            KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                            keycursorstate = Keycursorstate.Skill;
                        }
                        _cursorselection.Play();
                        /*                        Console.WriteLine("Skill Down key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                                                Console.WriteLine("State = " + (int)keycursorstate);*/
                        break;
                }
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Up) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                bool isFirstActive;
                if (!Singleton.Instance.isKeyboardCursorActive)
                {
                    isFirstActive = true;
                    Singleton.Instance.isKeyboardCursorActive = true;
                }
                else
                {
                    isFirstActive = false;
                }
                switch (keycursorstate)
                {
                    case Keycursorstate.Skill:
                        if (!isFirstActive)
                        {
                            keyboardCursorPosCounter = nav_button_poslist.Count + skill_button_poslist.Count - 1;
                            KeyboardCursorPos = nav_button_poslist[keyboardCursorPosCounter - skill_button_poslist.Count];
                            keycursorstate = Keycursorstate.Navigation;
                        }
                        _cursorselection.Play();
                        /*                        Console.WriteLine("Nav Up key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                                                Console.WriteLine("State = " + (int)keycursorstate);*/
                        break;
                    case Keycursorstate.Navigation:
                        if (!isFirstActive)
                        {
                            keyboardCursorPosCounter = 0;
                            KeyboardCursorPos = skill_button_poslist[keyboardCursorPosCounter];
                            keycursorstate = Keycursorstate.Skill;
                            _cursorselection.Play();
                        }
                        /*                       Console.WriteLine("Skill Up key Pos: " + keyboardCursorPosCounter + "Real pos: " + keyboardCursorPosCounter);
                                               Console.WriteLine("State = " + (int)keycursorstate);*/
                        break;
                }
            }

            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Escape) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
            {
                _selected.Play();
                m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
            }


            if (Singleton.Instance._currentkey.IsKeyDown(Keys.Enter) && Singleton.Instance._currentkey != Singleton.Instance._previouskey && Singleton.Instance.isKeyboardCursorActive)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0://skill1
                        if (Singleton.Instance.level_sk1 < 3 && skillPoint > 0)
                        {
                            Singleton.Instance.level_sk1 += 1;
                            skillPoint -= 1;
                            //Start to do play selected button sound
                            if (_selected.State == SoundState.Playing)
                            {
                                _selected.Stop();
                                _selected1.Play();
                            }
                            else if (_selected1.State == SoundState.Playing)
                            {
                                _selected1.Stop();
                                _selected.Play();
                            }
                            else
                                _selected.Play();
                            //End to do play selected button sound
                        }
                        break;

                    case 1://skill2
                        if (Singleton.Instance.level_sk2 < 3 && skillPoint > 0)
                        {
                            Singleton.Instance.level_sk2 += 1;
                            skillPoint -= 1;
                            //Start to do play selected button sound
                            if (_selected.State == SoundState.Playing)
                            {
                                _selected.Stop();
                                _selected1.Play();
                            }
                            else if (_selected1.State == SoundState.Playing)
                            {
                                _selected1.Stop();
                                _selected.Play();
                            }
                            else
                                _selected.Play();
                            //End to do play selected button sound
                        }
                        break;

                    case 2://skill3
                        if (Singleton.Instance.level_sk3 < 3 && skillPoint > 0)
                        {
                            Singleton.Instance.level_sk3 += 1;
                            skillPoint -= 1;
                            //Start to do play selected button sound
                            if (_selected.State == SoundState.Playing)
                            {
                                _selected.Stop();
                                _selected1.Play();
                            }
                            else if (_selected1.State == SoundState.Playing)
                            {
                                _selected1.Stop();
                                _selected.Play();
                            }
                            else
                                _selected.Play();
                            //End to do play selected button sound
                        }
                        break;
                    case 3://Back
                        //Do when enter Back button

                        //Start to do play selected button sound
                        _selected.Play();
                        //End to do play selected button sound


                        if (Singleton.Instance.CurrentStage == 0)
                            m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                        else
                        {
                            m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                        }
                        Singleton.Instance.CurrentStage = 0;
                        Singleton.Instance.CurrentHero = "";
                        break;
                    case 4://Start
                           //Do when enter Start button

                        //Start to do play selected button sound
                        _selected.Play();
                        Singleton.Instance.CurrentStage += 1;
                        //End to do play selected button sound
                        if (Singleton.Instance.CurrentStage == 1)
                            m_screenManager.ChangeScreen(new HowToPlayScreen(m_screenManager));
                        else
                            m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                        break;
                    case 5://Reset skill
                           //Do when enter Reset skill button

                        //Start to do play selected button sound
                        if (_selected.State == SoundState.Playing)
                        {
                            _selected.Stop();
                            _selected1.Play();
                        }
                        else if (_selected1.State == SoundState.Playing)
                        {
                            _selected1.Stop();
                            _selected.Play();
                        }
                        else
                            _selected.Play();
                        //End to do play selected button sound
                        Singleton.Instance.level_sk1 = previous_level_sk1;
                        Singleton.Instance.level_sk2 = previous_level_sk2;
                        Singleton.Instance.level_sk3 = previous_level_sk3;
                        skillPoint = 2;
                        break;

                }
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, Vector2.Zero);
            spriteBatch.Draw(_bg2, Vector2.Zero);
            //spriteBatch.DrawString(_font, "Upgrade Skill" + skillPoint, new Vector2(Singleton.SCREENWIDTH / 2 + 220, 100) - _font.MeasureString("Upgrade Skill") / 2, Color.White);
            spriteBatch.DrawString(_font,
                                    "Upgrade Skill",
                                    new Vector2(Singleton.SCREENWIDTH / 2 + 200, 130),
                                    Color.White, 0, _font.MeasureString("Upgrade Skill") / 2, 2, 0, 0);
            spriteBatch.DrawString(_font,
                                    Singleton.Instance.CurrentHero.ToUpper(),
                                    new Vector2(200, 130),
                                    Color.White, 0, _font.MeasureString(Singleton.Instance.CurrentHero.ToUpper()) / 2, 1.5f, 0, 0);
            if (skillPoint == 0)
                spriteBatch.DrawString(_font, "Remaining Skill Points :  " + skillPoint, new Vector2(Singleton.SCREENWIDTH / 2, 600) - _font.MeasureString("Remaining Skill Points :") / 2, Color.Red);
            else
                spriteBatch.DrawString(_font, "Remaining Skill Points :  " + skillPoint, new Vector2(Singleton.SCREENWIDTH / 2, 600) - _font.MeasureString("Remaining Skill Points :") / 2, Color.White);


            for (int i = 0; i <= skill_button_texturelist.Count - 1; i++)
            {
                spriteBatch.Draw(_skillwindow, skill_button_poslist[i], null, null, new Vector2(_skillwindow.Width / 2, _skillwindow.Height / 2), 0, Vector2.One, null, 0);
                spriteBatch.Draw(skill_button_texturelist[i], new Vector2(skill_button_poslist[i].X, skill_button_poslist[i].Y - 75), null, null, new Vector2(skill_button_texturelist[i].Width / 2, skill_button_texturelist[i].Height / 2), 0, skill_button_scalelist[i], null, 0);
            }

            if (skillPoint == 0)
            {
                spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk1, skill_button_poslist[0], Color.Red, 0, new Vector2((skill_button_texturelist[0].Width - 100) / 2, (skill_button_texturelist[0].Height + 150) / 2) + _font.MeasureString("level") / 2, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk2, skill_button_poslist[1], Color.Red, 0, new Vector2((skill_button_texturelist[1].Width - 100) / 2, (skill_button_texturelist[1].Height + 150) / 2) + _font.MeasureString("level") / 2, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk3, skill_button_poslist[2], Color.Red, 0, new Vector2((skill_button_texturelist[2].Width - 100) / 2, (skill_button_texturelist[2].Height + 150) / 2) + _font.MeasureString("level") / 2, 1, SpriteEffects.None, 0);
                spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[0].X, skill_button_poslist[0].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, Vector2.One, Color.Gray, 0);
                spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[1].X, skill_button_poslist[1].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, Vector2.One, Color.Gray, 0);
                spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[2].X, skill_button_poslist[2].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, Vector2.One, Color.Gray, 0);
            }
            else
            {
                if (Singleton.Instance.level_sk1 < 3)
                {
                    spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk1, skill_button_poslist[0], Color.White, 0, new Vector2((skill_button_texturelist[0].Width - 100) / 2, (skill_button_texturelist[0].Height + 150) / 2) + _font.MeasureString("level") / 2, _addskill_button_scalelist[0], SpriteEffects.None, 0);
                    spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[0].X, skill_button_poslist[0].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, _addskill_button_scalelist[0], null, 0);

                }
                else
                {
                    spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk1, skill_button_poslist[0], Color.Red, 0, new Vector2((skill_button_texturelist[0].Width - 100) / 2, (skill_button_texturelist[0].Height + 150) / 2) + _font.MeasureString("level") / 2, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[0].X, skill_button_poslist[0].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, Vector2.One, Color.Gray, 0);

                }
                if (Singleton.Instance.level_sk2 < 3)
                {
                    spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk2, skill_button_poslist[1], Color.White, 0, new Vector2((skill_button_texturelist[1].Width - 100) / 2, (skill_button_texturelist[1].Height + 150) / 2) + _font.MeasureString("level") / 2, _addskill_button_scalelist[1], SpriteEffects.None, 0);
                    spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[1].X, skill_button_poslist[1].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, _addskill_button_scalelist[1], null, 0);

                }
                else
                {
                    spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk2, skill_button_poslist[1], Color.Red, 0, new Vector2((skill_button_texturelist[1].Width - 100) / 2, (skill_button_texturelist[1].Height + 150) / 2) + _font.MeasureString("level") / 2, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[1].X, skill_button_poslist[1].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, Vector2.One, Color.Gray, 0);

                }
                if (Singleton.Instance.level_sk3 < 3)
                {
                    spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk3, skill_button_poslist[2], Color.White, 0, new Vector2((skill_button_texturelist[2].Width - 100) / 2, (skill_button_texturelist[2].Height + 150) / 2) + _font.MeasureString("level") / 2, _addskill_button_scalelist[2], SpriteEffects.None, 0);
                    spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[2].X, skill_button_poslist[2].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, _addskill_button_scalelist[2], null, 0);

                }
                else
                {
                    spriteBatch.DrawString(_font, "level  " + Singleton.Instance.level_sk3, skill_button_poslist[2], Color.Red, 0, new Vector2((skill_button_texturelist[2].Width - 100) / 2, (skill_button_texturelist[2].Height + 150) / 2) + _font.MeasureString("level") / 2, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(_addskill, new Vector2(skill_button_poslist[2].X, skill_button_poslist[2].Y + 118), null, null, new Vector2(_addskill.Width / 2, _addskill.Height / 2), 0, Vector2.One, Color.Gray, 0);

                }
            }

            for (int i = 0; i <= _skillDes.GetLength(0) - 1; i++)
            {
                spriteBatch.DrawString(_font, _skillDes[0, i], skill_button_poslist[0], Color.White, 0, new Vector2((skill_button_texturelist[0].Width - 100) / 2, (skill_button_texturelist[0].Height - 150 - 80 * i) / 2) + _font.MeasureString(_skillDes[0, i]) / 2, 0.8f, SpriteEffects.None, 0);
                spriteBatch.DrawString(_font, _skillDes[1, i], skill_button_poslist[1], Color.White, 0, new Vector2((skill_button_texturelist[1].Width - 100) / 2, (skill_button_texturelist[1].Height - 150 - 80 * i) / 2) + _font.MeasureString(_skillDes[1, i]) / 2, 0.8f, SpriteEffects.None, 0);
                spriteBatch.DrawString(_font, _skillDes[2, i], skill_button_poslist[2], Color.White, 0, new Vector2((skill_button_texturelist[2].Width - 100) / 2, (skill_button_texturelist[2].Height - 150 - 80 * i) / 2) + _font.MeasureString(_skillDes[2, i]) / 2, 0.8f, SpriteEffects.None, 0);
            }

            if (Singleton.Instance.CurrentStage == 0)
            {
                spriteBatch.DrawString(_font,
                                    nav_button_name_list[0],
                                    nav_button_poslist[0],
                                    nav_button_colorlist[0], 0, _font.MeasureString(nav_button_name_list[0]) / 2, nav_button_scalelist[0], 0, 0);
            }
            else
            {
                spriteBatch.DrawString(_font,
                                    "Menu",
                                    nav_button_poslist[0],
                                    nav_button_colorlist[0], 0, _font.MeasureString("Menu") / 2, nav_button_scalelist[0], 0, 0);
            }


            spriteBatch.DrawString(_font,
                    nav_button_name_list[1],
                    nav_button_poslist[1],
                    nav_button_colorlist[1], 0, _font.MeasureString(nav_button_name_list[1]) / 2, nav_button_scalelist[1], 0, 0);

            spriteBatch.DrawString(_font,
                        nav_button_name_list[2],
                        nav_button_poslist[2],
                        nav_button_colorlist[2], 0, _font.MeasureString(nav_button_name_list[2]) / 2, nav_button_scalelist[2], 0, 0);

            if (Singleton.Instance.isKeyboardCursorActive)
            {

                switch (keyboardCursorPosCounter)
                {
                    case 0:
                    case 1:
                    case 2:
                        spriteBatch.Draw(_KeyboardCursor, KeyboardCursorPos, null, new Rectangle(0, 0, 100, 100), new Vector2(_KeyboardCursor.Width / 2, _KeyboardCursor.Height / 2), 0, new Vector2(2.2f, 3.5f), Color.Red, 0);
                        break;
                    case 3:
                        if (Singleton.Instance.CurrentStage == 0)
                        {
                            spriteBatch.DrawString(_font,
                                    "Back",
                                    nav_button_poslist[0],
                                    Color.Red, 0, _font.MeasureString("Back") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        }
                        else
                        {
                            spriteBatch.DrawString(_font,
                                    "Menu",
                                    nav_button_poslist[0],
                                    Color.Red, 0, _font.MeasureString("Menu") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        }
                        break;
                    case 4:
                        spriteBatch.DrawString(_font,
                        "START",
                        nav_button_poslist[1],
                        Color.Red, 0, _font.MeasureString("START") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        break;
                    case 5:
                        spriteBatch.DrawString(_font,
                        "Reset skill",
                        nav_button_poslist[2],
                        Color.Red, 0, _font.MeasureString("Reset skill") / 2, new Vector2(1.5f, 1.5f), 0, 0);
                        break;
                }


                //}

            }


            //spriteBatch.DrawString(_font,
            //     "[Console] Res: " + Singleton.SCREENWIDTH + "x" + Singleton.SCREENHEIGHT + "    Keyboard status: " + Singleton.Instance.isKeyboardCursorActive + "    Mouse status: " + Singleton.Instance.isMouseActive + "   MousePos: " + Singleton.Instance._currentmouse.Position.X + ", " + Singleton.Instance._currentmouse.Position.Y,
            //     new Vector2(1, Singleton.SCREENHEIGHT - 20),
            //     Color.White, 0, new Vector2(0, 0), new Vector2(0.8f, 0.8f), 0, 0);

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Draw(spriteBatch);
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

        public void NavCommandList(int i)
        {
            switch (i)
            {
                case 0://back command

                    if (Singleton.Instance.CurrentStage == 0)
                        m_screenManager.ChangeScreen(new SelectCharScreen(m_screenManager));
                    else
                        m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));
                    Singleton.Instance.CurrentStage = 0;
                    Singleton.Instance.CurrentHero = "";
                    break;
                case 1://start command
                    Singleton.Instance.CurrentStage += 1;
                    if (Singleton.Instance.CurrentStage == 1)
                        m_screenManager.ChangeScreen(new HowToPlayScreen(m_screenManager));
                    else
                        m_screenManager.ChangeScreen(new PlayScreen(m_screenManager));
                    break;
                case 2://reset command
                    Singleton.Instance.level_sk1 = previous_level_sk1;
                    Singleton.Instance.level_sk2 = previous_level_sk2;
                    Singleton.Instance.level_sk3 = previous_level_sk3;
                    skillPoint = 2;
                    break;
            }
            //Start to do play selected button sound
            if (_selected.State == SoundState.Playing)
            {
                _selected.Stop();
                _selected1.Play();
            }
            else if (_selected1.State == SoundState.Playing)
            {
                _selected1.Stop();
                _selected.Play();
            }
            else
                _selected.Play();
            //End to do play selected button sound
        }

        public void SkillCommandList(int i)
        {
            switch (i)
            {
                case 0://skill1 command
                    if (Singleton.Instance.level_sk1 < 3 && skillPoint > 0)
                    {
                        Singleton.Instance.level_sk1 += 1;
                        skillPoint -= 1;
                        //Start to do play selected button sound
                        if (_selected.State == SoundState.Playing)
                        {
                            _selected.Stop();
                            _selected1.Play();
                        }
                        else if (_selected1.State == SoundState.Playing)
                        {
                            _selected1.Stop();
                            _selected.Play();
                        }
                        else
                            _selected.Play();
                        //End to do play selected button sound
                    }
                    break;
                case 1://skill2 command
                    if (Singleton.Instance.level_sk2 < 3 && skillPoint > 0)
                    {
                        Singleton.Instance.level_sk2 += 1;
                        skillPoint -= 1;
                        //Start to do play selected button sound
                        if (_selected.State == SoundState.Playing)
                        {
                            _selected.Stop();
                            _selected1.Play();
                        }
                        else if (_selected1.State == SoundState.Playing)
                        {
                            _selected1.Stop();
                            _selected.Play();
                        }
                        else
                            _selected.Play();
                        //End to do play selected button sound
                    }
                    break;
                case 2://skill3 command
                    if (Singleton.Instance.level_sk3 < 3 && skillPoint > 0)
                    {
                        Singleton.Instance.level_sk3 += 1;
                        skillPoint -= 1;
                        //Start to do play selected button sound
                        if (_selected.State == SoundState.Playing)
                        {
                            _selected.Stop();
                            _selected1.Play();
                        }
                        else if (_selected1.State == SoundState.Playing)
                        {
                            _selected1.Stop();
                            _selected.Play();
                        }
                        else
                            _selected.Play();
                        //End to do play selected button sound
                    }
                    break;
            }
        }
    }
}