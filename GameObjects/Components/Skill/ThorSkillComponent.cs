using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class ThorSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;
        int barier;
        int count;
        int _attack = 1;

        public ThorSkillComponent()
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
                if (barier > 0)
                    barier -= 1;
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }

            parent.attack = _attack;
            rng = rnd.Next(1, 11);
            count += 1;
            if ( parent.InTurn && parent.status != 1 && count == 1) {
                switch (Singleton.Instance.level_sk3)
                {
                    case 1:
                        if (rng >= 10)
                        {

                            _attack++;

                            parent.SendMessage(this, 3);
                        }
                      
                        break;
                    case 2:
                        if (rng >= 9)
                        {

                            _attack++;

                            parent.SendMessage(this, 3);
                        }
                     
                        break;
                    case 3:
                        if (rng >= 8)
                        {

                            _attack++;

                            parent.SendMessage(this, 3);
                        }
                      
                        break;
                }
            }
           
            if (!parent.InTurn)
            {
                count = 0;
            }

            if (parent.skill == 1)
            {
                parent.status = 3;
                switch (Singleton.Instance.level_sk1)
                {
                    case 1:
                        barier = 1;
                        break;
                    case 2:
                        barier = 2;
                        break;
                    case 3:
                        barier = 3;
                        break;
                }
                parent.skill = 0;
            }

            if(parent.skill == 2)
            {
                switch (Singleton.Instance.level_sk2)
                {
                    case 1:
                        parent.SendMessage(this, 204);

                        break;
                    case 2:
                        parent.SendMessage(this, 205);

                        break;
                    case 3:
                        parent.SendMessage(this, 206);

                        break;
                }

                parent.skill = 0;
            }

            if(barier <= 0)
            {
                parent.status = 0;
            }


            

            base.Update(gameTime, gameObjects, parent);
        }
    }
}
