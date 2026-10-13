using System.Diagnostics;
using System.Numerics;

namespace TowardAgarioStepOne
{
    public partial class MainPage : ContentPage
    {
        private bool initialized;
        private WorldDrawable worldDrawable = new();
        private IDispatcherTimer timer;

        public MainPage()
        {
            InitializeComponent();
            UpdateGuiLabels();
        }

        private void UpdateGuiLabels()
        {
            CircleCenterLbl.Text = $"Center: {worldDrawable.model.CircleCenter.X}, {worldDrawable.model.CircleCenter.Y}";
            DirectionLbl.Text = $"Direction: {worldDrawable.model.Direction.X}, {worldDrawable.model.Direction.Y}";
        }

        /// <summary>
        ///    Called when the window is resized.  
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Debug.WriteLine($"OnSizeAllocated {width} {height}");

            if (!initialized)
            {
                initialized = true;
                InitializeGameLogic();
            }
        }

        private void InitializeGameLogic()
        {
            this.PlaySurface.Drawable = worldDrawable;
            Window.Width = 500;
            timer = Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(60);
            timer.Tick += (s, e) => GameStep();
            timer.Start();
        }

        private void GameStep()
        {
            worldDrawable.model.AdvanceGameOneStep();
            PlaySurface.Invalidate();
            UpdateGuiLabels();
        }
    }

}
