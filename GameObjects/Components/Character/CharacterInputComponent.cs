using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodOfDuty
{
    class CharacterInputComponent : InputComponent
    {

        int message;

        int _bulletSkill;
        int count = 0;
        ContentManager content;
        Texture2D _bullet;
        SoundEffectInstance _shoot;

        float waitTime = 0;
        private bool _throw;
        public GameObject bullet;

        public CharacterInputComponent(ContentManager content)
        {
            _throw = false;
            _bullet = content.Load<Texture2D>("sprites/ball");
            _shoot = content.Load<SoundEffect>("sounds/shoot").CreateInstance();
            _shoot.Volume = Singleton.Instance.MasterSFXVolume;
            this.content = content;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            parent.Direction = new Vector2((float)Math.Cos(parent.Rotation + (float)Math.PI / 2), (float)Math.Sin(parent.Rotation + (float)Math.PI / 2));
            CheckRemove(parent);



            if (Singleton.Instance.CurrentTurnState == Singleton.TurnState.shoot)
            {
                if (parent.status == 1 && parent.InTurn)
                {
                    parent.shooting = false;
                    parent.InTurn = false;
                    count += 1;
                    Singleton.Instance.CurrentTurnState = Singleton.TurnState.enemy;
                    if (count >= 1)
                    {
                        count = 0;
                        parent.status = 0;
                    }

                }

                if (Singleton.Instance._currentkey.IsKeyDown(Keys.Space) && Singleton.Instance._currentkey != Singleton.Instance._previouskey)
                {
                    _throw = true;
                }

                if (_throw)
                {
                    parent.SendMessage(this, 2);
                    waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                    if (waitTime > 0.3)
                    {
                        Shoot(gameObjects, parent);
                        Singleton.Instance.CurrentTurnState = Singleton.TurnState.enemy;
                        _throw = false;
                        waitTime = 0;
                    }
                }
            }

            base.Update(gameTime, gameObjects, parent);

        }

        private void CheckRemove(GameObject parent)
        {

            if (bullet != null && bullet.IsActive == false)
            {
                parent.shooting = false;
                parent.InTurn = false;
                bullet = null;
            }


        }

        public void Shoot(List<GameObject> gameObjects, GameObject parent)
        {
            Singleton.Instance.Cooldown_1 -= 1;
            Singleton.Instance.Cooldown_2 -= 1;

            _shoot.Play();

            bullet = BulletFactory.create(content,parent.Weapon);

            bullet.Direction = parent.Direction;
            bullet.Position = parent.Position + new Vector2(120, -100);
            bullet.Rotation = parent.Rotation - (float)Math.PI;
            bullet.attack = parent.attack;
            bullet.force = parent.force;
            bullet.LinearVelocity = parent.LinearVelocity * 50;

            if (_bulletSkill == 201)
            {
                bullet.status = 1;
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 202)
            {
                bullet.status = 2;
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 207)
            {
                bullet.status = 3;
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 204)
            {
                bullet.Scale = new Vector2(1.25f, 1.25f);
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 205)
            {
                bullet.Scale = new Vector2(1.5f, 1.5f);
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 206)
            {
                bullet.Scale = new Vector2(2, 2);
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 299)
            {
                bullet.status = 99;
                _bulletSkill = 0;
            }

            gameObjects.Add(bullet);
            parent.shooting = true;
            Singleton.Instance.follow = bullet;
        }


        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);

            if(sender.id == 4)
            {
                _bulletSkill = message;
            }
           else
                this.message = message;
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
