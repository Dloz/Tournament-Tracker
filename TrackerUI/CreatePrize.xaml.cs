using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrackerLibrary;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for CreatePrize.xaml
    /// </summary>
    public partial class CreatePrize : Window
    {
        PrizeModel prize;
        public CreatePrize()
        {
            InitializeComponent();
            prize = new PrizeModel();
            this.DataContext = prize;
        }

        private void CreatePrizeButton_Click(object sender, RoutedEventArgs e)
        {
            var _prize = new PrizeModel(
                placeNumber: placeNumberBox.Text,
                placeName: placeNameBox.Text,
                prizeAmount: prizeAmountBox.Text,
                prizePercentage: prizePercentageBox.Text);
            foreach (var db in GlobalConfig.Connections)
            {
                db.CreatePrize(_prize);
            }
        }
    }
}
