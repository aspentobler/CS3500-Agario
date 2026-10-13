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
/// This file represents a player (a circle).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    /// <summary>
    /// Used to define a Player object
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// Identification
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor used for deserializing.
        /// </summary>
        public Player()
        {

        }

        /// <summary>
        /// Constructor that assigns the name.
        /// </summary>
        /// <param name="Name"></param>
        public Player(string Name)
        {
            this.Name = Name;
        }
    }
}
