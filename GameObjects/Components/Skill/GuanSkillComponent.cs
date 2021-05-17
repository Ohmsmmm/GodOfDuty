
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class GuanSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;




        public GuanSkillComponent()
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {


            if (parent.IsHit)
            {
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }


            rng = rnd.Next(1, 11);

            if (rng >= 8 && !parent.InTurn && !parent.action && parent.status!= 1)
            {

                
                parent.InTurn = true;

                parent.SendMessage(this, 3);
            }



            if(parent.skill == 1)
            {

                parent.SendMessage(this, 202);
                parent.skill = 0;
            }


            else if(parent.skill == 2)
            {

                parent.attack += 1;
                parent.SendMessage(this, 3);
                parent.skill = 0;
            }
                

            base.Update(gameTime, gameObjects, parent);
        }

        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
        }
    }
}
