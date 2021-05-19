using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class SungSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int count;

        public SungSkillComponent()
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
            if (parent.IsHit)
            {
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }




            if (!parent.InTurn)
            {
                count = 0;
            }
            else
            {
                count += 1;
            }


            if (parent.InTurn && parent.status != 1 && count == 1)
            {
                int rand = rnd.Next(1, 4);
                if (rand > 2)
                {
                    parent.HP++;
                }
                parent.SendMessage(this, 3);
            }



            if (parent.skill == 1)
            {

                parent.SendMessage(this, 201);
                parent.skill = 0;
            }

            else if (parent.skill == 2)
            {

                parent.SendMessage(this, 206);
                parent.skill = 0;
            }




            base.Update(gameTime, gameObjects, parent);
        }
    }
}
