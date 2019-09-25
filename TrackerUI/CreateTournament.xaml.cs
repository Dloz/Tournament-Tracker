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
    public partial class CreateTournament : Window, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = new List<TeamModel>();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournament()
        {
            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            InitializeComponent();
            availableTeams = GlobalConfig.Connection.GetTeam_All();
            WireUpLists();
        }

        private void WireUpLists()
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

                WireUpLists();
            }
        }

        private void DeleteSelectedPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            var team = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (team != null)
            {
                availableTeams.Add(team);
                selectedTeams.Remove(team);

                WireUpLists();
            }
        }

        private void CreatePrizeButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the CreatePrize window.
            CreatePrize prizeWin = new CreatePrize(this);
            prizeWin.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            // Get the PrizeModel from the window
            // Add PrizeModel to the selected list.
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void CreateNewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            var teamWin = new CreateTeam(this);
            teamWin.Show();
        }

        private void RemoveSelectedTeamButton_Click(object sender, RoutedEventArgs e)
        {
            TeamModel p = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (p == null) return;

            availableTeams.Add(p);
            selectedTeams.Remove(p);

            WireUpLists();
        }

        private void RemoveSelectedPrizeButton_Click(object sender, RoutedEventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p == null) return;

            selectedPrizes.Remove(p);

            WireUpLists();
        }

        private void CreateTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);
            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid Entry Fee", "Invali fee", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            // Create tournament model
            var tm = new TournamentModel();

            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // Wire up matchups
            TournamentLogic.CreateRounds(tm);

            // Create tournament entry
            // Create all of the prize entries
            // Create all of the team entries
            GlobalConfig.Connection.CreateTournament(tm);
        }
    }
}
