using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Rubber_Duck_Debugging
{
    public sealed partial class MainPage : Page
    {
        public DuckMode Mode { get; set; }
        public int NumberOfProblemsSolved { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            Mode = DuckMode.Resting;
            NumberOfProblemsSolved = int.Parse(StoredDataHandler.GetData().Result.Single().NumberOfProblems);
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
                NumberOfProblemsSolved++;
                StoredDataHandler.WriteData(NumberOfProblemsSolved.ToString());
                Mode = DuckMode.Resting;
            }
            else
            {
                Mode = DuckMode.Listening;
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
            HeaderText.Text = string.Format("I have helped you solve {0} problems", NumberOfProblemsSolved);
        }
    }
}
