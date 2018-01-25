using System;
using System.Configuration;
using System.Data;
using GotDotNet.ApplicationBlocks.Data;

	/// <summary>
	/// The BaseBusiness class is the base class for all the business classes.
	/// </summary>
namespace RotmanTrading.Business
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class BaseBusiness
    {
        #region Declarations
        protected string m_conROTMAN = ConfigurationManager.AppSettings["ConnStringRotman"].ToString();
        protected AdoHelper m_dbMSSqlHelper = AdoHelper.CreateHelper("GotDotNet.ApplicationBlocks.Data", "GotDotNet.ApplicationBlocks.Data.SqlServer");
        protected AdoHelper m_dbHelper = AdoHelper.CreateHelper("GotDotNet.ApplicationBlocks.Data", "GotDotNet.ApplicationBlocks.Data.Oracle");
         
        public const int LENGTH_TEAM_CODE = 4;
        public const int LENGTH_TEAM_NAME = 64;
        public const int LENGTH_LOGIN_NAME = 50;
        public const int LENGTH_PASSWORD = 50;
        public const int LENGTH_PASSWORD_ENTRY = 25;
        public const int LENGTH_TICKET_IMAGE_FILENAME = 200;
        public const int LENGTH_SERIAL_NUMBER = 6;
        public const int LENGTH_FILE_NAME = 200;

        //Common Error Messages
        public const string ERR_MSG_PAGE_OP_MUST_BE_SPECIFIED = "A Page Operation must be specified";
        public const string ERR_MSG_PAGE_OP_IS_INVALID = "The Page Operator is invalid";
        public const string ERR_MSG_PORTAL_OUTCRY_ID_MUST_BE_SPECIFIED = "A Portal Outcry ID must be specified";
        public const string ERR_MSG_GAME_ID_MUST_BE_SPECIFIED = "A Game must be specified";
        public const string ERR_MSG_GAME_IMPLEMENATION_MUST_BE_SPECIFIED = "A Game implementation must be specified";
        public const string ERR_MSG_TEAM_ID_MUST_BE_SPECIFIED = "A Team ID must be specified";
        public const string ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED = "A Team Code must be specified";
        public const string ERR_MSG_TEAM_NAME_MUST_BE_SPECIFIED = "A Team Name must be specified";
        public const string ERR_MSG_TEAM_CODE_IS_TOO_LONG = "Team Code is too long";
        public const string ERR_MSG_TEAM_NAME_IS_TOO_LONG = "Team Name is too long";
        public const string ERR_MSG_ALGO_RANK_MUST_BE_SPECIFIED = "A rank must be specified";
        public const string ERR_MSG_RANK_MUST_BE_NUMERIC = "Rank must be numeric";
        public const string ERR_MSG_USER_ID_MUST_BE_SPECIFIED = "A User ID must be specified";
        public const string ERR_MSG_PROFIT_VALUE_MUST_BE_SPECIFIED = "Profit value must be provided";
        public const string ERR_MSG_USER_CODE_MUST_BE_SPECIFIED = "User code must be provided";
        public const string ERR_MSG_TEAM_CODE_DOES_NOT_EXIST = "Team code does not exist in database";
        public const string ERR_MSG_TEAM_CODE_ALREADY_EXISTS = "Team code already exists in the database";
        public const string ERR_MSG_GAME_ID_MUST_BE_NUMERIC = "Game ID must be numeric";
        public const string ERR_MSG_USER_CODE_MUST_BE_NUMERIC = "User code must be numeric";
        public const string ERR_MSG_GAME_ID_MUST_BE_BETW_1_AND_41 = "Game ID must be between 1 and 41";
        public const string ERR_MSG_PORTAL_SYSTEM_VALUE_MUST_BE_SPECIFIED = "A portal system value must be specified";
        public const string ERR_MSG_PORTAL_SYSTEM_VALUE_MUST_BE_NUMERIC = "The portal system value must be numeric";
        public const string ERR_MSG_ERROR_INSERTING_DATA_INTO_DB = "Error inserting data into database";
        public const string ERR_MSG_ERROR_OCCURRED = "An error occurred. Contact the system admistrator";
        public const string ERR_MSG_FINAL_SCORE_MUST_BE_SPECIFIED = "A final score must be provided";
        public const string ERR_MSG_QUANTITY_MUST_BE_SPECIFIED = "A quantity must be specified";
        public const string ERR_MSG_PRICE_MUST_BE_SPECIFIED = "A price must be specified";
        public const string ERR_MSG_SERIAL_NUMBER_MUST_BE_SPECIFIED = "A serial number must be specified";
        public const string ERR_MSG_SERIAL_NUMBER_ALREADY_EXISTS = "Serial number already exists";
        public const string ERR_MSG_SERIAL_NUMBER_IS_TOO_LONG = "Serial number is too long";
        public const string ERR_MSG_SERIAL_NUMBER_NOT_CORRECT_LENGTH = "Serial number must contain 6 digits";
        public const string ERR_MSG_BUYING_AND_SELLING_TEAMS_MUST_BE_DIFFERENT = "Buying and Selling Teams must be different";
        public const string ERR_MSG_QUANTITY_MUST_BE_NUMERIC = "Quantity must be numeric";
        public const string ERR_MSG_PRICE_MUST_BE_NUMERIC = "Price must be numeric";
        public const string ERR_MSG_SESSION_MUST_BE_SPECIFIED = "A session must be specified";
        public const string ERR_MSG_SESSION_MUST_BE_NUMERIC = "Session must be numeric";
        public const string ERR_MSG_QUANTITY_IS_TOO_LOW = "Quantity is too low";
        public const string ERR_MSG_QUANTITY_IS_TOO_HIGH = "Quantity is too high";
        public const string ERR_MSG_PRICE_IS_TOO_LOW = "Price is too low";
        public const string ERR_MSG_PRICE_IS_TOO_HIGH = "Price is too high";
        public const string ERR_MSG_VALIDATION_FAILED = "Validation failed";
        public const string ERR_MSG_NO_TEAM_FOR_USER = "User is not associated with any team";
        public const string ERR_MSG_NO_DATA_TO_DISPLAY = "No data to display";
        public const string ERR_MSG_NO_DATA_FOUND = "No data found";
        public const string ERR_MSG_TICKET_IMAGE_ID_MUST_BE_SPECIFIED = "Ticket Image ID must be specified";
        public const string ERR_MSG_TICKET_IMAGE_FILE_NAME_MUST_BE_SPECIFIED = "Ticket image file name must be specified";
        public const string ERR_MSG_TICKET_IMAGE_FILE_ALREADY_EXISTS = "Ticket image file already exists";
        public const string ERR_MSG_TICKET_IMAGE_FILE_DOES_NOT_EXIST = "Ticket image file does not exist. Please upload the file and try this operation again.";
        public const string ERR_MSG_TICKET_IMAGE_FILE_ALREADY_MATCHED = "Ticket image file has already been matched to another transaction.";

        public const string ERR_MSG_LOGIN_NAME_MUST_BE_SPECIFIED = "Login name must be provided";
        public const string ERR_MSG_PASSWORD_MUST_BE_SPECIFIED = "Password must be provided";
        public const string ERR_MSG_CONFORM_PASSWORD_MUST_BE_SPECIFIED = "Confirm password must be provided";
        public const string ERR_MSG_PASSWORDS_DO_NOT_MATCH = "Passwords do not match";
        public const string ERR_MSG_USER_STATUS_MUST_BE_SPECIFIED = "A User Status must be selected";
        public const string ERR_MSG_USER_STATUS_INVALID = "User Status is invalid";
        public const string ERR_MSG_USER_TYPE_MUST_BE_SPECIFIED = "A User Type must be selected";
        public const string ERR_MSG_LOGIN_NAME_ALREADY_EXISTS = "Login name already exists";
        public const string ERR_MSG_LOGIN_NAME_IS_TOO_LONG = "Login name is too long";
        public const string ERR_MSG_PASSWORD_IS_TOO_LONG = "Password is too long";

        public const string ERR_MSG_SERIAL_NUMBER_DOES_NOT_EXIST = "serial number does not exist.";
        public const string ERR_MSG_SERIAL_NUMBER_ALREADY_EXISTS_BUT_CONTAINS_TICKET_IMAGE = "serial number exists, BUT it already contains a ticket image id.";
        #endregion

        #region Constructors
        public BaseBusiness()
        {
        }
        #endregion

        #region Common Methods
        public static bool IsInt(string strValue)
        {
            int valueAsInt;
            if (!int.TryParse(strValue, out valueAsInt))
            {
                return false;
            }
            return true;
        }

        #endregion

    }   //end public class BaseBusiness
}