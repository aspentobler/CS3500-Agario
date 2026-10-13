/// <summary>
/// Author:    Joshua Elieson and Aspen Tobler
/// Date:      1-April-2024 todo
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
/// This file represents the chat client Maui program that uses the main page code.
/// </summary>
using Logger;
using Microsoft.Extensions.Logging;

namespace ClientGUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddDebug();
                loggingBuilder.AddProvider(new CustomFileLoggerProvider());
            }).AddTransient<MainPage>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<MainPage>();
            return builder.Build();
        }
    }
}
