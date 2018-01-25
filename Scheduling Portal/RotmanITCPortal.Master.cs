using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RotmanTrading.Business;

namespace RotmanTrading
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public partial class RotmanITCPortal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResetMenu = false;

            if (SessionManager.UserTypeID == Enumerations.UserTypes.Admin)
            {
                this.tblPortalAdminMenu.Visible = true;
            }
            else if (SessionManager.UserTypeID == Enumerations.UserTypes.Ticket)
            {
                this.tblPortalOutcryAdminMenu.Visible = true;
            }
            else if (SessionManager.UserTypeID == Enumerations.UserTypes.Student)
            {
                this.tblTeamMenu.Visible = true;
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;
            string strVersion = string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            lblDev.Text = string.Format("<!-- Developed by ESP Technologies (http://www.esptech.com) Version: {0} -->", strVersion);
        }

        public bool ResetMenu
        {
            set
            {
                this.tblPortalAdminMenu.Visible = value;
                this.tblTeamMenu.Visible = value;
                this.tblPortalOutcryAdminMenu.Visible = value;
            }
        }

    }   //public partial class RotmanITCPortal
}
