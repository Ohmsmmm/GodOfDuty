using Microsoft.Xna.Framework;
using System;

namespace Final_Assignment
{
    class Camera
    {

        public Vector2 CameraPosition;

        public bool shake;

        public Matrix Transform { get; private set; }


        public void Follow(GameObject target)
        {
            Random rnd = new Random();
            CameraPosition = target.Position;
            if (target.Position.X > 4000 - Singleton.SCREENWIDTH / 2 - 20)
                CameraPosition.X = 4000 - Singleton.SCREENWIDTH / 2 - 20;

            if (target.Position.X < Singleton.SCREENWIDTH / 2)
                CameraPosition.X = Singleton.SCREENWIDTH / 2;

            if (target.Position.Y < (Singleton.SCREENHEIGHT - 1000) / 2)
                CameraPosition.Y = (Singleton.SCREENHEIGHT - 1000) / 2;

            if (target.Position.Y > 1000 - Singleton.SCREENHEIGHT / 2 - 20)
                CameraPosition.Y = 1000 - Singleton.SCREENHEIGHT / 2 - 20;

            var position = Matrix.CreateTranslation(
             -CameraPosition.X - (target.Rectangle.Width / 2),
             -CameraPosition.Y - (target.Rectangle.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                    Singleton.SCREENWIDTH / 2,
                   Singleton.SCREENHEIGHT / 2,
                   0);

            if (shake)
            {
                offset = Matrix.CreateTranslation(
                              rnd.Next((Singleton.SCREENWIDTH / 2) - 3, (Singleton.SCREENWIDTH / 2) + 3),
                              rnd.Next((Singleton.SCREENHEIGHT / 2) - 3, (Singleton.SCREENHEIGHT / 2) + 3),
                                0);
            }


            Transform = position * offset;
        }






    }
}
