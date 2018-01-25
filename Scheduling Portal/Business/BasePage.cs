using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RotmanTrading.Business
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        #region Declarations
        public bool m_bSendInfoEmails = false;
        public bool m_bSendErrorEmails = false;
        public bool m_bSendDebugEmails = false;

        protected const string SUBMIT_BUTTON_MESSAGE = "Please wait ...";
        private const string UNAUTHORIZED_ACCESS_REDIRECTION = "~/Login.aspx";
        public const string APPLICATION_OUTCRY_RUNNING = "OutcryRunning";
        public const string APPLICATION_OUTCRY_START_TIME = "OutcryStartTime";
        public const string APPLICATION_OUTCRY_ELAPSED_TIME = "OutcryElapsedTime";

        private const string HEAT_HEADER = "Heat ";
        private const string SUB_HEAT_HEADER = "Sub Heat ";
        private const string SUB_HEAT_RANK_HEADER = "Sub Heat Rank";
        private const string AVERAGE_HEADER = "Average Rank";
         
        #endregion

        #region Constructors
        public BasePage() : base()
        {
            SetupSendEmails();
            SetProperties();
        }

        public BasePage(bool bRunClientOnload) : base()
        {
            SetupSendEmails();
            SetProperties();
            if (bRunClientOnload)
            {
                RunClientOnload();
            }
        }
        #endregion

        #region Properties
        public string HeatHeader { get; set; }
        public string SubheatHeader { get; set; }
        public string SubheatRankHeader { get; set; }
        #endregion

        #region Support
        private void SetProperties()
        {
            if (GameSettings.GameImplementation == GameSettings.GameImplementations.Regular)
            {
                HeatHeader = HEAT_HEADER;
                SubheatHeader = SUB_HEAT_HEADER;
                SubheatRankHeader = SUB_HEAT_RANK_HEADER;
            }
            else if (GameSettings.GameImplementation == GameSettings.GameImplementations.August082012)
            {
                HeatHeader = HEAT_HEADER;
                SubheatHeader = HEAT_HEADER;
                SubheatRankHeader = AVERAGE_HEADER;
            }
            else if (GameSettings.GameImplementation == GameSettings.GameImplementations.February2013)
            {
                HeatHeader = HEAT_HEADER;
                SubheatHeader = SUB_HEAT_HEADER;
                SubheatRankHeader = SUB_HEAT_RANK_HEADER;
            }
            else if (GameSettings.GameImplementation == GameSettings.GameImplementations.February2014)
            {
                HeatHeader = HEAT_HEADER;
                SubheatHeader = SUB_HEAT_HEADER;
                SubheatRankHeader = SUB_HEAT_RANK_HEADER;
            }
        }

        private void SetupSendEmails()
        {
            m_bSendInfoEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendInfoEmails"]);
            m_bSendErrorEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendErrorEmails"]);
            m_bSendDebugEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendDebugEmails"]);
        }

        private void RunClientOnload()
        {
            StringBuilder sb = new StringBuilder();
            ClientScriptManager objCSM = this.ClientScript;

            if (!objCSM.IsStartupScriptRegistered("clientPageOnLoad"))
            {
                sb.Append("<SCRIPT FOR='window' EVENT='onload()' LANGUAGE='JavaScript'>\r\n");
                sb.Append("PageOnload();\r\n");

                sb.Append("</SCRIPT>\r\n");
                objCSM.RegisterStartupScript(this.GetType(), "clientPageOnLoad", sb.ToString(), false);
                sb.Remove(0, sb.Length);
            }
        }   //end RunClientOnload

        public void PageUnauthorizedAccess()
        {
            SessionManager.UserTypeID = Enumerations.UserTypes.NotDefined;

            //below is just for debug
            string strMsg = string.Format("Page Unauthorized Access, UserTypeID = [{0}]", SessionManager.UserTypeID);
            Utilities.JustInfoNotification(strMsg, m_bSendInfoEmails);

            Response.Redirect(BasePage.UNAUTHORIZED_ACCESS_REDIRECTION, true);
        }
        #endregion

        #region Common Methods
        public string DetermineRank(int iRankValue)
        {
            string strClass = "norank";

            switch (iRankValue)
            {
                case 1:
                    strClass = "rank1";
                    break;
                case 2:
                    strClass = "rank2";
                    break;
                case 3:
                    strClass = "rank3";
                    break;
                case 4:
                    strClass = "rank4";
                    break;
                case 5:
                    strClass = "rank5";
                    break;
                default:
                    strClass = "norank";
                    break;
            }
            return strClass;
        }   //DetermineRank

        public Color DetermineRankColor(int iRankValue, Color objCurrentBackColor)
        {
            Color objColor = objCurrentBackColor;

            switch (iRankValue)
            {
                case 1:
                    objColor = Color.FromArgb(255, 204, 000);   //#FFCC00
                    break;
                case 2:
                    objColor = Color.FromArgb(204, 204, 204);   //#CCCCCC
                    break;
                case 3:
                    objColor = Color.FromArgb(204, 153, 000);   //#CC9900
                    break;
                case 4:
                    objColor = Color.FromArgb(075, 191, 103);   //#4BBF67
                    break;
                case 5:
                    objColor = Color.FromArgb(070, 160, 228);   //#46A0E4
                    break;
                default:
                    objColor = objCurrentBackColor;
                    break;
            }
            return objColor;
        }

        public string GetHeatNumber(Enumerations.GameIDs eGameID)
        {
            return GetHeatNumber((int)eGameID);
        }

        public string GetHeatNumber(int iGameID)
	    {

            //if (GameSettings.GameImplementation == GameSettings.GameImplementations.Regular)
            //{
            //    switch (iGameID)
            //    {
            //        case (int)Enumerations.GameIDs.Commod1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod4:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod5:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod6:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT12:
            //            strHeatNumber = "2 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.OO1Final:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.OO2Final:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.QO1Final:
            //            strHeatNumber = "1 - 1";    //was 1 - 3
            //            break;
            //        case (int)Enumerations.GameIDs.QO2Final:
            //            strHeatNumber = "1 - 2";    //was 1 - 4
            //            break;
            //        case (int)Enumerations.GameIDs.CR1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.CR2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.CR6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.CR7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.CR8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.CR12:
            //            strHeatNumber = "2 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED12:
            //            strHeatNumber = "2 - 6";
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //else if (GameSettings.GameImplementation == GameSettings.GameImplementations.August082012)
            //{
            //    switch (iGameID)
            //    {
            //        case (int)Enumerations.GameIDs.Commod1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        //case (int)Enumerations.GameIDs.Commod6:
            //        //    strHeatNumber = "2 - 3";
            //        //    break;
            //        case (int)Enumerations.GameIDs.SandT1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT7:
            //            strHeatNumber = "1 - 7";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT8:
            //            strHeatNumber = "1 - 8";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT9:
            //            strHeatNumber = "1 - 9";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT10:
            //            strHeatNumber = "1 - 10";
            //            break;
            //        //case (int)Enumerations.GameIDs.SandT11:
            //        //    strHeatNumber = "2 - 5";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.SandT12:
            //        //    strHeatNumber = "2 - 6";
            //        //    break;
            //        case (int)Enumerations.GameIDs.OO1Final:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.OO2Final:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.QO1Final:
            //            strHeatNumber = "1 - 1";    //was 1 - 3
            //            break;
            //        case (int)Enumerations.GameIDs.QO2Final:
            //            strHeatNumber = "1 - 2";    //was 1 - 4
            //            break;
            //        case (int)Enumerations.GameIDs.CR1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.CR2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.CR6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.CR7:
            //            strHeatNumber = "1 - 7";
            //            break;
            //        case (int)Enumerations.GameIDs.CR8:
            //            strHeatNumber = "1 - 8";
            //            break;
            //        case (int)Enumerations.GameIDs.CR9:
            //            strHeatNumber = "1 - 9";
            //            break;
            //        case (int)Enumerations.GameIDs.CR10:
            //            strHeatNumber = "1 - 10";
            //            break;
            //        //case (int)Enumerations.GameIDs.CR11:
            //        //    strHeatNumber = "2 - 5";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.CR12:
            //        //    strHeatNumber = "2 - 6";
            //        //    break;
            //        case (int)Enumerations.GameIDs.TRQED1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        //case (int)Enumerations.GameIDs.TRQED6:
            //        //    strHeatNumber = "1 - 6";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED7:
            //        //    strHeatNumber = "2 - 1";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED8:
            //        //    strHeatNumber = "2 - 2";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED9:
            //        //    strHeatNumber = "2 - 3";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED10:
            //        //    strHeatNumber = "2 - 4";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED11:
            //        //    strHeatNumber = "2 - 5";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED12:
            //        //    strHeatNumber = "2 - 6";
            //        //    break;
            //        default:
            //            break;
            //    }
            //}
            //else if (GameSettings.GameImplementation == GameSettings.GameImplementations.February2013)
            //{
            //    //Swapping BPCommodity & Thomson Reuters
            //    switch (iGameID)
            //    {
            //        //Thomson Reuters (6)
            //        case (int)Enumerations.GameIDs.Commod1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod4:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod5:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod6:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        //Sales & Trader (10)
            //        case (int)Enumerations.GameIDs.SandT1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        //case (int)Enumerations.GameIDs.SandT6:
            //        //    strHeatNumber = "";
            //        //    break;
            //        case (int)Enumerations.GameIDs.SandT7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        //case (int)Enumerations.GameIDs.SandT12:
            //        //    strHeatNumber = "2 - 6";
            //        //    break;

            //        case (int)Enumerations.GameIDs.OO1Final:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.OO2Final:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.QO1Final:
            //            strHeatNumber = "1 - 1";    //was 1 - 3
            //            break;
            //        case (int)Enumerations.GameIDs.QO2Final:
            //            strHeatNumber = "1 - 2";    //was 1 - 4
            //            break;
            //        case (int)Enumerations.GameIDs.CR1:
            //            strHeatNumber = "1 - 1";
            //            break;

            //        //CreditRisk/Options (10)
            //        case (int)Enumerations.GameIDs.CR2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        //case (int)Enumerations.GameIDs.CR6:
            //        //    strHeatNumber = "";
            //        //    break;
            //        case (int)Enumerations.GameIDs.CR7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.CR8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        //case (int)Enumerations.GameIDs.CR12:
            //        //    strHeatNumber = "2 - 6";
            //        //    break;

            //        //BP Commodities (8)
            //        case (int)Enumerations.GameIDs.TRQED1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED7:
            //            strHeatNumber = "1 - 7";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED8:
            //            strHeatNumber = "1 - 8";
            //            break;
            //        //case (int)Enumerations.GameIDs.TRQED9:
            //        //    strHeatNumber = "2 - 3";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED10:
            //        //    strHeatNumber = "2 - 4";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED11:
            //        //    strHeatNumber = "2 - 5";
            //        //    break;
            //        //case (int)Enumerations.GameIDs.TRQED12:
            //        //    strHeatNumber = "2 - 6";
            //        //    break;
            //        default:
            //            break;
            //    }
            //}
            //else if (GameSettings.GameImplementation == GameSettings.GameImplementations.February2014)
            //{
            //    switch (iGameID)
            //    {
            //        //Thomson Reuters (6)
            //        case (int)Enumerations.GameIDs.Commod1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod7:
            //            strHeatNumber = "1 - 7";
            //            break;
            //        case (int)Enumerations.GameIDs.Commod8:
            //            strHeatNumber = "1 - 8";
            //            break;
            //        //Sales & Trader (10)
            //        case (int)Enumerations.GameIDs.SandT1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.SandT12:
            //            strHeatNumber = "2 - 6";
            //            break;

            //        case (int)Enumerations.GameIDs.OO1Final:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.OO2Final:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.QO1Final:
            //            strHeatNumber = "1 - 1";    //was 1 - 3
            //            break;
            //        case (int)Enumerations.GameIDs.QO2Final:
            //            strHeatNumber = "1 - 2";    //was 1 - 4
            //            break;
            //        case (int)Enumerations.GameIDs.CR1:
            //            strHeatNumber = "1 - 1";
            //            break;

            //        //CreditRisk/Options (10)
            //        case (int)Enumerations.GameIDs.CR2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.CR6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.CR7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.CR8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.CR9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.CR10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.CR11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.CR12:
            //            strHeatNumber = "2 - 6";
            //            break;

            //        //TRQED (10)
            //        case (int)Enumerations.GameIDs.TRQED1:
            //            strHeatNumber = "1 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED2:
            //            strHeatNumber = "1 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED3:
            //            strHeatNumber = "1 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED4:
            //            strHeatNumber = "1 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED5:
            //            strHeatNumber = "1 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED6:
            //            strHeatNumber = "1 - 6";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED7:
            //            strHeatNumber = "2 - 1";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED8:
            //            strHeatNumber = "2 - 2";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED9:
            //            strHeatNumber = "2 - 3";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED10:
            //            strHeatNumber = "2 - 4";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED11:
            //            strHeatNumber = "2 - 5";
            //            break;
            //        case (int)Enumerations.GameIDs.TRQED12:
            //            strHeatNumber = "2 - 6";
            //            break;
            //        default:
            //            break;
            //    }

            return "";
            }
          //GetHeatNumber

        public string GetSubHeatPageTitle(Enumerations.GameIDs eGameID)
        {
            return GetSubHeatPageTitle((int)eGameID);
        }

        public string GetSubHeatPageTitle(int iGameID)
        {

            return "";
        }   //GetSubHeatPageTitle

        public string AnyErrors(ArrayList errorList)
        {
            StringBuilder sb = new StringBuilder();
            if (errorList.Count != 0)
            {
                sb.Append("<table border=0 cellPadding=0 cellSpacing=2 width='100%'>");
                for (int idx = 0; idx < errorList.Count; idx++)
                {
                    sb.AppendFormat("<tr><td><font color=red>{0}</font></td></tr>", errorList[idx].ToString());
                }	//end for
                sb.Append("</table>");
            }
            return sb.ToString();
        }	//end function AnyErrors

        public string AnyErrorsNoHTML(ArrayList errorList)
        {
            StringBuilder sb = new StringBuilder();
            if (errorList.Count != 0)
            {
                for (int idx = 0; idx < errorList.Count; idx++)
                {
                    sb.AppendFormat("({0}) {1}", idx + 1,errorList[idx].ToString());
                }	//end for
            }
            return sb.ToString();
        }

        /// <summary>
        /// Method: SetSelectedItemByValue
        /// This function sets the selected item for a ListControl.
        //  Note:  a return value is required if DataBinding syntax is used,
        //  so i'm returning an empty string.
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="strValue"></param>
        /// <param name="bClearPrevious"></param>
        /// <returns></returns>
        public string SetSelectedItemByValue(ListControl objList,
            string strValue,
            bool bClearPrevious)
        {
            string strReturn = String.Empty;
            if (bClearPrevious)
            {
                objList.ClearSelection();
            }
            if (strValue != null)
            {
                for (int idx = 0; idx <= objList.Items.Count - 1; idx++)
                {
                    if (objList.Items[idx].Value.Trim().ToLower().CompareTo(strValue.Trim().ToLower()) == 0)
                    {
                        objList.Items[idx].Selected = true;
                        break;
                    }
                }	//end for
            }
            return strReturn;
        }	//end SetSelectedItemByValue

        public string RemoveSelectedItemByValue(ListControl objList, string strValue)
        {
            string strReturn = String.Empty;
            if (strValue != null)
            {
                for (int idx = 0; idx <= objList.Items.Count - 1; idx++)
                {
                    if (objList.Items[idx].Value.Trim().ToLower().CompareTo(strValue.Trim().ToLower()) == 0)
                    {
                        objList.Items.RemoveAt(idx);
                        break;
                    }
                }	//end for
            }
            return strReturn;
        }	//end RemoveSelectedItemByValue

        #endregion

        #region File I/O
        public void DeleteImageFile(string strFileName)
        {
            string strFolder = ConfigurationManager.AppSettings["TicketImagesUploadFolder"];
            string strFilePath = string.Format("{0}\\{1}", strFolder, strFileName);
            if (File.Exists(strFilePath))
            {
                System.IO.File.Delete(strFilePath);
            }
        }

        #endregion

        #region Drop-down Lists
        public void CreateGamesDropdown(DropDownList ddlGames, int iSelectedValue)
        {
            ddlGames.Items.Insert(0, new ListItem("-- Select Game --", "-1"));
            ddlGames.Items.Insert(1, new ListItem(string.Format("Game {0}", (int)Enumerations.GameIDs.OO1Final), Convert.ToString((int)Enumerations.GameIDs.OO1Final)));
            ddlGames.Items.Insert(2, new ListItem(string.Format("Game {0}", (int)Enumerations.GameIDs.OO2Final), Convert.ToString((int)Enumerations.GameIDs.OO2Final)));
            ddlGames.Items.Insert(3, new ListItem(string.Format("Game {0}", (int)Enumerations.GameIDs.QO1Final), Convert.ToString((int)Enumerations.GameIDs.QO1Final)));
            ddlGames.Items.Insert(4, new ListItem(string.Format("Game {0}", (int)Enumerations.GameIDs.QO2Final), Convert.ToString((int)Enumerations.GameIDs.QO2Final)));

            int iSelectedIndex = 0;
            switch (iSelectedValue)
            {
                case (int)Enumerations.GameIDs.NotDefined:
                    iSelectedIndex = 0;
                    break;
                case (int)Enumerations.GameIDs.OO1Final:
                    iSelectedIndex = 1;
                    break;
                case (int)Enumerations.GameIDs.OO2Final:
                    iSelectedIndex = 2;
                    break;
                case (int)Enumerations.GameIDs.QO1Final:
                    iSelectedIndex = 3;
                    break;
                case (int)Enumerations.GameIDs.QO2Final:
                    iSelectedIndex = 4;
                    break;
                default:
                    iSelectedIndex = 0;
                    break;
            }

            ddlGames.SelectedIndex = iSelectedIndex;
        }

        public void CreateUserStatusesDropdown(DropDownList ddlUserStatuses, string strSelectedValue)
        {
            ddlUserStatuses.Items.Insert(0, new ListItem("-- Select User Status --", "-1"));
            ddlUserStatuses.Items.Insert(1, new ListItem("Active", "Y"));
            ddlUserStatuses.Items.Insert(2, new ListItem("InActive", "N"));

            int iSelectedIndex = 0;
            switch (strSelectedValue)
            {
                case "-1":
                    iSelectedIndex = 0;
                    break;
                case "Y":
                    iSelectedIndex = 1;
                    break;
                case "N":
                    iSelectedIndex = 2;
                    break;
            }

            ddlUserStatuses.SelectedIndex = iSelectedIndex;
        }

        #endregion

    }   //end public class BasePage
}
