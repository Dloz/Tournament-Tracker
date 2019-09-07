using System;
using System.ComponentModel;

namespace TrackerLibrary
{
    /// <summary>
    /// Represents what prize is for the given place
    /// </summary>
    public class PrizeModel: IDataErrorInfo
    {
        /// <summary>
        /// The unique identifier for the prize.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The numberic identifier for the place(2 for secand, etc.)
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// The friendly name for the place(second place, etc.)
        /// </summary>
        public string PlaceName { get; set; } = "";
        /// <summary>
        /// The fix amount this place earns or zero if it is not used.
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// The number represents the percentage of overall take or
        /// zero if it is not used. The percentage is a fraction of 1 (so 0.5 for 
        /// 50%).
        /// </summary>
        public double PrizePercentage { get; set; }

        public string this[string columnName] {
            get {
                string error = String.Empty;
                switch (columnName)
                {
                    case "PlaceNumber":
                        if (PlaceNumber < 0)
                        {
                            error = "Place number can not be less than 0";
                        }
                        else if (PlaceNumber == 0)
                        {
                            error = "Place number can not be equal to 0";
                        }
                        break;
                    case "PlaceName":
                        if (PlaceName.Length == 0)
                        {
                            error = "Please enter the place name";
                        }
                        break;
                    case "PrizeAmount":
                        if (PrizeAmount < 0)
                        {
                            error = "The prize amount should not be less than 0";
                        }
                        break;
                    case "PrizePercentage":
                        if (PrizePercentage < 0)
                        {
                            error = "The prize percentage can not be less than 0";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }

        public PrizeModel() { }


        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            PlaceNumber = int.Parse(placeNumber);
            PlaceName = placeName;
            PrizeAmount = decimal.Parse(prizeAmount);
            PrizePercentage = double.Parse(prizePercentage);

            if (PrizeAmount != 0 && PrizePercentage != 0.0)
            {
                PrizePercentage = 0d;
            }
        }
    }
}