/// <summary>
/// Author:    Aspen Tobler and Joshua Elieson
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
/// This file represents the logger wrapper class for our Agario game.
/// </summary>

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    /// <summary>
    /// This is a wrapper class that inherits from the ILoggerProvider interface and makes use
    /// of the CustomFileLogger class.
    /// </summary>
    public class CustomFileLoggerProvider : ILoggerProvider
    {
        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomFileLogger(categoryName);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // This method is not needed because our CustomFileLogger utilizes the File.AppendAllText()
            // method,which handles opening and closing the specified file.
        }
    }
}
