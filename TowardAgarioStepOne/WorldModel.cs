// todo TA says the screen is supposed to be 800 by 800. to get direction of mouse, do 400 - point where mouse is
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowardAgarioStepOne
{
    internal class WorldModel
    {
        public Vector2 CircleCenter;
        public float Radius;
        public Vector2 Direction;
        public double Width;
        public double Height;

        public WorldModel()
        {
            CircleCenter = new Vector2(100, 100);
            Radius = 10;
            Direction = new Vector2(50, 25);
            Width = 800;
            Height = 800;
        }

        public void AdvanceGameOneStep()
        {
            CircleCenter.X += Direction.X;
            CircleCenter.Y += Direction.Y;

            if (CircleCenter.X - Radius >= Width || CircleCenter.X + Radius <= 0)
            {
                Direction.X *= -1;
            }

            if (CircleCenter.Y - Radius >= Height || CircleCenter.Y + Radius <= 0)
            {
                Direction.Y *= -1;
            }
        }

    }
}
