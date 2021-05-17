using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class SkillComponent : Component
    {
        public int _skill;

        public SkillComponent()
        {
            id = 4;
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
        }

        public override void ReceiveMessage(int message, Component sender)
        {
            if (sender.id == 2)
            {
                _skill = message;
            }

            base.ReceiveMessage(message, sender);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
                

            if (parent.status == 2 && parent.InTurn)
            {
                parent.HP -= 1;
                parent.SendMessage(this, 4);
                parent.status = 0;
            }



            base.Update(gameTime, gameObjects, parent);
        }
    }
}
