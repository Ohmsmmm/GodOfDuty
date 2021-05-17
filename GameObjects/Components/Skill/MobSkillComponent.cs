using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GodOfDuty
{
    class MobSkillComponent:SkillComponent
    {
        public MobSkillComponent()
        {
        }

        public override void ReceiveMessage(int message, Component sender)
        {

            base.ReceiveMessage(message, sender);
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            if (parent.IsHit)
            {
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
