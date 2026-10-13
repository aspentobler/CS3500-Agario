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
/// This file represents the logger for our Agario game.
/// </summary>

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    /// <summary>
    /// This is a custom file logger class that implements from the interface
    /// ILogger. It logs data to a file along with the date and time, specific
    /// thread, etc.
    /// </summary>
    public class CustomFileLogger : ILogger
    {
        /// <summary>
        /// Name of file.
        /// </summary>
        string _FileName;

        /// <summary>
        /// Key used to lock logger.
        /// </summary>
        object key = new object();

        /// <summary>
        /// Constructor that creates a directory for the given file name.
        /// </summary>
        public CustomFileLogger(string categoryName)
        {
            _FileName = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData)
                + Path.DirectorySeparatorChar
                + $"CS3500-{categoryName}.log";
        }

        /// <summary>
        /// Begins a "logical operation scope" that gives the log more
        /// information about what part of the code is being logged.
        /// This gives additional context, making it easier to search
        /// and correlate log statements with what pieces of code are
        /// being logged.
        /// </summary>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (key)
            {
                File.AppendAllText(_FileName, $"{DateTime.Now} ({Thread.CurrentThread.ManagedThreadId}) - {logLevel} - {formatter(state, exception)} {Environment.NewLine}");
            }
        }
    }
}
