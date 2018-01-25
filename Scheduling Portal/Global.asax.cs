using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using RotmanTrading.Business;

namespace RotmanTrading
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        private bool m_bSendDebugEmails = false;

        protected void Application_Start(object sender, EventArgs e)
        {
            m_bSendDebugEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendDebugEmails"]);
            Application["OutcryRunning"] = false;
            Application["OutcryStartTime"] = DateTime.MinValue;
            Application["OutcryElapsedTime"] = 0;

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;
            string strVersion = string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            string strNameAndVersion = string.Format("{0} Version: {1}", assemblyName.Name, strVersion);
            Utilities.WebApplicationNameAndVersion = strNameAndVersion;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            SessionManager.UserTypeID = Enumerations.UserTypes.NotDefined;
            SessionManager.UserID = -1;
            SessionManager.RunOutcry = false;
            SessionManager.ToHappenRowCount = 0;
            SessionManager.SearchXactionsWithNoTicketsSelected = false;
            SessionManager.SearchSerialNumberSelected = false;
            SessionManager.SearchFilenameSelected = false;
            SessionManager.SearchTeamCodeGameNumberSelected = false;
            SessionManager.SearchFindAllTicketsSelected = false;

            this.Session.Timeout = 90;  //in minutes

            //below is just for debug
            Utilities.JustInfoNotification("session starting", m_bSendDebugEmails);
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            //Note: Any changes made here also need to be made in logout.aspx.cs.
            //SessionManager.UserTypeID = Enumerations.UserTypes.NotDefined;
            //SessionManager.AdminView = false;
            //below is just for debug
            Utilities.JustInfoNotification("session ending", m_bSendDebugEmails);
        }

    }
}