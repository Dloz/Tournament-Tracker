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
    /// Interaction logic for CreateTournament.xaml
    /// </summary>
    public partial class CreateTournament : Window
    {
        List<TeamModel> availableTeams = new List<TeamModel>();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournament()
        {
            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            InitializeComponent();
            availableTeams = GlobalConfig.Connection.GetTeam_All();
            InitializeLists();
        }

        private void InitializeLists()
        {
            teamsComboBox.ItemsSource = null;
            teamsComboBox.ItemsSource = availableTeams;
            teamsComboBox.DisplayMemberPath = "TeamName";

            tournamentTeamsListBox.ItemsSource = null;
            tournamentTeamsListBox.ItemsSource = selectedTeams;
            tournamentTeamsListBox.DisplayMemberPath = "TeamName";

            prizesListBox.ItemsSource = null;
            prizesListBox.ItemsSource = selectedPrizes;
            prizesListBox.DisplayMemberPath = "PlaceName";

        }

        private void AddTeamButton_Click(object sender, RoutedEventArgs e)
        {
            var team = (TeamModel)teamsComboBox.SelectedItem;

            if (team != null)
            {
                availableTeams.Remove(team);
                selectedTeams.Add(team);

                InitializeLists();
            }
        }

        private void DeleteSelectedPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            var team = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (team != null)
            {
                availableTeams.Add(team);
                selectedTeams.Remove(team);

                InitializeLists();
            }
        }
    }
}
