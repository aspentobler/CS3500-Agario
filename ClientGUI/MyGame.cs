/// <summary>
/// Author:    Joshua Elieson and Aspen Tobler
/// Date:      21-April-2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500, Aspen Tobler, and Joshua Elieson - This work may not 
///            be copied for use in Academic Coursework.
///
/// I, Aspen Tobler and Joshua Elieson, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
/// This file represents the Drawable surface. It inherits from IDrawable and draws the players,
/// food, and world based on information sent from the server.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgarioModels;
using Microsoft.Extensions.Logging;

namespace ClientGUI
{
    /// <summary>
    /// This class defines a MyGame object, to be used in our agario project as the IDrawable
    /// </summary>
    public class MyGame : IDrawable
    {
        /// <summary>
        /// Instance of a world.
        /// </summary>
        public World world;

        /// <summary>
        /// Width of the client's screen.
        /// </summary>
        private readonly float width = 800;

        /// <summary>
        /// Height of the client's screen.
        /// </summary>
        private readonly float height = 800;

        /// <summary>
        /// Constructor for a MyGame object
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="gv"></param>
        /// <param name="world"></param>
        public MyGame(World world)
        {
            this.world = world;
        }

        /// <summary>
        /// Draws the world.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="dirtyRect"></param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.LightBlue;
            canvas.FillRectangle(0, 0, width, height);
            canvas.FillColor = Colors.Pink;
            DrawFoods(canvas);
            DrawPlayers(canvas);
        }

        /// <summary>
        /// Draw all the players that are currently in the world.
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawPlayers(ICanvas canvas)
        {
            foreach (var player in world.Players)
            {
                ConvertFromWorldToScreen(player.Value.X, player.Value.Y, player.Value.Radius,
                    out float screenX, out float screenY, out float screenRadius);
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 4;

                canvas.FillColor = Color.FromArgb("#" + player.Value.ARGBColor.ToString("X8"));
                canvas.FillCircle(screenX, screenY, screenRadius);
                canvas.DrawString(player.Value.Name, screenX, (float)(screenY - (player.Value.Radius * 3)), HorizontalAlignment.Center);
            }
        }

        /// <summary>
        /// Draw all the food objects that are currently in the world.
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawFoods(ICanvas canvas)
        {
            foreach (var food in world.Foods)
            {
                {
                    ConvertFromWorldToScreen(food.Value.X, food.Value.Y, food.Value.Radius,
                    out float screenX, out float screenY, out float screenRadius);
                    canvas.StrokeColor = Colors.Black;
                    canvas.StrokeSize = 2;
                    canvas.FillColor = Color.FromArgb("#" + food.Value.ARGBColor.ToString("X8"));
                    canvas.FillCircle(screenX, screenY, screenRadius);
                }
            }
        }

        /// <summary>
        /// Converts the coordinates from the world to the screen.
        /// </summary>
        /// <param name="worldX"></param>
        /// <param name="worldY"></param>
        /// <param name="worldW"></param>
        /// <param name="worldH"></param>
        /// <param name="screenX"></param>
        /// <param name="screenY"></param>
        /// <param name="screenW"></param>
        /// <param name="screenH"></param>
        private void ConvertFromWorldToScreen(
                        in float worldX, in float worldY, in float worldRadius,
                        out float screenX, out float screenY, out float screenRadius)
        {
            screenX      = (worldX - world.leftEdge)   * world.scaleFactor;
            screenY      = (worldY - world.bottomEdge) * world.scaleFactor;
            screenRadius =  worldRadius                * world.scaleFactor;
        }
    }
}
