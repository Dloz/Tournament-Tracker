using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Doing processing functions and calls the next window.
        /// </summary>
        /// <param name="window">Represents the window that needs some processing. </param>
        /// <param name="message">Represents the message that should apper at the Progress Bar. </param>
        public ProgressBar(Window window, string message)
        {
            this.messageLabel.Content = message;
            ProcessAsync();
            this.Close();
            window.Show();
        }

        /// <summary>
        /// Creates an illusion of processing.
        /// </summary>
        private async void ProcessAsync()
        {
            await ProgressBarFillAsync();
        }

        /// <summary>
        /// Filling out the progress bar from start to the end.
        /// </summary>
        private async Task ProgressBarFillAsync()
        {
            ProgressBar1.Value = 0d;
            for (;ProgressBar1.Value < 100;)
            {
                ProgressBar1.Value += 2;
                await Task.Delay(20);
            }
        }
    }
}
