using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Rubber_Duck_Debugging
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public DuckMode Mode { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            Mode = DuckMode.Resting;
            SetHeader();
            SetButtonText();
        }

        private void OnOffButtonClick(object sender, RoutedEventArgs e)
        {
            ToggleMode();
        }

        private void ToggleMode()
        {
            Mode = Mode == DuckMode.Resting ? DuckMode.Listening : DuckMode.Resting;
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
    }
}
