using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GodOfDuty
{
    public class Main : Game
    {
        private SpriteBatch _spriteBatch;
        private SpriteBatch m_spriteBatch;
        private IGameScreenManager m_screenManager;

        SpriteFont _font;
        protected Song song;
        private GraphicsDeviceManager graphics;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Singleton.SCREENWIDTH,
                PreferredBackBufferHeight = Singleton.SCREENHEIGHT
            };
            Window.Title = "TALES WAR";
            IsMouseVisible = true;

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }


        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("font/File");

            song = Content.Load<Song>("sounds/bgm_1");
            MediaPlayer.Volume = Singleton.Instance.MasterBGMVolume;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_screenManager = new GameScreenManager(m_spriteBatch, Content);


            m_screenManager.ChangeScreen(new MenuScreen(m_screenManager));

            m_screenManager.OnGameExit += Exit;
        }


        protected override void UnloadContent()
        {
            if (m_screenManager != null)
            {
                m_screenManager.Dispose();

                m_screenManager = null;
            }
        }


        protected override void Update(GameTime gameTime)
        {

            m_screenManager.ChangeBetweenScreen();

            m_screenManager.HandleInput(gameTime);

            m_screenManager.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            m_screenManager.Draw(gameTime);

            graphics.BeginDraw();

            base.Draw(gameTime);
        }
    }
}
