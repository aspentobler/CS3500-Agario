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
/// This file represents the client GUI mimicking the game Agario.io, displaying a main player
/// as a circle that can interact with other players and eat little foods around the world.
/// Multiple people can connect to the same server, making this a multi-player game.
/// </summary>

using Communications;
using AgarioModels;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Diagnostics;
using System.Xml;
using System.Numerics;


namespace ClientGUI
{
    /// <summary>
    /// MainPage for the agario client, where the user interacts and plays the game
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Networking object allowing for a connection to a server.
        /// </summary>
        private readonly Networking server;

        /// <summary>
        /// Logging object that logs information to a file.
        /// </summary>
        private readonly ILogger<MainPage> logger;

        /// <summary>
        /// Keeps track of the X value of where the player's mouse is.
        /// </summary>
        private int cursorX;

        /// <summary>
        /// Keeps track of the Y value of where the player's mouse is.
        /// </summary>
        private int cursorY;

        /// <summary>
        /// Instance of a game world.
        /// </summary>
        private World world;

        /// <summary>
        /// Instance of a player.
        /// </summary>
        private Player mainPlayer;

        /// <summary>
        /// Size of the screen that will display when the game is ran. (800 x 800).
        /// </summary>
        private readonly float clientSize = 800;

        /// <summary>
        /// Tracks the number of frames (displays about 30 per second).
        /// </summary>
        private int frameCount;

        /// <summary>
        /// Constructor for mainpage object
        /// </summary>
        /// <param name="logger"></param>
        public MainPage(ILogger<MainPage> logger)
        {
            InitializeComponent();

            this.logger = logger;
            world = new();
            this.PlaySurface.Drawable = new MyGame(world);
            server = new(logger, ConnectedToServer, DisconnectedFromServer, MessageFromServer);

            frameCount = 0;
            IDispatcherTimer Timer = Dispatcher.CreateTimer();
            Timer.Interval = TimeSpan.FromSeconds((double)0.033333334);
            Timer.Tick += (s, e) => onFrame();
            Timer.Start();
        }

        /// <summary>
        /// called every frame (about 30 times a second)
        /// </summary>
        private void onFrame()
        {
            try
            {
                PlaySurface.Invalidate();
                frameCount++;
                FPSLabel.Text = "FPS: 30";
                FrameCounterLabel.Text = "Frame: " + frameCount;
            }
            catch (Exception ex)
            {
                logger.LogError("Error occurred while tryign to redraw the screen: " + ex.Message);
            }
        }

        /// <summary>
        /// Checks for incoming messages from the server and carries out the appropriate action.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        private void MessageFromServer(Networking channel, string message)
        {
            try
            {
                if (message.StartsWith(Protocols.CMD_Player_Object))
                {
                    logger.LogInformation("Message from server: " + Protocols.CMD_Player_Object);
                    mainPlayer = new Player(NameEntry.Text);

                    int.TryParse(message.Remove(0, Protocols.CMD_Player_Object.Length), out int tempId);

                    if (world.Players.ContainsKey(tempId))
                    {
                        mainPlayer = world.Players[tempId];

                        UpdateWorldEdges();
                        logger.LogInformation("Main player added to game: " + mainPlayer.Name);
                    }
                    PlaySurface.Invalidate();
                }
                else if (message.StartsWith(Protocols.CMD_Food))
                {
                    logger.LogInformation("Message from server: " + Protocols.CMD_Food);
                    string tempMessage = message.Remove(0, Protocols.CMD_Food.Length);
                    List<Food> tempFoodList = JsonSerializer.Deserialize<List<Food>>(tempMessage) ?? throw new Exception("bad json");

                    // add the foods sent from the server to the world
                    foreach (var food in tempFoodList)
                    {
                        if (!world.Foods.ContainsKey(food.ID))
                        {
                            world.Foods.Add(food.ID, food);
                            logger.LogDebug("Food added to world");
                        }
                    }
                    PlaySurface.Invalidate();
                    logger.LogDebug("Food added to world");
                }
                else if (message.StartsWith(Protocols.CMD_Dead_Players))
                {
                    logger.LogInformation("Message from server: " + Protocols.CMD_Dead_Players);
                    string tempMessage = message.Remove(0, Protocols.CMD_Dead_Players.Length);
                    List<int> idList = JsonSerializer.Deserialize<List<int>>(tempMessage) ?? throw new Exception("bad json");

                    // remove all the players that have died from the world
                    foreach (var id in idList)
                    {
                        if (world.Players.ContainsKey(id))
                        {
                            world.Players.Remove(id);
                        }
                    }
                    logger.LogDebug("Some players died and were removed from game.");
                }
                else if (message.StartsWith(Protocols.CMD_Eaten_Food))
                {
                    logger.LogInformation("Message from server: " + Protocols.CMD_Eaten_Food);
                    string tempMessage = message.Remove(0, Protocols.CMD_Eaten_Food.Length);
                    List<long> idList = JsonSerializer.Deserialize<List<long>>(tempMessage) ?? throw new Exception("bad json");

                    // remove all the foods that have died from the world
                    foreach (var id in idList)
                    {
                        if (world.Foods.ContainsKey(id))
                        {
                            world.Foods.Remove(id);
                        }
                    }
                    logger.LogDebug("Some food was eaten and removed from game.");
                }
                else if (message.StartsWith(Protocols.CMD_HeartBeat))
                {
                    //update all the labels
                    HeartBeatLabel.Text = "Heart Beat: " + message.Remove(0, Protocols.CMD_HeartBeat.Length);
                    if (mainPlayer != null)
                    {
                        CenteredOnLabel.Text = "Centered On: " + mainPlayer.Position.ToString();
                        MassLabel.Text = "Mass: " + mainPlayer.Mass.ToString();
                    }
                    UpdateWorldEdges();
                    server.SendAsync(String.Format(Protocols.CMD_Move, cursorX, cursorY));

                }
                else if (message.StartsWith(Protocols.CMD_Update_Players))
                {
                    logger.LogInformation("Message from server: " + Protocols.CMD_Update_Players);
                    string tempMessage = message.Remove(0, Protocols.CMD_Update_Players.Length);
                    List<Player> tempPlayerList = JsonSerializer.Deserialize<List<Player>>(tempMessage) ?? throw new Exception("bad json");

                    // add the players sent from the server to the world
                    foreach (var player in tempPlayerList)
                    {
                        if (!world.Players.ContainsKey(player.ID))
                        {
                            world.Players.Add(player.ID, player);
                        }
                        else if (mainPlayer != null && player.ID == mainPlayer.ID)
                        {
                            mainPlayer.Position = player.Position;
                            mainPlayer.Mass     = player.Mass;
                            mainPlayer.X        = player.X;
                            mainPlayer.Y        = player.Y;
                        }
                        else
                        {
                            world.Players.Remove(player.ID);
                            world.Players.Add(player.ID, player);

                        }
                    }
                    logger.LogDebug("Players list updated");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error processing  message: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the world edge to ensure when the player moves, the view does, too.
        /// </summary>
        private void UpdateWorldEdges()
        {
            if (mainPlayer != null)
            {
                world.leftEdge    = mainPlayer.X - (mainPlayer.ZoomView / 2);
                world.rightEdge   = mainPlayer.X + (mainPlayer.ZoomView / 2);
                world.topEdge     = mainPlayer.Y + (mainPlayer.ZoomView / 2);
                world.bottomEdge  = mainPlayer.Y - (mainPlayer.ZoomView / 2);
                world.scaleFactor = clientSize   /  mainPlayer.ZoomView;
            }
        }

        /// <summary>
        /// called on disconnection from the server
        /// </summary>
        /// <param name="channel"></param>
        private void DisconnectedFromServer(Networking channel)
        {
            DisplayAlert("Disconnected", "You have been disconnected from the server.", "OK");
            WelcomeScreen.IsVisible = true;
            GameScreen.IsVisible = false;
            Spinner.IsVisible = false;
            logger.LogInformation("Client was disconnected from the server.");
        }

        /// <summary>
        /// called on connection to the server
        /// </summary>
        /// <param name="channel"></param>
        private async void ConnectedToServer(Networking channel)
        {
            WelcomeScreen.IsVisible = false;
            GameScreen.IsVisible = true;
            entry.Focus();
            splitButton.IsVisible = true;
            logger.LogInformation("Client connected to the server");

            await server.SendAsync(String.Format(Protocols.CMD_Start_Game, NameEntry.Text));
        }

        /// <summary>
        /// conversion from our screen to the "actual" world/server coordinates
        /// </summary>
        /// <param name="screenX"></param>
        /// <param name="screenY"></param>
        /// <param name="worldX"></param>
        /// <param name="worldY"></param>
        private void ConvertFromScreenToWorld(
                in float screenX, in float screenY,
                out float worldX, out float worldY)
        {
            worldX = world.leftEdge   + (screenX / world.scaleFactor);
            worldY = world.bottomEdge + (screenY / world.scaleFactor);
        }

        /// <summary>
        /// Defines the button that is used to connect to the game server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ConnectToServerButton(object sender, EventArgs e)
        {
            try
            {
                Spinner.IsVisible = true;
                await server.ConnectAsync(ServerEntry.Text, 11000);
                await server.HandleIncomingDataAsync();
            }
            catch (Exception ex)
            {
                Spinner.IsVisible = false;
                await DisplayAlert("Error", "Unable to connnect to server", "OK");
                logger.LogError("Error occured trying to connect to the server: " + ex.Message);
            }
        }

        /// <summary>
        /// Used to detect the players inputted name, or if there is a name in the entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, EventArgs e)
        {
            // If the user has filled out all the fields (name, server, and port), the button will be enabled.
            if (!string.IsNullOrEmpty(NameEntry.Text) && !string.IsNullOrEmpty(ServerEntry.Text) && !string.IsNullOrEmpty(PortEntry.Text))
            {
                ConnectButton.IsEnabled = true;
            }
            else
            {
                ConnectButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Used to move the player, detect where the cursor is and where to send the player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointerChanged(object sender, PointerEventArgs e)
        {
            Point? position = e.GetPosition(PlaySurface);
            // Play Surface is the name of the graphics view to get coordinates relative to.
            if (position.HasValue)
            {
                // Get the cursor position relative to the PlaySurface
                float screenX = (float)position.Value.X;
                float screenY = (float)position.Value.Y;
                ConvertFromScreenToWorld(screenX, screenY, out float worldX, out float worldY);
                cursorX = (int)worldX;
                cursorY = (int)worldY;
            }
        }
        /// <summary>
        /// This function splits the player, called only on the split button press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Split(object sender, EventArgs e)
        {
            server.SendAsync(String.Format(Protocols.CMD_Split, cursorX, cursorY));
            logger.LogInformation("Split requested by client");
        }

        /// <summary>
        /// used to detect a spacebar press from the player in order to split
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyInput(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.EndsWith(" "))
            {
                server.SendAsync(String.Format(Protocols.CMD_Split, cursorX, cursorY));
                logger.LogInformation("Split requested by client");
            }
        }
    }
}
