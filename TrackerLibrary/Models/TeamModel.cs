using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one team in the tournament.
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// The list of team members.
        /// </summary>
        List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        /// <summary>
        /// The name of the team.
        /// </summary>
        public string TeamName { get; set; }

        public TeamModel()
        {

        }
    }
}
