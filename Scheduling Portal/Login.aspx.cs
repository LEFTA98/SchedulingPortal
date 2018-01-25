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
    public partial class Login : BasePage
    {
        #region Declarations
        private ArrayList m_errorList = new ArrayList();
        MSSqlDataAccess m_objDA = new MSSqlDataAccess();
        #endregion

        #region Constructors
        public Login() : base(false)
		{
        }
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

            this.lblErrorMsg.Text = string.Empty;

            Page.SetFocus(txtLoginName);

            if (!Page.IsPostBack)
            {
            }
            else
            {
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (m_errorList.Count > 0)
            {
                this.lblErrorMsg.Visible = true;
                this.lblErrorMsg.Text += base.AnyErrors(m_errorList);
            }
        }	//end OnPreRender
        #endregion

        #region Button Event Handlers
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
                return;
            }

            if (m_objDA.CheckLogin(this.txtLoginName.Text, this.txtPassword.Text))
            {
                DataRow dr = m_objDA.UserDataDT.Rows[0];
                //if (dr["UserStatus"].ToString().CompareTo("Y") == 0)
                //{
                    SessionManager.UserTypeID = m_objDA.ConvertUserType(Convert.ToInt16(dr["UserTypeID"]));
                    SessionManager.UserID = Convert.ToInt32(dr["UserID"]);
                    SessionManager.SchoolName = Convert.ToString(dr["SchoolName"]);

                    //below is just for debug
                    //string strMsg = string.Format("user {0} just logged in", this.txtLoginName.Text);
                    //Utilities.JustInfoNotification(strMsg, base.m_bSendInfoEmails);

                    //redirection
                    switch (SessionManager.UserTypeID)
                    {
                        case Enumerations.UserTypes.Admin:
                            SessionManager.AdminView = true;
                            Response.Redirect("~/Portal/Admin/MainControl.aspx", true);
                            break;
                        case Enumerations.UserTypes.Student:
                            SessionManager.AdminView = false;
                            Response.Redirect("~/Portal/Scheduling.aspx", true);
                            break;
                    }   //end switch
                //}
                //else
                //{
                //    this.m_errorList.Add("User status is not active");
                //}
            }
            else
            {
                this.m_errorList.Add("Login incorrect");
            }
        }

        #endregion

    }   //public partial class Login
}
