
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GodOfDuty
{
    class GameObject : ICloneable
    {


        protected InputComponent _input;
        protected PhysicComponent _physics;
        protected GraphicComponent _graphics;
        protected SkillComponent _skills;


        public Texture2D _hit;

        public int attack;
        public int HP;
        public int status;
        public int skill = 0;

        public bool InTurn;
        public bool action;
        public bool shooting = false;
        public bool IsHit = false;



        #region PUBLIC_VARIABLES

        public Dictionary<string, SoundEffectInstance> SoundEffects;
        public Vector2 Position;

        public float Rotation;

        public float LinearVelocity = 1.3f;
        public float force = 1f;
        public float gravity = 1f;

        public Vector2 Scale;
        public Vector2 Direction;
        public Vector2 Origin;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public string Name;
        public string Weapon;
        public bool IsActive = true;


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - Viewport.Width / 2 * Scale.X), (int)(Position.Y - Viewport.Height / 2 * Scale.Y)
                                           , (int)(Viewport.Width * Scale.X), (int)(Viewport.Height * Scale.Y));
            }
        }
        public Rectangle Viewport;
        #endregion



        public GameObject(InputComponent input, PhysicComponent physics, GraphicComponent graphics, SkillComponent skill)
        {
            _input = input;
            _physics = physics;
            _graphics = graphics;
            _skills = skill;

            Position = Vector2.Zero;
            Scale = Vector2.One;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0f;
            IsActive = true;
            Origin = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            if (_input != null) _input.Update(gameTime, gameObjects, this);
            if (_skills != null && HP > 0) _skills.Update(gameTime, gameObjects, this);
            if (_physics != null) _physics.Update(gameTime, gameObjects, this);
            if (_graphics != null) _graphics.Update(gameTime, gameObjects, this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_graphics != null) _graphics.Draw(spriteBatch, this);
            //if(_hit != null) spriteBatch.Draw(_hit, Rectangle, Color.Red);
            //check hit box
        }

        public void SendMessage(Component sender, int message)
        {
            if (_input != null) _input.ReceiveMessage(message, sender);
            if (_skills != null) _skills.ReceiveMessage(message, sender);
            if (_physics != null) _physics.ReceiveMessage(message, sender);
            if (_graphics != null) _graphics.ReceiveMessage(message, sender);
        }

        public virtual void Reset()
        {
            if (_input != null) _input.Reset();
            if (_physics != null) _physics.Reset();
            if (_graphics != null) _graphics.Reset();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }




    }
}
