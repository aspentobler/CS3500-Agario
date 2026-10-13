```
Author:     Joshua Elieson and Aspen Tobler
Start Date: 8-April-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  buzz101kill and JoshElieson
Repo:       https://github.com/uofu-cs3500-spring24/assignment-eight-agario-2-teamjoshaspen3
Commit Date: 21-April-2024
Solution:   Agario
Copyright:  CS 3500, Josh Elieson, and Aspen Tobler - This work may not be copied for use in Academic Coursework.
```

# Overview of the Agario functionality

This solution consists of an AgarioModels project, a Logger project, and a Client GUI. The AgarioModels project includes classes that
define any game object, including a player or food, as well as the world data and string protocols used between the server and client.

The LoggerLibrary includes a custom logger that creates a new file and logs things to the file name with the date and time, 
the current thread, and the log level. For any errors that occur, the log level is set to LogError. For anything that is small 
that happens that does not need to be logged in the information level, such as a new thread being started or a name being entered 
incorrectly from the cient, we logged it with the LogDebug. This helped us figure out errors that were occurring, while not putting 
it in our file with the LogInformation. Whenever a message is sent, a client connects or disconnects, etc. we logged this in the 
LogInformation, ensuring that it gets saved properly to a file and includes a time stamp and which thread it came from.

The Client GUI code creates a MainPage that contains all the data for the intial GUI, where a user can input their name and connect to a server,
as well as the gameplay GUI, where a user can play our version of agario. This project also includes the MyGame class, where everything to do with the
world is smoothly drawn at 30fps.

For testing, we were able to host a server locally and play the game for ourselves.

# Note

For our game, we expected drawing the scene and reaching 30 or higher fps to be a larger bottleneck, but it turned
out to be the network data. We found that because our drawings were simple and we only rendered in players and
food while they were nearby, redrawing the scene at 30 frames was no issue.

We also decided to go with the given Communications class and downloaded the Communications.dll from canvas, we 
wanted to avoid errors and this method ensured it all worked correctly. 

# Partnership

We both worked together to complete this assignment. Josh focused more heavily on the player movement and name tag 
generation, while Aspen focused more on the server/client connection and the player camera view. That said, we both
had trouble on many different parts of this assignment and worked together to debug and clean up each part.

# Time Expenditures:

1. Assignment 8: Predicted Hours: 30      Actual Hours: 40

      Breakdown:           Aspen: 20              Josh: 20
                       
      Progress:  25         
      Debugging: 10         
      Learning:   5

As we have worked on assignments mroe together and gotten more practice on larger software projects, our predictions have gotten more accurate overall.
We still often will underestimate, due to us forgetting to account for time learning new material and doing new things, but overall we have become better at
estimating the time expeditures for big projects.

# References:

We used the IDispatcherTimer documentation (https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatchertimer?view=windowsdesktop-8.0)
to learn about and understand how to user a timer to generate our frames. We also used ChatGPT (https://chat.openai.com/) to learn about the 
IDrawable and ICanvas classes.