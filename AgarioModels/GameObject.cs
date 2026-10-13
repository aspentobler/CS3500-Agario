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
/// This file represents a GameObject (ex. food, player).
using System.Numerics;

namespace AgarioModels
{
    /// <summary>
    /// Definition for a GameObject, including Players and Food objects
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Position of GameObject
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// X value
        /// </summary>
        public float X 
        { 
            get { return Position.X; }
            set { Position.X = value; } 
        }

        /// <summary>
        /// Y value
        /// </summary>
        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        /// <summary>
        /// Color
        /// </summary>
        public int ARGBColor { get; set; }

        /// <summary>
        /// Mass of the circle
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Radius of circle
        /// </summary>
        public float Radius => (float)Math.Sqrt(Mass / Math.PI);

        /// <summary>
        /// Determines how much to zoom out based on the player's radius.
        /// </summary>
        public float ZoomView => 20 * (Radius * 2);
    }
}
