using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GodOfDuty
{
    class PhysicComponent : Component
    {
        public PhysicComponent()
        {
            id = 3;
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
            base.Update(gameTime, gameObjects, parent);
        }


        #region Collision
        public bool IsTouching(GameObject parent, GameObject g)
        {
            return IsTouchingLeft(parent,g) ||
                   IsTouchingTop(parent,g) ||
                   IsTouchingRight(parent,g) ||
                   IsTouchingBottom(parent,g);
        }

        public bool IsTouchingLeft(GameObject parent,GameObject g)
        {
            return parent.Rectangle.Right > g.Rectangle.Left &&
                    parent.Rectangle.Left < g.Rectangle.Left &&
                    parent.Rectangle.Bottom > g.Rectangle.Top &&
                    parent.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingRight(GameObject parent,GameObject g)
        {
            return parent.Rectangle.Right > g.Rectangle.Right &&
                    parent.Rectangle.Left < g.Rectangle.Right &&
                    parent.Rectangle.Bottom > g.Rectangle.Top &&
                    parent.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingTop(GameObject parent,GameObject g)
        {
            return parent.Rectangle.Right > g.Rectangle.Left &&
                    parent.Rectangle.Left < g.Rectangle.Right &&
                    parent.Rectangle.Bottom > g.Rectangle.Top &&
                    parent.Rectangle.Top < g.Rectangle.Top;
        }

        public bool IsTouchingBottom(GameObject parent,GameObject g)
        {
            return parent.Rectangle.Right > g.Rectangle.Left &&
                    parent.Rectangle.Left < g.Rectangle.Right &&
                    parent.Rectangle.Bottom > g.Rectangle.Bottom &&
                    parent.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion
    }
}
