using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace GodOfDuty
{
    class InputComponent : Component
    {

        public InputComponent()
        {
            id = 2;
        }
        public override void Update(GameTime gameTime, List<GameObject> gameObjects,
        GameObject parent)
        {

        }

        public override void ReceiveMessage(int message, Component sender)
        {

        }

        public override void Reset()
        {

        }

    }
}
