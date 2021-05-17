using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace GodOfDuty
{
    class BulletFactory
    {
        public BulletFactory()
        {
            
        }

        public static GameObject create(ContentManager content,string weapon)
        {

            Texture2D _bullet = content.Load<Texture2D>("sprites/ball");
            Texture2D hit = content.Load<Texture2D>("sprites/hitbox");
            GameObject bullet = null;


            switch (weapon)
            {

                case "lance":
                    bullet = new GameObject(null,
                               new BulletPhysicComponent(content),
                               new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(0,400,100,200),1) },
                                         { "Hit", new Animation(_bullet, new Rectangle(0,0,100,200),1) },
                                         { "Skill", new Animation(_bullet, new Rectangle(0,200,100,200),1) },
                                    }),
                               null)
                    {
                        Viewport = new Rectangle(0, 0, 50, 50),
                     
                    };
                    break;

                case "bar":
                    bullet = new GameObject(null,
                               new BulletPhysicComponent(content),
                               new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(100,400,100,200),1) },
                                         { "Hit", new Animation(_bullet, new Rectangle(100,0,100,200),1) },
                                         { "Skill", new Animation(_bullet, new Rectangle(100,200,100,200),1) },
                                    }),
                               null)
                    {
                        Viewport = new Rectangle(0, 0, 50, 50),
                     
                    };
                    break;

                case "hammer":
                    bullet = new GameObject(null,
                               new BulletPhysicComponent(content),
                               new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(200,400,100,100),1) },
                                         { "Hit", new Animation(_bullet, new Rectangle(200,0,100,100),1) },
                                         { "Skill", new Animation(_bullet, new Rectangle(200,200,100,100),1) },
                                    }),
                               null)
                    {
                        Viewport = new Rectangle(0, 0, 50, 50),
                      
                    };
                    break;

                case "thunder":
                     bullet = new GameObject(null,
                                new BulletPhysicComponent(content),
                                new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(300,400,100,100),1) },
                                         { "Hit", new Animation(_bullet, new Rectangle(300,0,100,100),1) },
                                         { "Skill", new Animation(_bullet, new Rectangle(300,200,100,100),1) },
                                     }),
                                null)
                    {
                        Viewport = new Rectangle(0, 0, 50, 50),
                        
                    };
                    break;

                case "rock":
                    bullet = new GameObject(null,
                               new BulletPhysicComponent(content),
                               new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(400,400,100,100),1) },
                                         { "Hit", new Animation(_bullet, new Rectangle(400,0,100,100),1) },
                                         { "Skill", new Animation(_bullet, new Rectangle(400,200,100,100),1) },
                                    }),
                               null)
                    {
                        Viewport = new Rectangle(0, 0, 50, 50),
                       
                    };
                    break;
            }

            return bullet;
        }
    }
}
