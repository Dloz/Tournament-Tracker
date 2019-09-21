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
    /// Interaction logic for CreateTeam.xaml
    /// </summary>
    public partial class CreateTeam : Window
    {
        private ITeamRequester callerWin;
        private List<PersonModel> availableTeamMembers = new List<PersonModel>();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        public CreateTeam(ITeamRequester caller)
        {
            InitializeComponent();
            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            this.DataContext = new PersonModel();
            availableTeamMembers = GlobalConfig.Connection.GetPerson_All();

            //CreateSampleData();
            callerWin = caller;

            WireUpLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Dima", LastName = "Lozovik" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Artem", LastName = "Artem" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "No", LastName = "Name" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "John", LastName = "Doe" });
        }

        private void WireUpLists()
        {
            selectTeamMemberDropDown.ItemsSource = null;

            selectTeamMemberDropDown.ItemsSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMemberPath = "FullName";

            teamMembersListBox.ItemsSource = null;

            teamMembersListBox.ItemsSource = selectedTeamMembers;
            teamMembersListBox.DisplayMemberPath = "FullName";
        }

        private void CreateMemberButton_Click(object sender, RoutedEventArgs e)
        {
            var person = new PersonModel();
            person.FirstName         = firstNameValue.Text;
            person.LastName          = lastNameValue.Text;
            person.Email             = emailValue.Text;
            person.CellphoneNumber   = cellphoneValue.Text;

            GlobalConfig.Connection.CreatePerson(person);

            selectedTeamMembers.Add(person);

            WireUpLists();

            // Refresh the fields.
            firstNameValue.Text   = "";
            lastNameValue.Text    = "";
            emailValue.Text       = "";
            cellphoneValue.Text = "";
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p == null) return;

            availableTeamMembers.Remove(p);
            selectedTeamMembers.Add(p);

            WireUpLists();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p == null) return;

            availableTeamMembers.Add(p);
            selectedTeamMembers.Remove(p);

            WireUpLists();
        }

        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            TeamModel t = new TeamModel();

            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);

            callerWin.TeamComplete(t);

            this.Close();
        }

    }
}
