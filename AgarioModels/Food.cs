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
/// This file represents a food object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    /// <summary>
    /// Used to define a Food object
    /// </summary>
    public class Food : GameObject
    {
        /// <summary>
        /// ID for a food object.s
        /// </summary>
        public long ID { get; set; }
    }

}
