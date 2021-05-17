using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GodOfDuty
{
    class GameScreenManager : IGameScreenManager
    {
        private readonly SpriteBatch m_spriteBatch;
        private readonly ContentManager m_contentManager;
        private Action m_onGameExit;

        private readonly IList<IGameScreen> m_gameScreens = new List<IGameScreen>();


        public GameScreenManager(SpriteBatch spriteBatch,ContentManager contentManager)
        {
            m_spriteBatch = spriteBatch;
            m_contentManager = contentManager;

        }

        public void ChangeScreen(IGameScreen screen)
        {
            RemoveAllScreens();

            m_gameScreens.Add(screen);

            screen.Init(m_contentManager);
        }

        public void PushScreen(IGameScreen screen)
        {
            if (!IsScreenListEmpty)
            {
                var curScreen = GetCurrentScreen();

                curScreen.Pause();
            }

            m_gameScreens.Add(screen);

            screen.Init(m_contentManager);
        }

        private bool IsScreenListEmpty
        {
            get
            {
                return m_gameScreens.Count <= 0;
            }
        }

        private IGameScreen GetCurrentScreen()
        {
            return m_gameScreens[m_gameScreens.Count - 1];
        }

        private void RemoveAllScreens()
        {
            while (!IsScreenListEmpty)
            {
                RemoveCurrentScreen();
            }
        }


        private void RemoveCurrentScreen()
        {
            var screen = GetCurrentScreen();

            screen.Dispose();

            m_gameScreens.Remove(screen);
        }

        public void PopScreen()
        {
            if (!IsScreenListEmpty)
            {
                RemoveCurrentScreen();
            }

            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                screen.Resume();
            }

        }

        public void Update(GameTime gameTime)
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                if (!screen.IsPaused)
                {
                    screen.Update(gameTime);
                }
            }

        }

        public void Draw(GameTime gameTime)
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                //if (!screen.IsPaused)
                //{
                //    screen.Draw(gameTime,m_spriteBatch);
                //}
                screen.Draw(gameTime, m_spriteBatch);

            }

        }

        public void HandleInput(GameTime gameTime)
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                //if (!screen.IsPaused)
                //{
                //    screen.HandleInput(gameTime);
                //}
                screen.HandleInput(gameTime);

            }
        }

        public void ChangeBetweenScreen()
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                if (!screen.IsPaused)
                {
                    screen.ChangeBetweenScreen();
                }
            }

        }

        public void Exit()
        {
            if(m_onGameExit != null)
            {
                m_onGameExit();
            }
        }

       public event Action OnGameExit
        {
            add { m_onGameExit += value; }
            remove { m_onGameExit -= value; }
        }

        public void Dispose()
        {
            RemoveAllScreens();
        }



    }
}
