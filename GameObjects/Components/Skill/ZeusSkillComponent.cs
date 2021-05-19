using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class ZeusSkillComponent:SkillComponent
    {

        Random rnd = new Random();
        int rng;

        public ZeusSkillComponent()
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



            if (parent.IsHit && parent.status != 1)
            {

                rng = rnd.Next(1, 11);
                switch (Singleton.Instance.level_sk3)
                {
                    case 1:
                        if (rng >= 8)
                        {
                            parent.HP += 2; //30%
                            parent.SendMessage(this, 3);
                        }
                        else parent.SendMessage(this, 4);
                        break;
                    case 2:
                        if (rng >= 7)
                        {
                            parent.HP += 2; //40%
                            parent.SendMessage(this, 3);
                        }
                        else parent.SendMessage(this, 4);
                        break;
                    case 3:
                        if (rng >= 6)
                        {
                            parent.HP += 2; //50%
                            parent.SendMessage(this, 3);
                        }
                        else parent.SendMessage(this, 4);
                        break;
                }
                parent.IsHit = false;
            }

            if (parent.skill == 1)
            {

                parent.SendMessage(this, 201);
                switch (Singleton.Instance.level_sk1)
                {
                    case 1:
                        parent.attack = 2;
                        break;
                    case 2:
                        parent.attack = 3;
                        break;
                    case 3:
                        parent.attack = 4;
                        break;
                }
                parent.skill = 0;

            }

            if (parent.skill == 2)
            {
               
                switch (Singleton.Instance.level_sk2)
                {
                    case 1:
                        parent.attack = 2;
                        parent.SendMessage(this, 299);
                        break;
                    case 2:
                        parent.attack = 4;
                        parent.SendMessage(this, 299);
                        break;
                    case 3:
                        parent.attack = 6;
                        parent.SendMessage(this, 299);
                        break;
                }
                parent.skill = 0;
            }

            if (parent.IsHit)
            {
                parent.SendMessage(this, 4);
                parent.IsHit = false;
            }

            if (!parent.InTurn)
            {
                parent.attack = 1;
            }


            base.Update(gameTime, gameObjects, parent);
        }
    }
}
