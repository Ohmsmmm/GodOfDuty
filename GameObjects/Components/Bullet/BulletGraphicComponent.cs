using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class BulletGraphicComponent : GraphicComponent
    {
        ContentManager content;


        public BulletGraphicComponent(ContentManager content, Dictionary<string, Animation> animations) : base(animations)
        {
            this.content = content;
        }

        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            if (parent.IsHit)
            {
                _animationManager.Play(_animations["Hit"]);
            }
            else if(parent.status != 0)
            {
                _animationManager.Play(_animations["Skill"]);
            }
            else{
                _animationManager.Play(_animations["Shoot"]);
            }


            _animationManager.Update(gameTime);

            base.Update(gameTime, gameObjects, parent);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
             
            _animationManager.Draw(spriteBatch, parent.Position, parent.Rotation, parent.Scale);
        }
    }
}
