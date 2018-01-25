using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using GotDotNet.ApplicationBlocks.Data;
using RotmanTrading.Business;

namespace RotmanTrading.Business
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class MSSqlDataAccess : BaseBusiness
    {
        #region Declarations
        private const string SP_GET_SCORES = "GetScores";
        private const string SP_GET_SOCIAL_OUTCRY_SCORES = "GetSocialOutcryScores";
        private const string SP_GET_HEAT_DATA = "GetHeatData";
        private const string SP_GET_OPEN_OUTCRY = "GetOpenOutcry";
        private const string SP_GET_QUANT_OUTCRY = "GetQuantOutcry";
        private const string SP_GET_BP_COMMODITY_DETAILS = "GetBPCommodityDetails";
        private const string SP_GET_S_AND_T_DETAILS = "GetSandTDetails";
        private const string SP_GET_CREDIT_RISK_DETAILS = "GetCreditRiskDetails";
        private const string SP_GET_THOMSON_REUTERS_QED_DETAILS = "GetThomsonReutersQEDDetails";
        private const string SP_DELETE_GAME = "DeleteGame";
        private const string SP_DELETE_SOCIAL_OUTCRY_SCORES = "DeleteSocialOutcryScores";
        private const string SP_IS_TEAM_CODE_EXIST = "IsTeamCodeExist";
        private const string SP_INSERT_PORTAL_SCORE = "InsertPortalScore";
        private const string SP_INSERT_SOCIAL_OUTCRY_SCORE = "InsertSocialOutcryScore";
        private const string SP_INSERT_ALGO_RANKING = "InsertAlgoRanking";
        private const string SP_PROCESS_GAME_INPUT = "ProcessGameInput";
        private const string SP_GENERATE_ALGO_SCORES = "GenerateAlgoScores";
        private const string SP_GET_PORTAL_SYSTEM_VALUE = "GetPortalSystemValue";
        private const string SP_UPDATE_PORTAL_SYSTEM_VALUE = "UpdatePortalSystemValue";
        private const string SP_CLEAR_TICKET = "ClearTicket";
        private const string SP_GET_OUTCRY_POSITIONS = "GetOutcryPositions";
        private const string SP_UPDATE_FINAL_SCORING = "UpdateFinalScoring";
        private const string SP_UPDATE_OUTCRY_SCORING = "UpdateOutcryScoring";

        private const string SP_CHECK_LOGIN = "CheckLogin";

        private const string SP_INSERT_TRADE = "InsertTrade";
        private const string SP_FILL_TEAM_DROPDOWN = "FillTeamDropdown";
        private const string SP_CLEAR_TRADES = "ClearTrades";
        private const string SP_GET_TRADES = "GetTrades";
        private const string SP_GET_TRADE_FINAL_SCORES = "GetTradeFinalScores";

        private const string SP_INSERT_TICKET = "InsertTicket";
        private const string SP_UPDATE_TICKET = "UpdateTicket";
        private const string SP_DELETE_TICKET = "DeleteTicket";
        private const string SP_GET_TICKET = "GetTicket";
        private const string SP_GET_RECENT_TICKETS = "GetRecentTickets";
        private const string SP_IS_SERIAL_NUMBER_EXIST = "IsSerialNumberExist";
        private const string SP_GET_TEAMS = "GetTeams";
        private const string SP_GET_OUTCRY_TRANSACTIONS = "GetOutcryTransactions";
        private const string SP_GET_TEAM_FOR_USER = "GetTeamForUser";
        private const string SP_GET_TICKET_IMAGE = "GetTicketImage";
        private const string SP_GET_ALL_TICKET_IMAGES_FOR_GAME = "GetAllTicketImagesForGame";
        private const string SP_IS_TICKET_IMAGE_FILE_EXIST = "IsTicketImageFileExist";
        private const string SP_IS_TICKET_IMAGE_FILE_BEEN_MATCHED = "IsTicketImageBeenMatched";

        private const string SP_GET_STOCK_CHART_DATA = "GetStockChartData";
        private const string SP_GET_TO_HAPPEN_ROW_COUNT = "GetToHappenRowCount";

        private const string SP_GET_NEWS = "GetNews";
        private const string SP_GET_NEWS_DETAIL = "GetNewsDetail";

        private const string SP_UPDATE_REFRESHER = "UpdateRefresher";
        private const string SP_RESET_OUTCRY = "ResetOutcry";

        private const string SP_INSERT_RAW_DATA = "InsertRawData";
        private const string SP_GET_RAW_DATA = "GetRawData";
        private const string SP_GET_DELETE_RAW_DATA = "DeleteRawData";

        private const string SP_GET_TEAM = "GetTeam";
        private const string SP_INSERT_TEAM = "InsertTeam";
        private const string SP_UPDATE_TEAM = "UpdateTeam";
        private const string SP_DELETE_TEAM = "DeleteTeam";
        private const string SP_DELETE_ALL_TEAMS = "DeleteAllTeams";

        private const string SP_GET_USER = "GetUser";
        private const string SP_GET_USERS = "GetUsers";
        private const string SP_INSERT_USER = "InsertUser";
        private const string SP_UPDATE_USER = "UpdateUser";
        private const string SP_DELETE_USER = "DeleteUser";
        private const string SP_DELETE_ALL_USERS = "DeleteAllUsers";
        private const string SP_GET_USER_TYPES = "GetUserTypes";
        private const string SP_GET_USER_STATUSES = "GetUserStatuses";
        private const string SP_IS_USER_LOGIN_EXIST = "IsUserLoginExist";

        private const string SP_FIND_SERIAL_NUMBER = "FindSerialNumber";
        private const string SP_FIND_TICKET_IMAGE_FILE = "FindTicketImageFile";
        private const string SP_FIND_TRANSACTIONS_BY_TEAM_AND_GAME = "FindTransactionsByTeamAndGame";
        private const string SP_FIND_TRANSACTIONS_WITH_NO_TICKETS = "FindTransactionsWithNoTickets";
        private const string SP_FIND_ALL_TICKETS = "FindAllTickets";

        private const string SP_GET_UNMATCHED_TICKETS = "GetUnmatchedTickets";

        private const string SP_INSERT_TICKET_IMAGE_IMAGE = "InsertTicketImage";

        private const string CHECK_VIEW_SETTINGS = "CheckViewSettings";
        private const string UPDATE_VIEW_SETTINGS = "UpdateViewSettings";

        #endregion

        #region Constructors
        public MSSqlDataAccess()
        {
        }

        #endregion

        #region Transaction Support
        private IDbTransaction m_objTransaction = null;
		private bool m_bTransactionSupport = false;
		private Transaction m_objXaction = null;

        /// <summary>
        /// Property: TransactionRef
        /// Property that sets/gets the transaction object.
        /// </summary>
        public IDbTransaction TransactionRef
        {
            set
            {
                this.m_objTransaction = value;
            }
            get
            {
                return this.m_objTransaction;
            }
        }

        /// <summary>
        /// Property: TransactionSupport
        /// Property that sets/gets whether transaction support is required.
        /// </summary>
        public bool TransactionSupport
        {
            set
            {
                m_bTransactionSupport = value;
            }
            get
            {
                return m_bTransactionSupport;
            }
        }

        /// <summary>
        /// Method: TransactionEnable
        /// Enable transaction support.
        /// </summary>
        public void TransactionEnable()
        {
            //Enable transaction support
            m_objXaction = new Transaction(base.m_conROTMAN, m_dbMSSqlHelper);
            this.TransactionSupport = true;
        }	//TransactionEnable

        /// <summary>
        /// Method: TransactionBegin
        /// Call this method to start transaction support.
        /// </summary>
        public void TransactionBegin()
        {
            if (m_objXaction != null)
            {
                this.TransactionRef = m_objXaction.TransactionBegin();
            }
        }	//TransactionBegin

        /// <summary>
        /// Method: TransactionCommit
        /// Call this method to commit the transaction.
        /// </summary>
        public void TransactionCommit()
        {
            if (m_objXaction != null)
            {
                m_objXaction.TransactionCommit();
                this.TransactionSupport = false;
            }
        }	//TransactionCommit

        /// <summary>
        /// Method: TransactionRollback
        /// Call this method to rollback the transaction.
        /// </summary>
        public void TransactionRollback()
        {
            if (m_objXaction != null)
            {
                m_objXaction.TransactionRollback();
                this.TransactionSupport = false;
            }
        }	//TransactionRollback
        #endregion

        #region Properties
        private DataTable m_dtScoringData = null;
        private DataTable m_dtStockChartData = null;
        private DataTable m_dtUserData = null;
        private DataTable m_dtDropdownData = null;

        public DataTable ScoringDataDT
        {
            set
            {
                m_dtScoringData = value;
            }
            get
            {
                return m_dtScoringData;
            }
        }

        public DataTable UserDataDT
        {
            set
            {
                m_dtUserData = value;
            }
            get
            {
                return m_dtUserData;
            }
        }

        public DataTable StockChartDataDT
        {
            set
            {
                m_dtStockChartData = value;
            }
            get
            {
                return m_dtStockChartData;
            }
        }
        public DataTable DropdownDataDT
        {
            set
            {
                m_dtDropdownData = value;
            }
            get
            {
                return m_dtDropdownData;
            }
        }
        #endregion

        #region Support
      /*  public string ConvertPortalSystemType(Enumerations.PortalSystemTypes iTypeID)
        {
            string strPortalSystemType = string.Empty;

            switch (iTypeID)
            {
                case Enumerations.PortalSystemTypes.commod:
                    strPortalSystemType = "commod";
                    break;
                case Enumerations.PortalSystemTypes.commodview:
                    strPortalSystemType = "commodview";
                    break;
                case Enumerations.PortalSystemTypes.finalview:
                    strPortalSystemType = "finalview";
                    break;
                case Enumerations.PortalSystemTypes.g1:
                    strPortalSystemType = "g1";
                    break;
                case Enumerations.PortalSystemTypes.g2:
                    strPortalSystemType = "g2";
                    break;
                case Enumerations.PortalSystemTypes.g3:
                    strPortalSystemType = "g3";
                    break;
                case Enumerations.PortalSystemTypes.g4:
                    strPortalSystemType = "g4";
                    break;
                case Enumerations.PortalSystemTypes.g5:
                    strPortalSystemType = "g5";
                    break;
                case Enumerations.PortalSystemTypes.g6:
                    strPortalSystemType = "g6";
                    break;
                case Enumerations.PortalSystemTypes.g7:
                    strPortalSystemType = "g7";
                    break;
                case Enumerations.PortalSystemTypes.g8:
                    strPortalSystemType = "g8";
                    break;
                case Enumerations.PortalSystemTypes.g9:
                    strPortalSystemType = "g9";
                    break;
                case Enumerations.PortalSystemTypes.g10:
                    strPortalSystemType = "g10";
                    break;
                case Enumerations.PortalSystemTypes.g11:
                    strPortalSystemType = "g11";
                    break;
                case Enumerations.PortalSystemTypes.g12:
                    strPortalSystemType = "g12";
                    break;
                case Enumerations.PortalSystemTypes.g13:
                    strPortalSystemType = "g13";
                    break;
                case Enumerations.PortalSystemTypes.g14:
                    strPortalSystemType = "g14";
                    break;
                case Enumerations.PortalSystemTypes.g15:
                    strPortalSystemType = "g15";
                    break;
                case Enumerations.PortalSystemTypes.g16:
                    strPortalSystemType = "g16";
                    break;
                case Enumerations.PortalSystemTypes.g17:
                    strPortalSystemType = "g17";
                    break;
                case Enumerations.PortalSystemTypes.g18:
                    strPortalSystemType = "g18";
                    break;
                case Enumerations.PortalSystemTypes.oo1final:
                    strPortalSystemType = "oo1final";
                    break;
                case Enumerations.PortalSystemTypes.oo2final:
                    strPortalSystemType = "oo2final";
                    break;
                case Enumerations.PortalSystemTypes.ooview:
                    strPortalSystemType = "ooview";
                    break;
                case Enumerations.PortalSystemTypes.qo:
                    strPortalSystemType = "qo";
                    break;
                case Enumerations.PortalSystemTypes.qo1final:
                    strPortalSystemType = "qo1final";
                    break;
                case Enumerations.PortalSystemTypes.qo2final:
                    strPortalSystemType = "qo2final";
                    break;
                case Enumerations.PortalSystemTypes.qoview:
                    strPortalSystemType = "qoview";
                    break;
                case Enumerations.PortalSystemTypes.sandt:
                    strPortalSystemType = "sandt";
                    break;
                case Enumerations.PortalSystemTypes.sandtview:
                    strPortalSystemType = "sandtview";
                    break;
                case Enumerations.PortalSystemTypes.Undefined:
                    strPortalSystemType = string.Empty;
                    break;
            }	//end switch

            return strPortalSystemType;
        }

        public Enumerations.PortalSystemTypes ConvertPortalSystemType(string strPortalSystemType)
        {
            Enumerations.PortalSystemTypes iTypeID = Enumerations.PortalSystemTypes.Undefined;

            switch (strPortalSystemType)
            {
                case "commod":
                    iTypeID = Enumerations.PortalSystemTypes.commod;
                    break;
                case "commodview":
                    iTypeID = Enumerations.PortalSystemTypes.commodview;
                    break;
                case "finalview":
                    iTypeID = Enumerations.PortalSystemTypes.finalview;
                    break;
                case "g1":
                    iTypeID = Enumerations.PortalSystemTypes.g1;
                    break;
                case "g2":
                    iTypeID = Enumerations.PortalSystemTypes.g2;
                    break;
                case "g3":
                    iTypeID = Enumerations.PortalSystemTypes.g3;
                    break;
                case "g4":
                    iTypeID = Enumerations.PortalSystemTypes.g4;
                    break;
                case "g5":
                    iTypeID = Enumerations.PortalSystemTypes.g5;
                    break;
                case "g6":
                    iTypeID = Enumerations.PortalSystemTypes.g6;
                    break;
                case "g7":
                    iTypeID = Enumerations.PortalSystemTypes.g7;
                    break;
                case "g8":
                    iTypeID = Enumerations.PortalSystemTypes.g8;
                    break;
                case "g9":
                    iTypeID = Enumerations.PortalSystemTypes.g9;
                    break;
                case "g10":
                    iTypeID = Enumerations.PortalSystemTypes.g10;
                    break;
                case "g11":
                    iTypeID = Enumerations.PortalSystemTypes.g11;
                    break;
                case "g12":
                    iTypeID = Enumerations.PortalSystemTypes.g12;
                    break;
                case "g13":
                    iTypeID = Enumerations.PortalSystemTypes.g13;
                    break;
                case "g14":
                    iTypeID = Enumerations.PortalSystemTypes.g14;
                    break;
                case "g15":
                    iTypeID = Enumerations.PortalSystemTypes.g15;
                    break;
                case "g16":
                    iTypeID = Enumerations.PortalSystemTypes.g16;
                    break;
                case "g17":
                    iTypeID = Enumerations.PortalSystemTypes.g17;
                    break;
                case "g18":
                    iTypeID = Enumerations.PortalSystemTypes.g18;
                    break;
                case "oo1final":
                    iTypeID = Enumerations.PortalSystemTypes.oo1final;
                    break;
                case "oo2final":
                    iTypeID = Enumerations.PortalSystemTypes.oo2final;
                    break;
                case "ooview":
                    iTypeID = Enumerations.PortalSystemTypes.ooview;
                    break;
                case "qo":
                    iTypeID = Enumerations.PortalSystemTypes.qo;
                    break;
                case "qo1final":
                    iTypeID = Enumerations.PortalSystemTypes.qo1final;
                    break;
                case "qo2final":
                    iTypeID = Enumerations.PortalSystemTypes.qo2final;
                    break;
                case "qoview":
                    iTypeID = Enumerations.PortalSystemTypes.qoview;
                    break;
                case "sandt":
                    iTypeID = Enumerations.PortalSystemTypes.sandt;
                    break;
                case "sandtview":
                    iTypeID = Enumerations.PortalSystemTypes.sandtview;
                    break;
                case "":
                    iTypeID = Enumerations.PortalSystemTypes.Undefined;
                    break;
            }	//end switch

            return iTypeID;
        }*/

        public Enumerations.UserTypes ConvertUserType(int iTypeID)
        {
            Enumerations.UserTypes iUserTypeID = Enumerations.UserTypes.NotDefined;

            switch (iTypeID)
            {
                case 1:
                    iUserTypeID = Enumerations.UserTypes.Admin;
                    break;
                case 2:
                    iUserTypeID = Enumerations.UserTypes.Ticket;
                    break;
                case 3:
                    iUserTypeID = Enumerations.UserTypes.Student;
                    break;
            }	//end switch

            return iUserTypeID;
        }
        #endregion

        #region Portal System Methods
        public bool GetScores(bool bShowWeightedScore, string strSort, string strSortDirection) /// puts string into an array of SqlParameters, gives attributes, and returns bool
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_SCORES, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_SCORES, arrSqlParams);
            }
            // gets #ranks instead of #tmp if dsResults.Tables[0], which is why [FinalScore] can't be found
            // figuring out why so many tables are returned by GetScores would be nice
            this.ScoringDataDT = dsResults.Tables[1];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetSocialOutcryScores(bool bShowWeightedScore, string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_SOCIAL_OUTCRY_SCORES, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_SOCIAL_OUTCRY_SCORES, arrSqlParams);
            }
            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetHeatData(int iGameID, string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@lGameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_HEAT_DATA, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_HEAT_DATA, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetOpenOutcry(Enumerations.OutcryTypes iOutcryType, string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;
            string strSP = string.Empty;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (iOutcryType == Enumerations.OutcryTypes.OpenOutcry)
            {
                strSP = SP_GET_OPEN_OUTCRY;
            }
            else if (iOutcryType == Enumerations.OutcryTypes.QuantOutcry)
            {
                strSP = SP_GET_QUANT_OUTCRY;
            }

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, strSP, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, strSP, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetBPCommodityDetails(string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_BP_COMMODITY_DETAILS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_BP_COMMODITY_DETAILS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetSandTDetails(string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_S_AND_T_DETAILS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_S_AND_T_DETAILS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetDetails(int casenumber, int startgameid, int numheats, int numsubheats, string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[6];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@casenumber";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Size = 3;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = casenumber;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@startgameid";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = startgameid;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@numheats";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Size = 3;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = numheats;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@numsubheats";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Size = 3;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = numsubheats;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetDetails", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetDetails", arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }


        public bool GetCreditRiskDetails(string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_CREDIT_RISK_DETAILS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_CREDIT_RISK_DETAILS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetThomsonReutersQEDDetails(string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_THOMSON_REUTERS_QED_DETAILS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_THOMSON_REUTERS_QED_DETAILS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteGame(int iGameID, int iAlgoGameID, GameSettings.GameImplementations eGameImplementation, ArrayList errorList)
        {
            SqlParameter[] arrSqlParams;

            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@lGameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@AlgoGameID";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iAlgoGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@iGameImplementationIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = (int)eGameImplementation;
            idx += 1;

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_GAME, arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_GAME, arrSqlParams);
            }

            return true;
        }

        public bool ProcessGameInput(int iGameID, GameSettings.GameImplementations eGameInplementation, ArrayList errorList)
        {
            SqlParameter[] arrSqlParams;

            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }
            else if (eGameInplementation == GameSettings.GameImplementations.NotDefined)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_IMPLEMENATION_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@lGameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@iGameImplementationIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = (int)eGameInplementation;
            idx += 1;

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_PROCESS_GAME_INPUT, arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_PROCESS_GAME_INPUT, arrSqlParams);
            }

            return true;
        }

        public bool GenerateAlgoScores()
        {
            bool bStat = false;

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_GENERATE_ALGO_SCORES, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_GENERATE_ALGO_SCORES, null);
            }

            bStat = true;

            return bStat;
        }

        public bool InsertAlgoRanking(
            string strTeamCode,
            string strRank,
            int iGameID,
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            //Validate & convert
            if (strTeamCode.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (!IsTeamCodeExist(strTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] {1}", strTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                return false;
            }
            else if (strRank.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_ALGO_RANK_MUST_BE_SPECIFIED);
                return false;
            }
            else if (!BaseBusiness.IsInt(strRank))
            {
                errorList.Add(BaseBusiness.ERR_MSG_RANK_MUST_BE_NUMERIC);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@RankIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Convert.ToInt16(strRank);
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_ALGO_RANKING, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_ALGO_RANKING, arrSqlParams);
            }

            if (obj1 != null)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool InsertPortalScore(
            string strTeamCode,
            string strUserCode,
            string strProfit,
            int iGameID,
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            //Validate & convert
            if (strTeamCode.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (strUserCode.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_CODE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (strProfit.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PROFIT_VALUE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (!IsTeamCodeExist(strTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] {1}", strTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                return false;
            }
            else if (!BaseBusiness.IsInt(strUserCode))
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_CODE_MUST_BE_NUMERIC);
                return false;
            }

            int iUserCode = int.Parse(strUserCode);
            float fProfit = float.Parse(strProfit);

            //GameID must be betw 1 & 38
            //Note: This count does not include algo
            if ((iGameID < 1) || (iGameID >= (int)Enumerations.GameIDs.NoMoreGames))
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_BETW_1_AND_41);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[4];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iUserCode;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ProfitIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Float;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = fProfit;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_PORTAL_SCORE, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_PORTAL_SCORE, arrSqlParams);
            }

            if (obj1 != null)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool IsTeamCodeExist(string strTeamCode)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            if (strTeamCode.Length == 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamCode;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_IS_TEAM_CODE_EXIST, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_IS_TEAM_CODE_EXIST, arrSqlParams);
            }

            if (obj1 != null)
            {
                if (Convert.ToInt16(obj1) == 1)
                {
                    bStat = true;
                }
            }

            return bStat;
        }

        public int GetPortalSystemValue(Enumerations.PortalSystemTypes iType)
        {
            int iValue = -1;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PortalSystemTypeIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_PORTAL_SYSTEM_VALUE, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_PORTAL_SYSTEM_VALUE, arrSqlParams);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];
                iValue = Convert.ToInt16(dr["Value"]);
            }

            return iValue;
        }

        public string GetCaseName(Enumerations.CaseID iType)
        {
            string iValue = null;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetCaseName", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetCaseName", arrSqlParams);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];
                iValue = Convert.ToString(dr["CaseName"]);
                return iValue;
            }

            else // empty table - create table with default values
            {
                if (m_bTransactionSupport)
                {
                    m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, "SetDefaultTableValues", null);
                }
                else
                {
                    m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, "SetDefaultTableValues", null);
                }
                return string.Empty;
            }

            
        }


        public string GetNumHeats(Enumerations.CaseID iType)
        {
            string iValue = null;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetNumHeats", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetNumHeats", arrSqlParams);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];

                if (dr["NumberHeats"].ToString() != "0")
                {
                    iValue = dr["NumberHeats"].ToString();
                }

                else
                {
                    iValue = "";
                }
            }

            return iValue;
        }


        public string GetNumSubheats(Enumerations.CaseID iType)
        {
            string iValue = null;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetNumSubheats", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetNumSubheats", arrSqlParams);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];

                if (dr["NumberSubheats"].ToString() != "0")
                {
                    iValue = dr["NumberSubheats"].ToString();
                }
                else
                {
                    iValue = "";
                }
            }

            return iValue;
        }

        public bool GetPublishSetting(int casenumber)
        {
            bool iValue = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@case";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = casenumber;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetPublishSetting", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetPublishSetting", arrSqlParams);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];
                iValue = (bool) dr["Publish"];
            }
     
            return iValue;
        }


        public string GetCaseWeighting(Enumerations.CaseID iType)
        {
            string iValue = null;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetCaseWeighting", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetCaseWeighting", arrSqlParams);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];
                if (dr["Weighting"].ToString() != "0")
                {
                    iValue = dr["Weighting"].ToString();
                }
                else
                {
                    iValue = "";
                }
            }

            return iValue;
        }




        public bool UpdatePortalSystemValue(
            Enumerations.PortalSystemTypes iType,
            string strValue,
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            //Validate & convert
            //Note: all values in db are numeric
            if (strValue.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PORTAL_SYSTEM_VALUE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (!BaseBusiness.IsInt(strValue))
            {
                errorList.Add(BaseBusiness.ERR_MSG_PORTAL_SYSTEM_VALUE_MUST_BE_NUMERIC);
                return false;
            }

            int iValue = int.Parse(strValue);

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PortalSystemTypeIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iValue;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_UPDATE_PORTAL_SYSTEM_VALUE, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_UPDATE_PORTAL_SYSTEM_VALUE, arrSqlParams);
            }

            if (iRowsUpdated == 1)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool UpdateCaseName(
            Enumerations.CaseID iType, 
            string strValue, 
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 50;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strValue;

            DataSet iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateCaseName", arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateCaseName", arrSqlParams);
            }
            bStat = true;
            return bStat;
        }

        public bool UpdateNumHeats(
        Enumerations.CaseID iType,
        string strValue,
        ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            if (strValue != "")
            {
                arrSqlParams[idx].Value = int.Parse(strValue);
            }
            else
            {
                arrSqlParams[idx].Value = 0;
            }
            idx += 1;

            //int iRowsUpdated;
            DataSet iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateNumHeats", arrSqlParams);
                //iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, "UpdateNumHeats", arrSqlParams);
            }
            else
            {
               // iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, "UpdateNumHeats", arrSqlParams);
                iRowsUpdated = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateNumHeats", arrSqlParams);
            }

            //if (iRowsUpdated == 1)
            //{
                bStat = true;
            //}

            return bStat;
        }
     /*   public bool UpdateNumHeats(
            Enumerations.CaseID iType,
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;


            int iValue = Int16.Parse(GetNumHeats(iType));

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iValue;
            idx += 1;

            DataSet iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateNumHeats", arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateNUmHeats", arrSqlParams);
            }

            
                bStat = true;
            

            return bStat;
        }*/

        public bool UpdateNumSubheats(
            Enumerations.CaseID iType,
            string strValue,
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;


            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            if (strValue != "")
            {
                arrSqlParams[idx].Value = int.Parse(strValue);
            }
            else
            {
                arrSqlParams[idx].Value = 0;
            }

            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, "UpdateNumSubheats", arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, "UpdateNumSubheats", arrSqlParams);
            }

            if (iRowsUpdated == 1)
            {
                bStat = true;
            }

            return bStat;
        }


        public bool UpdateWeighting(
                Enumerations.CaseID iType,
                string strValue,
                ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;


            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CaseIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            if (strValue != "")
            {
                arrSqlParams[idx].Value = int.Parse(strValue);
            }
            else
            {
                arrSqlParams[idx].Value = 0;
            }

            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, "UpdateWeighting", arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, "UpdateWeighting", arrSqlParams);
            }

            if (iRowsUpdated == 1)
            {
                bStat = true;
            }

            return bStat;
        }


        public bool UpdatePublishSetting(
                int iType,
                bool setting,
                ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;


            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@case";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iType;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@setting";
            arrSqlParams[idx].SqlDbType = SqlDbType.Bit;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = setting;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, "UpdatePublishSetting", arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, "UpdatePublishSetting", arrSqlParams);
            }

            if (iRowsUpdated == 1)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool ClearTicket(int iGameID, ArrayList errorList)
        {
            SqlParameter[] arrSqlParams;

            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_CLEAR_TICKET, arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_CLEAR_TICKET, arrSqlParams);
            }

            return true;
        }

        public bool GetOutcryPositions(int iGameID, string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@lGameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_OUTCRY_POSITIONS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_OUTCRY_POSITIONS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool UpdateFinalScoring()
        {
            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_UPDATE_FINAL_SCORING, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_UPDATE_FINAL_SCORING, null);
            }

            return true;
        }

        public bool UpdateOutcryScoring(
            int iGameID,
            string strFinalScore,
            GameSettings.GameImplementations eGameInplementation,
            ArrayList errorList)
        {
            bool bStat = false;
            DataSet dsResults = null;
            SqlParameter[] arrSqlParams;

            //Validate & convert
            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }
            else if (strFinalScore.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_FINAL_SCORE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (eGameInplementation == GameSettings.GameImplementations.NotDefined)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_IMPLEMENATION_MUST_BE_SPECIFIED);
                return false;
            }


            int idx = 0;
            int iFinalScore = int.Parse(strFinalScore);

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FinalScoreIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iFinalScore;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@iGameImplementationIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = (int)eGameInplementation;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_UPDATE_OUTCRY_SCORING, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_UPDATE_OUTCRY_SCORING, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                foreach (DataRow objDR in dsResults.Tables[0].Rows)
                {
                    errorList.Add(string.Format("<b>{0}</b> has been fined <b>{1}</b> for max stock position of <b>{2}</b>",
                        objDR["TeamCode"], objDR["Fine"], objDR["MaxPos"]));
                }
            }

            bStat = true;

            return bStat;
        }
        #endregion

        #region Login
        public bool CheckLogin(string strLoginName, string strPassword)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            //encrypt password
            Utilities objUtils = new Utilities();
            string strEncryptedPassword = objUtils.EncryptData(strPassword);
            objUtils = null;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strLoginNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 20;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strLoginName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strPasswordIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 20;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strPassword;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_CHECK_LOGIN, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_CHECK_LOGIN, arrSqlParams);
            }

            if (dsResults.Tables[0] != null)
            {
                if (dsResults.Tables[0].Rows.Count == 1)
                {
                    this.UserDataDT = dsResults.Tables[0];
                    bStat = true;
                }
            }

            return bStat;
        }
        #endregion

        #region Chart Methods
        public bool GetStockChartData()
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            arrSqlParams = new SqlParameter[0];

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_STOCK_CHART_DATA, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_STOCK_CHART_DATA, null);
            }

            this.StockChartDataDT = dsResults.Tables[0];

            if (this.StockChartDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public int GetToHappenRowCount()
        {
            int iRowCount = -1;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TO_HAPPEN_ROW_COUNT, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TO_HAPPEN_ROW_COUNT, null);
            }

            if (dsResults.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResults.Tables[0].Rows[0];
                iRowCount = Convert.ToInt32(dr[0]);
            }

            return iRowCount;
        }

        #endregion

        #region Outcry Methods
        public bool InsertTrade(
            string strBuyerTeamCode,
            string strSellerTeamCode,
            string strQty,
            string strPrice,
            string strSession,
            long lUserID,
            ArrayList errorList)
        {
            bool bStat = false;
            bool bError = false;
            SqlParameter[] arrSqlParams;

            //Validate & convert
            if (strBuyerTeamCode.Length == 0)
            {
                errorList.Add(string.Format("{0} for Buyer", BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED));
                bError = true;
            }
            if (strSellerTeamCode.Length == 0)
            {
                errorList.Add(string.Format("{0} for Seller", BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED));
                bError = true;
            }
            if (strQty.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_QUANTITY_MUST_BE_SPECIFIED);
                bError = true;
            }
            if (strPrice.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PRICE_MUST_BE_SPECIFIED);
                bError = true;
            }
            if (strSession.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_SESSION_MUST_BE_SPECIFIED);
                bError = true;
            }
            if (!IsTeamCodeExist(strBuyerTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] Buyer {1}", strBuyerTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                bError = true;
            }
            if (!IsTeamCodeExist(strSellerTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] Seller {1}", strSellerTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                bError = true;
            }
            if (strBuyerTeamCode.ToUpper().CompareTo(strSellerTeamCode.ToUpper()) == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_BUYING_AND_SELLING_TEAMS_MUST_BE_DIFFERENT);
                bError = true;
            }
            if (!BaseBusiness.IsInt(strQty))
            {
                errorList.Add(BaseBusiness.ERR_MSG_QUANTITY_MUST_BE_NUMERIC);
                bError = true;
            }
            if (!BaseBusiness.IsInt(strPrice))
            {
                errorList.Add(BaseBusiness.ERR_MSG_PRICE_MUST_BE_NUMERIC);
                bError = true;
            }
            if (!BaseBusiness.IsInt(strSession))
            {
                errorList.Add(BaseBusiness.ERR_MSG_SESSION_MUST_BE_NUMERIC);
                bError = true;
            }
            if (lUserID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED);
                bError = true;
            }

            if (bError)
            {
                return false;
            }

            int iQty = int.Parse(strQty);
            int iSession = int.Parse(strSession);
            long lPrice = long.Parse(strPrice);

            int idx = 0;

            arrSqlParams = new SqlParameter[6];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@BuyerTeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strBuyerTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SellerTeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSellerTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@QuantityIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iQty;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SessionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iSession;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PriceIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lPrice;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_TRADE, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_TRADE, arrSqlParams);
            }

            if (obj1 != null)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool ClearTrades()
        {
            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_CLEAR_TRADES, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_CLEAR_TRADES, null);
            }

            return true;
        }

        public bool GetTrades(string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TRADES, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TRADES, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetTradeFinalScores(int iSession, int iFinalPrice, string strSort, string strSortDirection)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[4];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SessionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iSession;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FinalPriceIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iFinalPrice;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 25;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSort;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strSortDirectionIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSortDirection;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TRADE_FINAL_SCORES, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TRADE_FINAL_SCORES, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool FillTeamDropdown()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_FILL_TEAM_DROPDOWN, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_FILL_TEAM_DROPDOWN, null);
            }

            this.DropdownDataDT = dsResults.Tables[0];

            if (this.DropdownDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }
        #endregion

        #region Portal Ticket Methods
        public bool InsertTicket(
            string strBuyerTeamCode,
            string strSellerTeamCode,
            string strQty,
            string strPrice,
            string strSerialNumber,
            string strGameID,
            long lUserID,
            ArrayList errorList)
        {
            bool bStat = false;
            bool bValid = true;
            int iQty = 0;
            long lPrice = 0;
            int iGameID = -1;
            SqlParameter[] arrSqlParams;

            bValid = IsTicketEntriesValid(
                strBuyerTeamCode,
                strSellerTeamCode,
                strQty,
                strPrice,
                strGameID,
                strSerialNumber,
                lUserID,
                ref iQty,
                ref lPrice,
                ref iGameID,
                true,
                errorList);

            if (!bValid)
            {
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[7];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@BuyerTeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strBuyerTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SellerTeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSellerTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@QuantityIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iQty;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PriceIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lPrice;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SerialNumberIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_SERIAL_NUMBER;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSerialNumber;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_TICKET, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_TICKET, arrSqlParams);
            }

            if (obj1 != null)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool UpdateTicket(
            long lPortalOutcryID,
            string strBuyerTeamCode,
            string strSellerTeamCode,
            string strQty,
            string strPrice,
            string strNewSerialNumber,
            string strGameID,
            string strNewFileName,
            string strCurrentSerialNumber,
            string strCurrentFileName,
            long lUserID,
            ArrayList errorList)
        {
            bool bStat = false;
            bool bValid = true;
            int iQty = 0;
            long lPrice = 0;
            int iGameID = -1;
            bool bCheckIfSerialNumberExists = true;
            SqlParameter[] arrSqlParams;

            if (lPortalOutcryID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_PORTAL_OUTCRY_ID_MUST_BE_SPECIFIED));
            }

            if (strNewSerialNumber.CompareTo(strCurrentSerialNumber) == 0)
            {
                bCheckIfSerialNumberExists = false;
            }

            bValid = IsTicketEntriesValid(
                strBuyerTeamCode,
                strSellerTeamCode,
                strQty,
                strPrice,
                strGameID,
                strNewSerialNumber,
                lUserID,
                ref iQty,
                ref lPrice,
                ref iGameID,
                bCheckIfSerialNumberExists,
                errorList);

            if (!bValid)
            {
                return false;
            }

            //Only do below file validation if a file name has been provided.
            if (strNewFileName.Length > 0)
            {
                //If the file name hasn't changed, then no need to do any validation
                if (strNewFileName.CompareTo(strCurrentFileName) != 0)
                {
                    //1st, check if the physical file exists. If not, then entry can't be added. The file
                    //will have to be uploaded.
                    if (!this.IsTicketImageFileExist(strNewFileName))
                    {
                        errorList.Add(BaseBusiness.ERR_MSG_TICKET_IMAGE_FILE_DOES_NOT_EXIST);
                        return false;
                    }

                    //2nd, if physical file exists, check if the file has already been matched with another transaction.
                    if (this.IsTicketImageBeenMatched(strNewFileName))
                    {
                        errorList.Add(BaseBusiness.ERR_MSG_TICKET_IMAGE_FILE_ALREADY_MATCHED);
                        return false;
                    }
                }
            }
            //else
            //{
            //    //Check if the file name is being removed. If so:
            //    //(1) Delete the file.
            //    //(2) Remove
            //    if (strNewFileName.CompareTo(strCurrentFileName) != 0)
            //    {
            //    }
            //}

            int idx = 0;

            arrSqlParams = new SqlParameter[9];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PortalOutcryIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lPortalOutcryID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@BuyerTeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strBuyerTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SellerTeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSellerTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@QuantityIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iQty;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PriceIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lPrice;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SerialNumberIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_SERIAL_NUMBER;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strNewSerialNumber;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FileNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TICKET_IMAGE_FILENAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strNewFileName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_UPDATE_TICKET, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_UPDATE_TICKET, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteTicket(long lPortalOutcryID)
        {
            bool bStat = false;

            if (lPortalOutcryID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_PORTAL_OUTCRY_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PortalOutcryIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lPortalOutcryID;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_TICKET, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_TICKET, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        private bool IsTicketEntriesValid(
            string strBuyerTeamCode,
            string strSellerTeamCode,
            string strQty,
            string strPrice,
            string strGameID,
            string strSerialNumber,
            long lUserID,
            ref int iQty,
            ref long lPrice,
            ref int iGameID,
            bool bCheckIfSerialNumberExists,
            ArrayList errorList)
        {
            bool bValid = true;

            if (strBuyerTeamCode.Length == 0)
            {
                errorList.Add(string.Format("{0} for Buying Team", BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED));
                bValid = false;
            }
            if (strSellerTeamCode.Length == 0)
            {
                errorList.Add(string.Format("{0} for Selling Team", BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED));
                bValid = false;
            }
            if (strQty.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_QUANTITY_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strPrice.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PRICE_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strSerialNumber.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_SERIAL_NUMBER_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strSerialNumber.Length != LENGTH_SERIAL_NUMBER)
            {
                errorList.Add(BaseBusiness.ERR_MSG_SERIAL_NUMBER_NOT_CORRECT_LENGTH);
                bValid = false;
            }
            if (!IsTeamCodeExist(strBuyerTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] Buying Team {1}", strBuyerTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                bValid = false;
            }
            if (!IsTeamCodeExist(strSellerTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] Selling Team {1}", strSellerTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                bValid = false;
            }
            if (strBuyerTeamCode.ToUpper().CompareTo(strSellerTeamCode.ToUpper()) == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_BUYING_AND_SELLING_TEAMS_MUST_BE_DIFFERENT);
                bValid = false;
            }
            if (!BaseBusiness.IsInt(strGameID))
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_NUMERIC);
                bValid = false;
            }
            if (!BaseBusiness.IsInt(strQty))
            {
                errorList.Add(BaseBusiness.ERR_MSG_QUANTITY_MUST_BE_NUMERIC);
                bValid = false;
            }
            if (!BaseBusiness.IsInt(strPrice))
            {
                errorList.Add(BaseBusiness.ERR_MSG_PRICE_MUST_BE_NUMERIC);
                bValid = false;
            }
            if (bCheckIfSerialNumberExists)
            {
                if (IsSerialNumberExist(strSerialNumber))
                {
                    errorList.Add(BaseBusiness.ERR_MSG_SERIAL_NUMBER_ALREADY_EXISTS);
                    bValid = false;
                }
            }
            if (lUserID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED);
                bValid = false;
            }

            iQty = int.Parse(strQty);
            lPrice = long.Parse(strPrice);
            iGameID = int.Parse(strGameID);

            //More validation
            if (iQty <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_QUANTITY_IS_TOO_LOW);
                bValid = false;
            }
            if (iQty > 100)
            {
                errorList.Add(BaseBusiness.ERR_MSG_QUANTITY_IS_TOO_HIGH);
                bValid = false;
            }
            if (lPrice < 100)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PRICE_IS_TOO_LOW);
                bValid = false;
            }
            if (lPrice > 10000)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PRICE_IS_TOO_HIGH);
                bValid = false;
            }

            return bValid;
        }

        public bool GetTicket(long lPortalOutcryID)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (lPortalOutcryID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_PORTAL_OUTCRY_ID_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PortalOutcryIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lPortalOutcryID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TICKET, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TICKET, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetRecentTickets(int iGameID, long lUserID, ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }
            else if (lUserID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_RECENT_TICKETS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_RECENT_TICKETS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetTeams()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TEAMS, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TEAMS, null);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetOutcryTransactions(int iGameID, long lTeamID, ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }
            else if (lTeamID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_ID_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lTeamID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_OUTCRY_TRANSACTIONS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_OUTCRY_TRANSACTIONS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetTeamForUser(long lUserID)
        {
            bool bStat = false;

            if (lUserID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TEAM_FOR_USER, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TEAM_FOR_USER, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetTicketImage(long lTicketImageID)
        {
            bool bStat = false;

            if (lTicketImageID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TICKET_IMAGE_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TicketImageIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lTicketImageID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TICKET_IMAGE, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TICKET_IMAGE, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetAllTicketImagesForGame(int iGameID)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (iGameID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_ALL_TICKET_IMAGES_FOR_GAME, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_ALL_TICKET_IMAGES_FOR_GAME, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool IsSerialNumberExist(string strSerialNumber)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            if (strSerialNumber.Length == 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_SERIAL_NUMBER_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SerialNumberIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_SERIAL_NUMBER;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSerialNumber;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_IS_SERIAL_NUMBER_EXIST, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_IS_SERIAL_NUMBER_EXIST, arrSqlParams);
            }

            if (obj1 != null)
            {
                if (Convert.ToInt16(obj1) == 1)
                {
                    bStat = true;
                }
            }

            return bStat;
        }

        public bool IsTicketImageFileExist(string strFileName)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            if (strFileName.Length == 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TICKET_IMAGE_FILE_NAME_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FileNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TICKET_IMAGE_FILENAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strFileName;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_IS_TICKET_IMAGE_FILE_EXIST, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_IS_TICKET_IMAGE_FILE_EXIST, arrSqlParams);
            }

            if (obj1 != null)
            {
                if (Convert.ToInt16(obj1) == 1)
                {
                    bStat = true;
                }
            }

            return bStat;
        }

        /// <summary>
        /// Method: IsTicketImageBeenMatched
        /// Checks if the file has already been matched with another transaction.
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public bool IsTicketImageBeenMatched(string strFileName)
        {
            bool bStat = true;
            SqlParameter[] arrSqlParams;

            if (strFileName.Length == 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TICKET_IMAGE_FILE_NAME_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FileNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TICKET_IMAGE_FILENAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strFileName;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_IS_TICKET_IMAGE_FILE_BEEN_MATCHED, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_IS_TICKET_IMAGE_FILE_BEEN_MATCHED, arrSqlParams);
            }

            bStat = Convert.ToBoolean(obj1);
            //if (obj1 != null)
            //{
            //    //if (Convert.ToBoolean(obj1) == 1)
            //    //{
            //    //    bStat = true;
            //    //}
            //}

            return bStat;
        }

        #endregion

        #region News Methods
        public bool GetNews()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_NEWS, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_NEWS, null);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetNewsDetail(long lNewsID)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@NewsIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lNewsID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_NEWS_DETAIL, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_NEWS_DETAIL, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }
        #endregion

        #region Run Outcry Methods
        public bool UpdateRefresher()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_UPDATE_REFRESHER, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_UPDATE_REFRESHER, null);
            }

            if (dsResults.Tables.Count > 0)
            {
                this.ScoringDataDT = dsResults.Tables[0];
                bStat = true;
            }

            return bStat;
        }

        public bool ResetOutcry()
        {
            bool bStat = false;

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_RESET_OUTCRY, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_RESET_OUTCRY, null);
            }

            bStat = true;

            return bStat;
        }
        #endregion

        #region Generate News
        public bool InsertRawData(
            string strEnabled,
            string strHeader,
            string strBody,
            string strTimeIn,
            string strIndexValue,
            string strEndTime,
            ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[6];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@EnabledIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 10;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strEnabled;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@HeaderIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 255;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strHeader;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@BodyIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Text;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strBody;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@StartTimeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 11;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTimeIn;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@IndexValueIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 11;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strIndexValue;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@EndTimeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 11;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strEndTime;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_RAW_DATA, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_RAW_DATA, arrSqlParams);
            }

            if (obj1 != null)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool GetRawData(ref int iNumEvents, ref int iRunningIndex)
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_RAW_DATA, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_RAW_DATA, null);
            }

            //Note: 3 recordsets will be returned
            DataRow objDR1 = dsResults.Tables[0].Rows[0];
            iNumEvents = Convert.ToInt16(objDR1[0]);
            this.ScoringDataDT = dsResults.Tables[1];
            DataRow objDR2 = dsResults.Tables[2].Rows[0];
            iRunningIndex = Convert.ToInt16(objDR2[0]);

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteRawData()
        {
            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_GET_DELETE_RAW_DATA, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_GET_DELETE_RAW_DATA, null);
            }

            return true;
        }

        #endregion

        #region Admin - Teams
        public bool GetTeam(long lTeamID)
        {
            bool bStat = false;

            if (lTeamID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TEAM_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lTeamID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_TEAM, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_TEAM, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool InsertTeamImport(
            string strTeamCode,
            string strTeamName,
            string strLoginName,
            string strPassword,
            ref long lTeamID,
            ArrayList errorList)
        {
            bool bStat = false;

            if (this.InsertTeam(strTeamCode, strTeamName, ref lTeamID, errorList))
            {
                long lUserID = -1;
                int iUserTypeID = 3;
                if (this.InsertUser(strLoginName, strPassword, strPassword, iUserTypeID.ToString(), "Y", lTeamID.ToString(), ref lUserID, errorList))
                {
                    bStat = true;
                }
            }

            //if (!IsTeamInputsValid(strTeamCode, strTeamName, true, errorList))
            //{
            //    return bStat;
            //}
            //else if (!IsUserInputsValid(strLoginName, strPassword, strPassword,iUserTypeID,"Y","-1",true))
            //{
            //    return bStat;
            //}

            ////encrypt password
            //Utilities objUtils = new Utilities();
            //string strEncryptedPassword = objUtils.EncryptData(strPassword);
            //objUtils = null;

            //SqlParameter[] arrSqlParams;

            //int idx = 0;

            //arrSqlParams = new SqlParameter[4];

            //arrSqlParams[idx] = new SqlParameter();
            //arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            //arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            //arrSqlParams[idx].Size = LENGTH_TEAM_CODE;
            //arrSqlParams[idx].Direction = ParameterDirection.Input;
            //arrSqlParams[idx].Value = strTeamCode;
            //idx += 1;

            //arrSqlParams[idx] = new SqlParameter();
            //arrSqlParams[idx].ParameterName = "@TeamNameIn";
            //arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            //arrSqlParams[idx].Size = LENGTH_TEAM_NAME;
            //arrSqlParams[idx].Direction = ParameterDirection.Input;
            //arrSqlParams[idx].Value = strTeamName;
            //idx += 1;

            //arrSqlParams[idx] = new SqlParameter();
            //arrSqlParams[idx].ParameterName = "@LoginNameIn";
            //arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            //arrSqlParams[idx].Size = LENGTH_LOGIN_NAME;
            //arrSqlParams[idx].Direction = ParameterDirection.Input;
            //arrSqlParams[idx].Value = strLoginName;
            //idx += 1;

            //arrSqlParams[idx] = new SqlParameter();
            //arrSqlParams[idx].ParameterName = "@PasswordIn";
            //arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            //arrSqlParams[idx].Size = LENGTH_PASSWORD;
            //arrSqlParams[idx].Direction = ParameterDirection.Input;
            //arrSqlParams[idx].Value = strEncryptedPassword;
            //idx += 1;

            ////arrSqlParams[idx] = new SqlParameter();
            ////arrSqlParams[idx].ParameterName = "@UserStatusIn";
            ////arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            ////arrSqlParams[idx].Size = 1;
            ////arrSqlParams[idx].Direction = ParameterDirection.Input;
            ////arrSqlParams[idx].Value = strUserStatus;
            ////idx += 1;

            ////arrSqlParams[idx] = new SqlParameter();
            ////arrSqlParams[idx].ParameterName = "@UserTypeIDIn";
            ////arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            ////arrSqlParams[idx].Direction = ParameterDirection.Input;
            ////arrSqlParams[idx].Value = int.Parse(strUserTypeID);
            ////idx += 1;

            //Object obj1;
            //if (m_bTransactionSupport)
            //{
            //    obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_TEAM_IMPORT, arrSqlParams);
            //}
            //else
            //{
            //    obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_TEAM_IMPORT, arrSqlParams);
            //}

            //if (obj1 != null)
            //{
            //    lTeamID = Convert.ToInt32(obj1);
            //    bStat = true;
            //}

            return bStat;
        }

        public bool InsertTeam(
            string strTeamCode,
            string strTeamName,
            ref long lTeamID,
            ArrayList errorList)
        {
            bool bStat = false;

            if (!IsTeamInputsValid(strTeamCode, strTeamName, true, errorList))
            {
                return bStat;
            }

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TEAM_CODE;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TEAM_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamName;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_TEAM, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_TEAM, arrSqlParams);
            }

            if (obj1 != null)
            {
                lTeamID = Convert.ToInt32(obj1);
                bStat = true;
            }

            return bStat;
        }

        public bool UpdateTeam(
            long lTeamID,
            string strNewTeamCode,
            string strCurrentTeamCode,
            string strTeamName,
            ArrayList errorList)
        {
            bool bStat = false;
            bool bCheckIfTeamCodeExists = true;

            if (strNewTeamCode.CompareTo(strCurrentTeamCode) == 0)
            {
                bCheckIfTeamCodeExists = false;
            }

            if (!IsTeamInputsValid(strNewTeamCode, strTeamName, bCheckIfTeamCodeExists, errorList))
            {
                return bStat;
            }
            if (lTeamID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TEAM_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lTeamID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TEAM_CODE;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strNewTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_TEAM_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamName;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_UPDATE_TEAM, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_UPDATE_TEAM, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteTeam(long lTeamID)
        {
            bool bStat = false;

            if (lTeamID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_TEAM_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lTeamID;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_TEAM, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_TEAM, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteAllTeams()
        {
            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_ALL_TEAMS, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_ALL_TEAMS, null);
            }

            return true;
        }

        private bool IsTeamInputsValid(string strTeamCode, string strTeamName, bool bCheckIfTeamCodeExists, ArrayList errorList)
        {
            bool bValid = true;

            if (strTeamCode.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strTeamName.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_NAME_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strTeamCode.Length > LENGTH_TEAM_CODE)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_CODE_IS_TOO_LONG);
                bValid = false;
            }
            if (strTeamName.Length > LENGTH_TEAM_NAME)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_NAME_IS_TOO_LONG);
                bValid = false;
            }
            //Determine if Team Code already exists. Don't allow duplicate team codes.
            if (bCheckIfTeamCodeExists)
            {
                if (IsTeamCodeExist(strTeamCode.ToUpper()))
                {
                    errorList.Add(string.Format("[{0}] {1}", strTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_ALREADY_EXISTS));
                    bValid = false;
                }
            }

            return bValid;
        }

        #endregion

        #region Admin - Users
        public bool GetUser(long lUserID)
        {
            bool bStat = false;

            if (lUserID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_USER, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_USER, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;

                //decrypt password
                DataRow objDR = this.ScoringDataDT.Rows[0];
                Utilities objUtils = new Utilities();
                objDR["Password"] = objUtils.DecryptData(objDR["Password"].ToString());
                objUtils = null;
            }

            return bStat;
        }

        public bool GetUsers()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_USERS, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_USERS, null);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool InsertUser(
            string strLoginName,
            string strPassword,
            string strConfirmPassword,
            string strUserTypeID,
            string strUserStatus,
            string strTeamID,
            ref long lUserID,
            ArrayList errorList)
        {
            bool bStat = false;

            //LoginName, Password, UserTypeID, UserStatus are required fields
            if (!IsUserInputsValid(strLoginName, strPassword, strConfirmPassword, strUserTypeID, strUserStatus, strTeamID, true, errorList))
            {
                return bStat;
            }

            //encrypt password
            Utilities objUtils = new Utilities();
            string strEncryptedPassword = objUtils.EncryptData(strPassword);
            objUtils = null;

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[5];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@LoginNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_LOGIN_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strLoginName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PasswordIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_PASSWORD;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strEncryptedPassword;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserStatusIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Char;
            arrSqlParams[idx].Size = 1;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strUserStatus;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserTypeIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = int.Parse(strUserTypeID);
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            long lTeamID = long.Parse(strTeamID);
            if (lTeamID == -1)
            {
                arrSqlParams[idx].Value = System.DBNull.Value;
            }
            else
            {
                arrSqlParams[idx].Value = lTeamID;
            }
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_USER, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_USER, arrSqlParams);
            }

            if (obj1 != null)
            {
                lUserID = Convert.ToInt32(obj1);
                bStat = true;
            }

            return bStat;
        }

        public bool UpdateUser(
            long lUserID,
            string strNewLoginName,
            string strCurrentLoginName,
            string strPassword,
            string strConfirmPassword,
            string strUserTypeID,
            string strUserStatus,
            string strTeamID,
            ArrayList errorList)
        {
            bool bStat = false;
            bool bCheckIfUserLoginExists = true;

            if (strNewLoginName.CompareTo(strCurrentLoginName) == 0)
            {
                bCheckIfUserLoginExists = false;
            }

            //LoginName, Password, UserTypeID, UserStatus are required fields
            if (!IsUserInputsValid(strNewLoginName, strPassword, strConfirmPassword, strUserTypeID, strUserStatus, strTeamID, bCheckIfUserLoginExists, errorList))
            {
                return bStat;
            }
            if (lUserID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED));
            }

            //encrypt password
            Utilities objUtils = new Utilities();
            string strEncryptedPassword = objUtils.EncryptData(strPassword);
            objUtils = null;

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[6];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@LoginNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_LOGIN_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strNewLoginName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PasswordIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_PASSWORD;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strEncryptedPassword;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserStatusIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 1;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strUserStatus;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserTypeIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = int.Parse(strUserTypeID);
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            long lTeamID = long.Parse(strTeamID);
            if (lTeamID == -1)
            {
                arrSqlParams[idx].Value = System.DBNull.Value;
            }
            else
            {
                arrSqlParams[idx].Value = lTeamID;
            }
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_UPDATE_USER, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_UPDATE_USER, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteUser(
            long lUserID)
        {
            bool bStat = false;

            if (lUserID <= 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_USER_ID_MUST_BE_SPECIFIED));
            }

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lUserID;
            idx += 1;

            int iRowsUpdated;
            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_USER, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_USER, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteAllUsers()
        {
            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_ALL_USERS, null);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_ALL_USERS, null);
            }

            return true;
        }

        public bool IsUserLoginExist(string strLoginName)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            if (strLoginName.Length == 0)
            {
                throw (new Exception(BaseBusiness.ERR_MSG_LOGIN_NAME_MUST_BE_SPECIFIED));
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@LoginNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_LOGIN_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strLoginName;
            idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_IS_USER_LOGIN_EXIST, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_IS_USER_LOGIN_EXIST, arrSqlParams);
            }

            if (obj1 != null)
            {
                if (Convert.ToInt16(obj1) == 1)
                {
                    bStat = true;
                }
            }

            return bStat;
        }

        private bool IsUserInputsValid(
            string strLoginName,
            string strPassword,
            string strConfirmPassword,
            string strUserTypeID,
            string strUserStatus,
            string strTeamID,
            bool bCheckIfUserLoginExists,
            ArrayList errorList)
        {
            bool bValid = true;

            //LoginName, Password, ConfirmPassword, UserTypeID, UserStatus are required fields

            if (strLoginName.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_LOGIN_NAME_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strLoginName.Length > LENGTH_LOGIN_NAME)
            {
                errorList.Add(BaseBusiness.ERR_MSG_LOGIN_NAME_IS_TOO_LONG);
                bValid = false;
            }
            if (strPassword.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PASSWORD_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strConfirmPassword.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PASSWORD_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if (strPassword.CompareTo(strConfirmPassword) != 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PASSWORDS_DO_NOT_MATCH);
                bValid = false;
            }
            if (strPassword.Length > LENGTH_PASSWORD_ENTRY)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PASSWORD_IS_TOO_LONG);
                bValid = false;
            }
            if (strConfirmPassword.Length > LENGTH_PASSWORD_ENTRY)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PASSWORD_IS_TOO_LONG);
                bValid = false;
            }
            if (strUserStatus.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_STATUS_MUST_BE_SPECIFIED);
                bValid = false;
            }
            if ((strUserStatus.CompareTo("Y") != 0) && (strUserStatus.CompareTo("N") != 0))
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_STATUS_INVALID);
                bValid = false;
            }
            if (strUserTypeID.CompareTo("-1") == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_TYPE_MUST_BE_SPECIFIED);
                bValid = false;
            }
            //TODO: check for valid user type
            if (bCheckIfUserLoginExists)
            {
                if (this.IsUserLoginExist(strLoginName))
                {
                    errorList.Add(BaseBusiness.ERR_MSG_LOGIN_NAME_ALREADY_EXISTS);
                    bValid = false;
                }
            }


            return bValid;
        }

        public bool GetUserTypes()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_USER_TYPES, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_USER_TYPES, null);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        #endregion

        #region Search Tickets
        public bool FindSerialNumber(string strSerialNumber, ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (strSerialNumber.Length <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_SERIAL_NUMBER_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SerialNumberIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_SERIAL_NUMBER;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strSerialNumber;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_FIND_SERIAL_NUMBER, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_FIND_SERIAL_NUMBER, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool FindTicketImageFile(string strTicketImageFile, ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (strTicketImageFile.Length <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TICKET_IMAGE_FILE_NAME_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TicketImageFileIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 20;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTicketImageFile;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_FIND_TICKET_IMAGE_FILE, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_FIND_TICKET_IMAGE_FILE, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool FindTransactionsByTeamAndGame(int iGameID, long lTeamID, ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            if (iGameID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_SPECIFIED);
                return false;
            }
            else if (lTeamID <= 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_NAME_MUST_BE_SPECIFIED);
                return false;
            }

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@GameIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iGameID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = lTeamID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_FIND_TRANSACTIONS_BY_TEAM_AND_GAME, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_FIND_TRANSACTIONS_BY_TEAM_AND_GAME, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool FindTransactionsWithNoTickets()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_FIND_TRANSACTIONS_WITH_NO_TICKETS, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_FIND_TRANSACTIONS_WITH_NO_TICKETS, null);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool FindAllTickets()
        {
            bool bStat = false;
            DataSet dsResults = null;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_FIND_ALL_TICKETS, null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_FIND_ALL_TICKETS, null);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        #endregion

        #region Match Tickets
        public bool GetUnmatchedTickets(int iNumRowsToReturn)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@NumberRowsToReturnIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iNumRowsToReturn;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, SP_GET_UNMATCHED_TICKETS, arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, SP_GET_UNMATCHED_TICKETS, arrSqlParams);
            }

            this.ScoringDataDT = dsResults.Tables[0];

            if (this.ScoringDataDT.Rows.Count > 0)
            {
                bStat = true;
            }

            return bStat;
        }

        #endregion

        #region Upload Tickets
        public int InsertTicketImage(string fileName)
        {
            string[] fileArray = fileName.Split('.');
            string serialNumber = fileArray[0]; // It's assumed the serial number will always be in the 2nd bucket
            int errorID = 0;

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FileName";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_FILE_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = fileName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SerialNumber";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_SERIAL_NUMBER;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = serialNumber;
            idx += 1;

            if (m_bTransactionSupport)
            {
                errorID = (int)m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_TICKET_IMAGE_IMAGE, arrSqlParams);
            }
            else
            {
                errorID = (int)m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_TICKET_IMAGE_IMAGE, arrSqlParams);
            }

            return errorID;
        }

        #endregion

        #region View Settings

        public bool CheckViewSettings(string pageName)
        {
            bool show;
            if (m_bTransactionSupport)
            {
                show = Convert.ToBoolean((bool)m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, CHECK_VIEW_SETTINGS, pageName));
            }
            else
            {
                show = Convert.ToBoolean(m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, CHECK_VIEW_SETTINGS, pageName));
            }

            return show;
        }

        public bool UpdateViewSettings(string pageName, bool showToPublic)
        {
            bool bStat = false;
            int iRowsUpdated;

            int idx = 0;

            int i;
            if (showToPublic)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }

            SqlParameter[] arrSqlParams;
            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@strPage";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = LENGTH_FILE_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = pageName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@intShow";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Size = LENGTH_FILE_NAME;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = i;
            idx += 1;

            if (m_bTransactionSupport)
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, UPDATE_VIEW_SETTINGS, arrSqlParams);
            }
            else
            {
                iRowsUpdated = m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, UPDATE_VIEW_SETTINGS, arrSqlParams);
            }

            if (iRowsUpdated > 0)
            {
                bStat = true;
            }

            return bStat;

        }

        public bool InsertPortalSocialOutcryScore(string strTraderID, string strTeamCode, string strSocialOutcryScore, ArrayList errorList)
        {
            bool bStat = false;
            SqlParameter[] arrSqlParams;

            //Validate & convert
            if (strTeamCode.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_TEAM_CODE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (strTraderID.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_CODE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (strSocialOutcryScore.Length == 0)
            {
                errorList.Add(BaseBusiness.ERR_MSG_PROFIT_VALUE_MUST_BE_SPECIFIED);
                return false;
            }
            else if (!IsTeamCodeExist(strTeamCode.ToUpper()))
            {
                errorList.Add(string.Format("[{0}] {1}", strTeamCode, BaseBusiness.ERR_MSG_TEAM_CODE_DOES_NOT_EXIST));
                return false;
            }
            else if (!BaseBusiness.IsInt(strTraderID))
            {
                errorList.Add(BaseBusiness.ERR_MSG_USER_CODE_MUST_BE_NUMERIC);
                return false;
            }

            int iUserCode = int.Parse(strTraderID);
            float fProfit = float.Parse(strSocialOutcryScore);

            ////GameID must be betw 1 & 38
            ////Note: This count does not include algo
            //if ((iGameID < 1) || (iGameID >= (int)Enumerations.GameIDs.NoMoreGames))
            //{
            //    errorList.Add(BaseBusiness.ERR_MSG_GAME_ID_MUST_BE_BETW_1_AND_41);
            //    return false;
            //}

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TeamCodeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 4;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = strTeamCode.ToUpper();
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TraderIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = iUserCode;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SocialOutcryScoreIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Float;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = fProfit;
            idx += 1;

            //arrSqlParams[idx] = new SqlParameter();
            //arrSqlParams[idx].ParameterName = "@GameIDIn";
            //arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            //arrSqlParams[idx].Direction = ParameterDirection.Input;
            //arrSqlParams[idx].Value = iGameID;
            //idx += 1;

            Object obj1;
            if (m_bTransactionSupport)
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(this.m_objTransaction, SP_INSERT_SOCIAL_OUTCRY_SCORE, arrSqlParams);
            }
            else
            {
                obj1 = m_dbMSSqlHelper.ExecuteScalar(base.m_conROTMAN, SP_INSERT_SOCIAL_OUTCRY_SCORE, arrSqlParams);
            }

            if (obj1 != null)
            {
                bStat = true;
            }

            return bStat;
        }

        public bool DeleteSocialOutcryScores(ArrayList errorList)
        {

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteNonQuery(this.m_objTransaction, SP_DELETE_SOCIAL_OUTCRY_SCORES);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteNonQuery(base.m_conROTMAN, SP_DELETE_SOCIAL_OUTCRY_SCORES);
            }

            return true;
        }

        #endregion

        public bool GetMemberDetails(int UserID)
        {
            
            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = UserID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetMemberDetails", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetMemberDetails", arrSqlParams);
            }


            m_dtUserData = dsResults.Tables[0];
            return true;


        }




        public bool GetFacultyDetails(int UserID)
        {

            SqlParameter[] arrSqlParams;
            DataSet dsResults = null;

            int idx = 0;

            arrSqlParams = new SqlParameter[1];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = UserID;
            idx += 1;

            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetFacultyDetails", arrSqlParams);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetFacultyDetails", arrSqlParams);
            }


            m_dtUserData = dsResults.Tables[0];
            return true;


        }

        public void DownloadFaculty()
        {
            DataSet dsResults = null;


            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "DownloadFaculty", null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "DownloadFaculty", null);
            }


            m_dtUserData = dsResults.Tables[0];


        }

        public void DownloadSchedule()
        {
            DataSet dsResults = null;


            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "DownloadSchedule", null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "DownloadSchedule", null);
            }


            m_dtUserData = dsResults.Tables[0];


        }

        public void DownloadMember()
        {
            DataSet dsResults = null;


            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "DownloadMember", null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "DownloadMember", null);
            }


            m_dtUserData = dsResults.Tables[0];


        }

        public int GetNumFaculty()
        {
            DataSet dsResults = null;
            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetNumFaculty", null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetNumFaculty", null);
            }

            return Convert.ToInt16(dsResults.Tables[0].Rows[0]["Number"]);
            
         }


        public int GetNumStudents()
        {
            DataSet dsResults = null;
            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "GetNumStudents", null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "GetNumStudents", null);
            }

            return Convert.ToInt16(dsResults.Tables[0].Rows[0]["Number"]);

        }

        public int SchedNumStudents()
        {
            DataSet dsResults = null;
            if (m_bTransactionSupport)
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "SchedNumStudents", null);
            }
            else
            {
                dsResults = m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "SchedNumStudents", null);
            }

            return Convert.ToInt16(dsResults.Tables[0].Rows[0]["Number"]);

        }

        public void UpdateFacultyDetails(
            int SchoolID,
            int Faculty,
            string Name,
            string Email,
            string Title,
            int WillAttend
            )
        {

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[6];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SchoolIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = SchoolID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FacultyIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Faculty;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@NameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Name;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@EmailIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Email;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TitleIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Title;
            idx += 1;


            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@WillAttendIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = WillAttend;



            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateFacultyDetails", arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateFacultyDetails", arrSqlParams);
            }


        }


        public void UpdateUserData(
               string LoginName,
               string Password,
               string SchoolName)
        {

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[3];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@LoginNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 20;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = LoginName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PasswordIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 20;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Password;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SchoolNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = SchoolName;
            idx += 1;


            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateUserData", arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateUserData", arrSqlParams);
            }


        }

        public void UpdateTimesData(
              int UserID,
              int BP,
              int a1,
              int a2,
              int a3,
              int a4)
        {

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[6];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = UserID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@BPHeatIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = BP;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@a1In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = a1;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@a2In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = a2;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@a3In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = a3;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@a4In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = a4;
            idx += 1;


            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateTimesData", arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateTimesData", arrSqlParams);
            }


        }


        public bool UpdateMemberDetails(
         int SchoolID,
         int Member,
         string FullName,
         string Email,
         string ShirtSize,
         string Education,
         string AreaofStudy,
         string ExpectedGraduation,
         int InvestmentBanking,
         int Trading,
         int Equities,
         int Derivatives,
            int RiskManagement,
            int PortfolioManagement,
            int Consulting,
            int FixedIncome,
            string Paragraph 
         )
        {


            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[17];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SchoolIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = SchoolID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@MemberIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Member;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FullNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = FullName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@EmailIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Email;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ShirtSizeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 10;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = ShirtSize;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@EducationIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 15;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Education;

            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@AreaofStudyIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = AreaofStudy;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ExpectedGraduationIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = ExpectedGraduation;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@InvestmentBankingIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = InvestmentBanking;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@TradingIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Trading;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@EquitiesIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Equities;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@DerivativesIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Derivatives;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@RiskManagementIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = RiskManagement;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@PortfolioManagementIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = PortfolioManagement;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ConsultingIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Consulting;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FixedIncomeIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = FixedIncome;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@ParagraphIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 10000;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Paragraph;

            

            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateMemberDetails", arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateMemberDetails", arrSqlParams);
            }



            return true;
        }

        public void UpdateStudentData(
               string School,
               string Name)
        {

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[2];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@SchoolIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = School;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@NameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Name;


            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateStudentData", arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateStudentData", arrSqlParams);
            }


        }

        public void UpdateRoles(
                    int UserID,
                    string FullName,
                    int BP,
                    int Credit,
                    int Quant,
                    int Cap,
                    int ST,
                    int Algo1,
                    int Algo2,
                    int Algo3,
                    int Algo4)
        {

            SqlParameter[] arrSqlParams;

            int idx = 0;

            arrSqlParams = new SqlParameter[11];

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@UserIDIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = UserID;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@FullNameIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.VarChar;
            arrSqlParams[idx].Size = 100;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = FullName;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@BPIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = BP;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CreditIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Credit;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@QuantIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Quant;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@CapIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Cap;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@STIn";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = ST;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@Algo1In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Algo1;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@Algo2In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Algo2;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@Algo3In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Algo3;
            idx += 1;

            arrSqlParams[idx] = new SqlParameter();
            arrSqlParams[idx].ParameterName = "@Algo4In";
            arrSqlParams[idx].SqlDbType = SqlDbType.Int;
            arrSqlParams[idx].Direction = ParameterDirection.Input;
            arrSqlParams[idx].Value = Algo4;
            idx += 1;


            if (m_bTransactionSupport)
            {
                m_dbMSSqlHelper.ExecuteDataset(this.m_objTransaction, "UpdateRoles", arrSqlParams);
            }
            else
            {
                m_dbMSSqlHelper.ExecuteDataset(base.m_conROTMAN, "UpdateRoles", arrSqlParams);
            }


        }



    }   //end public class MSSqlDataAccess
}
