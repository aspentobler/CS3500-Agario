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
/// This file represents the world of the game.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AgarioModels
{
    /// <summary>
    /// used to hold information for the agario world, including the size and locations of players and food
    /// </summary>
    public class World
    {
        /// <summary>
        /// Width of the world.
        /// </summary>
        public const int Width = 5000;

        /// <summary>
        /// Height of the world.
        /// </summary>
        public const int Height = 5000;

        // From world point of view. Edges of what the cient can see of the world.
        public float leftEdge;
        public float rightEdge;
        public float topEdge;
        public float bottomEdge;

        /// <summary>
        /// Scale factor
        /// </summary>
        public float scaleFactor;

        /// <summary>
        /// Players list in game.
        /// </summary>
        public Dictionary<int, Player> Players { get; set; }

        /// <summary>
        /// Foods list in game.
        /// </summary>
        public Dictionary<long, Food> Foods { get; set; }

        /// <summary>
        /// Constructor to assign lists.
        /// </summary>
        public World()
        {
            Players = new Dictionary<int, Player>();
            Foods = new Dictionary<long, Food>();
        }
    }
}
