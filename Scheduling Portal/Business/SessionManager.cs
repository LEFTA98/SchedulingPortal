using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;

namespace RotmanTrading.Business
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class SessionManager
    {
        #region Declarations
        private const string SESSION_USER_TYPE_ID = "UserTypeID";
        private const string SESSION_USER_ID = "UserID";
        private const string SESSION_USER_SCHOOLNAME = "SchoolName";
        private const string SESSION_ADMIN_VIEW = "AdminView";
        private const string SESSION_OUTCRY_TYPE = "OutcryType";
        private const string SESSION_RUN_OUTCRY = "RunOutcry";
        private const string SESSION_TO_HAPPEN_ROW_COUNT = "ToHappenRowCount";

        private const string SESSION_TRADE_FINAL_PRICE_1 = "TradeFinalPrice1";
        private const string SESSION_TRADE_FINAL_PRICE_2 = "TradeFinalPrice2";
        private const string SESSION_TRADE_FINAL_PRICE_3 = "TradeFinalPrice3";
        private const string SESSION_TRADE_FINAL_PRICE_4 = "TradeFinalPrice4";
        private const string SESSION_TRADE_FINAL_PRICE_5 = "TradeFinalPrice5";
        private const string SESSION_TRADE_FINAL_PRICE_6 = "TradeFinalPrice6";

        private const string SESSION_SEARCH_SERIAL_NUMBER = "SearchSerialNumber";
        private const string SESSION_SEARCH_FILENAME = "SearchFilename";
        private const string SESSION_SEARCH_TEAM_CODE = "SearchTeamCode";
        private const string SESSION_SEARCH_GAME_NUMBER = "SearchGameNumber";
        private const string SESSION_SEARCH_SERIAL_NUMBER_SELECTED = "SearchSerialNumberSelected";
        private const string SESSION_SEARCH_FILENAME_SELECTED = "SearchFilenameSelected";
        private const string SESSION_SEARCH_TEAM_CODE_GAME_NUMBER_SELECTED = "SearchTeamCodeGameNumberSelected";
        private const string SESSION_SEARCH_XACTIONS_WITH_NO_TICKETS_SELECTED = "SearchXactionsWithNoTicketsSelected";
        private const string SESSION_SEARCH_FIND_ALL_TICKETS_SELECTED = "SearchFindAllTicketsSelected";
        #endregion

        //Private constructor - no instances allowed
        private SessionManager() { }

        #region Properties
        public static HttpSessionState Session
        {
            get
            {
                HttpContext current = HttpContext.Current;
                if (current == null)
                {
                    //throw new NullReferenceException("HttpContext.Current is null");
                    current.Response.Redirect("~/Login.aspx", true);	
                }

                HttpSessionState currentState = current.Session;
                if (currentState == null)
                {
                    //throw new NullReferenceException("HttpContext.Current.Session is null");
                    current.Response.Redirect("~/Login.aspx", true);
                }
                return currentState;
            }
        }

        public static Enumerations.UserTypes UserTypeID
        {
            get
            {
                return (Enumerations.UserTypes)Session[SESSION_USER_TYPE_ID];
            }
            set
            {
                Session[SESSION_USER_TYPE_ID] = value;
            }
        }

        public static bool AdminView
        {
            get
            {
                return (bool)Session[SESSION_ADMIN_VIEW];
            }
            set
            {
                Session[SESSION_ADMIN_VIEW] = value;
            }
        }

        public static bool RunOutcry
        {
            get
            {
                return (bool)Session[SESSION_RUN_OUTCRY];
            }
            set
            {
                Session[SESSION_RUN_OUTCRY] = value;
            }
        }

        public static int UserID
        {
            get
            {
                return (int)Session[SESSION_USER_ID];
            }
            set
            {
                Session[SESSION_USER_ID] = value;
            }
        }

        public static string SchoolName
        {
            get
            {
                return (string)Session[SESSION_USER_SCHOOLNAME];
            }
            set
            {
                Session[SESSION_USER_SCHOOLNAME] = value;
            }
        }

        public static Enumerations.OutcryTypes OutcryType
        {
            get
            {
                return (Enumerations.OutcryTypes)Session[SESSION_OUTCRY_TYPE];
            }
            set
            {
                Session[SESSION_OUTCRY_TYPE] = value;
            }
        }

        public static int TradeFinalPrice1
        {
            get
            {
                return (int)Session[SESSION_TRADE_FINAL_PRICE_1];
            }
            set
            {
                Session[SESSION_TRADE_FINAL_PRICE_1] = value;
            }
        }

        public static int TradeFinalPrice2
        {
            get
            {
                return (int)Session[SESSION_TRADE_FINAL_PRICE_2];
            }
            set
            {
                Session[SESSION_TRADE_FINAL_PRICE_2] = value;
            }
        }

        public static int TradeFinalPrice3
        {
            get
            {
                return (int)Session[SESSION_TRADE_FINAL_PRICE_3];
            }
            set
            {
                Session[SESSION_TRADE_FINAL_PRICE_3] = value;
            }
        }

        public static int TradeFinalPrice4
        {
            get
            {
                return (int)Session[SESSION_TRADE_FINAL_PRICE_4];
            }
            set
            {
                Session[SESSION_TRADE_FINAL_PRICE_4] = value;
            }
        }

        public static int TradeFinalPrice5
        {
            get
            {
                return (int)Session[SESSION_TRADE_FINAL_PRICE_5];
            }
            set
            {
                Session[SESSION_TRADE_FINAL_PRICE_5] = value;
            }
        }

        public static int TradeFinalPrice6
        {
            get
            {
                return (int)Session[SESSION_TRADE_FINAL_PRICE_6];
            }
            set
            {
                Session[SESSION_TRADE_FINAL_PRICE_6] = value;
            }
        }

        public static int ToHappenRowCount
        {
            get
            {
                return (int)Session[SESSION_TO_HAPPEN_ROW_COUNT];
            }
            set
            {
                Session[SESSION_TO_HAPPEN_ROW_COUNT] = value;
            }
        }

        #endregion

        #region Properties for Search
        public static string SearchSerialNumber
        {
            get
            {
                return (string)Session[SESSION_SEARCH_SERIAL_NUMBER];
            }
            set
            {
                Session[SESSION_SEARCH_SERIAL_NUMBER] = value;
            }
        }

        public static string SearchFilename
        {
            get
            {
                return (string)Session[SESSION_SEARCH_FILENAME];
            }
            set
            {
                Session[SESSION_SEARCH_FILENAME] = value;
            }
        }

        public static string SearchTeamCode
        {
            get
            {
                return (string)Session[SESSION_SEARCH_TEAM_CODE];
            }
            set
            {
                Session[SESSION_SEARCH_TEAM_CODE] = value;
            }
        }

        public static string SearchGameNumber
        {
            get
            {
                return (string)Session[SESSION_SEARCH_GAME_NUMBER];
            }
            set
            {
                Session[SESSION_SEARCH_GAME_NUMBER] = value;
            }
        }

        public static bool SearchSerialNumberSelected
        {
            get
            {
                return (bool)Session[SESSION_SEARCH_SERIAL_NUMBER_SELECTED];
            }
            set
            {
                Session[SESSION_SEARCH_SERIAL_NUMBER_SELECTED] = value;
            }
        }

        public static bool SearchFilenameSelected
        {
            get
            {
                return (bool)Session[SESSION_SEARCH_FILENAME_SELECTED];
            }
            set
            {
                Session[SESSION_SEARCH_FILENAME_SELECTED] = value;
            }
        }

        public static bool SearchTeamCodeGameNumberSelected
        {
            get
            {
                return (bool)Session[SESSION_SEARCH_TEAM_CODE_GAME_NUMBER_SELECTED];
            }
            set
            {
                Session[SESSION_SEARCH_TEAM_CODE_GAME_NUMBER_SELECTED] = value;
            }
        }

        public static bool SearchXactionsWithNoTicketsSelected
        {
            get
            {
                return (bool)Session[SESSION_SEARCH_XACTIONS_WITH_NO_TICKETS_SELECTED];
            }
            set
            {
                Session[SESSION_SEARCH_XACTIONS_WITH_NO_TICKETS_SELECTED] = value;
            }
        }

        public static bool SearchFindAllTicketsSelected
        {
            get
            {
                return (bool)Session[SESSION_SEARCH_FIND_ALL_TICKETS_SELECTED];
            }
            set
            {
                Session[SESSION_SEARCH_FIND_ALL_TICKETS_SELECTED] = value;
            }
        }

        #endregion


    }   //public class SessionManager
}
