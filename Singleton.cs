using Microsoft.Xna.Framework.Input;
using System;

namespace GodOfDuty
{
    class Singleton
    {
        public const int SCREENWIDTH = 700;
        public const int SCREENHEIGHT = 700;

        public const int INVADERHORDEWIDTH = 550;
        public const int INVADERHORDEHEIGHT = 150;

        public int InvaderLeft;

        public int Score;
        public int Life;

        public Random Random;

        public enum GameState
        {
            StartNewLife,
            GamePlaying,
            GameOver
        }
        public GameState CurrentGameState;

        public KeyboardState PreviousKey, CurrentKey;

        private static Singleton instance;

        private Singleton() { }

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
