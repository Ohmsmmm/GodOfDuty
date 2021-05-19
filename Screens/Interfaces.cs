using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodOfDuty
{
    interface IGameScreenManager : IDisposable
    {
        void ChangeScreen(IGameScreen screen);

        void PushScreen(IGameScreen screen);

        void PopScreen();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        void HandleInput(GameTime gameTime);

        void ChangeBetweenScreen();

        void Exit();

        event Action OnGameExit;
    }




    interface IGameScreen : IDisposable
    {
        bool IsPaused { get; set; }

        void Pause();

        void Resume();

        void Init(ContentManager content);

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime,SpriteBatch spriteBatch);

        void HandleInput(GameTime gameTime);

        void ChangeBetweenScreen();
    }
}
