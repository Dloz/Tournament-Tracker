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
using TrackerLibrary.Models;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for CreatePrize.xaml
    /// </summary>
    public partial class CreatePrize : Window
    {
        IPrizeRequester callingWin;
        PrizeModel prize;
        public CreatePrize(IPrizeRequester caller)
        {
            GlobalConfig.InitializeConnections(true, true);
            InitializeComponent();
            prize = new PrizeModel();
            this.DataContext = prize;
            callingWin = caller;
        }

        private void CreatePrizeButton_Click(object sender, RoutedEventArgs e)
        {
            var _prize = new PrizeModel(
                placeNumber: placeNumberBox.Text,
                placeName: placeNameBox.Text,
                prizeAmount: prizeAmountBox.Text,
                prizePercentage: prizePercentageBox.Text);

            GlobalConfig.Connection.CreatePrize(_prize);

            callingWin.PrizeComplete(_prize);

            this.Close();

            //RefreshFields();
        }

        private void RefreshFields()
        {
            placeNumberBox.Text     = "";
            placeNameBox.Text       = "";
            prizeAmountBox.Text     = "";
            prizePercentageBox.Text = "";
        }
    }
}
