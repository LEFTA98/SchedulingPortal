using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
    public partial class Logout : BasePage
    {
        #region Declarations
        #endregion

        #region Constructors
        public Logout() : base(false)
		{
        }
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

            SessionManager.UserTypeID = Enumerations.UserTypes.NotDefined;
            SessionManager.AdminView = false;

            //You can terminate the session manually by calling Session.Abandon, but be
            //aware this will cause the Session_End event to not be called. 
            Page.Session.Abandon();

            Response.Redirect("~/Login.aspx", true);
        }
        #endregion

    }   //public partial class Logout
}
