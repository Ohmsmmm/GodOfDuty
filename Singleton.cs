using Microsoft.Xna.Framework.Input;


namespace Final_Assignment
{
    class Singleton
    {

        public KeyboardState _currentkey;
        public KeyboardState _previouskey;

        public MouseState _currentmouse;
        public MouseState _previousmouse;

        public const int SCREENWIDTH = 1200;
        public const int SCREENHEIGHT = 800;
        public GameObject follow;
        public Camera _camera;

        public string CurrentHero;
        public int CurrentStage;

        public int level_sk1;
        public int level_sk2;
        public int level_sk3;


        public int Cooldown_1;
        public int Cooldown_2;


        public float MasterBGMVolume = 0.2f;
        public float MasterSFXVolume = 0.3f;


        public bool isKeyboardCursorActive;
        public bool isMouseActive;


        public enum TurnState
        {
            skill,
            angle,
            force,
            shoot,
            enemy
        }

        public TurnState CurrentTurnState;

        private static Singleton instance;

        private Singleton()
        {

        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }

}
