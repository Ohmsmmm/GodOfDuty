using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class CharacterPhysicComponent : PhysicComponent
    {

        public CharacterPhysicComponent()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
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
            if(parent.HP <= 0)
            {
                parent.Viewport = new Rectangle(0, 0, 0, 0);
            }

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
