using System;
using System.ComponentModel;

namespace TrackerLibrary
{
    public class PersonModel: IDataErrorInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }

        public string this[string columnName] {
            get {
                string error = String.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if ((FirstName.Length < 0) || (FirstName.Length > 50))
                        {
                            error = "Amount of characters should be between 0 and 50.";
                        }
                        break;
                    case "LastName":
                        if ((LastName.Length < 0) || (LastName.Length > 50))
                        {
                            error = "Amount of characters should be between 0 and 50.";
                        }
                        break;
                    case "Email":
                        if (!Email.Contains("@"))
                        {
                            error = "Doesn't seems as a email address";

                        }
                        break;
                    case "CellphoneNumber":
                        if ((CellphoneNumber.Length != 8))
                        {
                            error = "Wrong cellphone number";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => throw new System.NotImplementedException();
    }
}