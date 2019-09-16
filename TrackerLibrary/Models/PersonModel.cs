using System;
using System.ComponentModel;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one person
    /// </summary>
    public class PersonModel: IDataErrorInfo
    {
        /// <summary>
        /// The unique identifier for the person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first name of the person.
        /// </summary>
        public string FirstName { get; set; } = "";

        /// <summary>
        /// The last name of the person.
        /// </summary>
        public string LastName { get; set; } = "";

        /// <summary>
        /// The primary email address of the person.
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// The primary cellphone number of the person.
        /// </summary>
        public string CellphoneNumber { get; set; } = "";

        public string FullName 
        {
            get {
                return $"{ FirstName } { LastName }";
            }
        }


        /// <summary>
        /// Validation of each property against business-rules.
        /// </summary>
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

        public PersonModel() { }

        public PersonModel(string firstName, string lastName, string email, string cellphoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CellphoneNumber = cellphoneNumber;
        }
    }
}