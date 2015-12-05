using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Rubber_Duck_Debugging
{
    public sealed partial class MainPage : Page
    {
        public DuckMode Mode { get; set; }
        public int NumberOfProblemsSolved { get; set; }
        public TimeSpan TotalTimeUsed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            Mode = DuckMode.Resting;
            var storedData = StoredDataHandler.GetData().Result;
            NumberOfProblemsSolved = int.Parse(storedData.NumberOfProblems);
            TotalTimeUsed = TimeSpan.Parse(storedData.TotalTimeUsed);
            SetHeader();
            SetButtonText();
        }

        private void OnOffButtonClick(object sender, RoutedEventArgs e)
        {
            ToggleMode();
        }

        private void ToggleMode()
        {
            if (Mode == DuckMode.Listening)
            {
                EndTime = DateTime.Now;
                TotalTimeUsed = TotalTimeUsed + (EndTime - StartTime);
                NumberOfProblemsSolved++;
                StoredDataHandler.WriteData(NumberOfProblemsSolved.ToString(), TotalTimeUsed.ToString());
                Mode = DuckMode.Resting;
            }
            else
            {
                Mode = DuckMode.Listening;
                StartTime = DateTime.Now;
            }

            SetHeader();
            SetButtonText();
        }

        private void SetButtonText()
        {
            OnOffButton.Content = Mode == DuckMode.Listening
                ? "Thank you!"
                : "Hey, have you got a sec?";
        }

        private void SetHeader()
        {
            HeaderText.Text = Mode == DuckMode.Listening
                ? "Now, Explain to me... What are you trying to do?"
                : "Zzzzzzzz";
        }

        private void Info_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (HeaderText.Text.StartsWith("I have helped you"))
                SetHeader();
            else
                HeaderText.Text =
                    string.Format(
                        "I have helped you solve {0} problems, while listening to your problems for {1}.",
                        NumberOfProblemsSolved, GetMinutes());
        }

        private string GetMinutes()
        {
            if (TotalTimeUsed.TotalMinutes < 1)
                return "less than one minute";
            if (Math.Round(TotalTimeUsed.TotalMinutes, 0) == 1)
                return "one minute";

            return Math.Round(TotalTimeUsed.TotalMinutes, 0).ToString(CultureInfo.InvariantCulture) + " minutes";
        }
    }
}
