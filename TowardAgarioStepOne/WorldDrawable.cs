using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace TowardAgarioStepOne
{
    internal class WorldDrawable : IDrawable
    {
        public WorldModel model;

        public WorldDrawable()
        {
            model = new WorldModel();
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Red;
            canvas.FillCircle(model.CircleCenter.X, model.CircleCenter.Y, model.Radius);
        }
    }
}
