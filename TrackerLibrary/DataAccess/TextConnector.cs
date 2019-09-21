using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";

        /// <summary>
        /// Saves a new person to the database
        /// </summary>
        /// <param name="model">The person information</param>
        /// <returns>The person information, uncluding the unique identifier.</returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            throw new NotImplementedException();
        }

        // TODO - wire up the CreatePrize for text files.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text file and converts text to List<PrizeModel>
            var prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the max Id and increment by 1
            int currentId = 1;

            if (prizes.Count > 1)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            // Add new record with the new Id
            prizes.Add(model);

            // Convert prizes to the List<string> and save the List<string> to the text file.
            prizes.SaveToPrizeFile(PrizesFile);

            return model;

        }

        public TeamModel CreateTeam(TeamModel model)
        {
            throw new NotImplementedException();
        }

        public TournamentModel CreateTournament(TournamentModel model)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetPerson_All()
        {
            throw new NotImplementedException();
        }

        public List<TeamModel> GetTeam_All()
        {
            throw new NotImplementedException();
        }
    }
}
