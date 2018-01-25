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

namespace RotmanTrading.Portal.Admin
{

    public partial class Registration : BasePage
    {
        #region Declarations
        private ArrayList m_errorList = new ArrayList();
        MSSqlDataAccess m_objDA = new MSSqlDataAccess();
        #endregion

        #region Constructors
        public Registration()
            : base(false)
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

            //Page.SetFocus(txtLoginName);

            if (!Page.IsPostBack)
            {
            }
            else
            {
            }

            School.Text = SessionManager.SchoolName;
            School.Font.Bold = true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                FillScreen();
            }	//end try
            finally
            {

                if (m_errorList.Count > 0)
                {
                    this.lblErrorMsg.Visible = true;
                    this.lblErrorMsg.Text += base.AnyErrors(m_errorList);
                }
            }
        }	//end OnPreRender
        #endregion


        private int ValueCheck(CheckBox Box)
        {
            if (Box.Checked)
                return 1;
            else
                return 0;
        }


        #region Button Event Handlers
        protected void buttonSaveValues_Click(object sender, EventArgs e)
        {
            //Perform validations
            Page.Validate();
            if (!Page.IsValid)
            {
                this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
                return;
            }


            string shirtsize1;
            if (Member1S.Checked)
            {
                shirtsize1="S";
            }
            else if (Member1M.Checked)
            {
                shirtsize1="M";
            }
            else if (Member1L.Checked)
            {
                shirtsize1="L";
            }
            else if (Member1XL.Checked)
            {
                shirtsize1="XL";
            }
            else
            {
                shirtsize1 = null;
            }

                        string shirtsize2;
            if (Member2S.Checked)
            {
                shirtsize2="S";
            }
            else if (Member2M.Checked)
            {
                shirtsize2="M";
            }
            else if (Member2L.Checked)
            {
                shirtsize2="L";
            }
            else if (Member2XL.Checked)
            {
                shirtsize2="XL";
            }
            else
            {
                shirtsize2 = null;
            }

                        string shirtsize3;
            if (Member3S.Checked)
            {
                shirtsize3="S";
            }
            else if (Member3M.Checked)
            {
                shirtsize3="M";
            }
            else if (Member3L.Checked)
            {
                shirtsize3="L";
            }
            else if (Member3XL.Checked)
            {
                shirtsize3="XL";
            }
            else
            {
                shirtsize3 = null;
            }

                        string shirtsize4;
            if (Member4S.Checked)
            {
                shirtsize4="S";
            }
            else if (Member4M.Checked)
            {
                shirtsize4="M";
            }
            else if (Member4L.Checked)
            {
                shirtsize4="L";
            }
            else if (Member4XL.Checked)
            {
                shirtsize4="XL";
            }
            else
            {
                shirtsize4 = null;
            }

                        string shirtsize5;
            if (Member5S.Checked)
            {
                shirtsize5="S";
            }
            else if (Member5M.Checked)
            {
                shirtsize5="M";
            }
            else if (Member5L.Checked)
            {
                shirtsize5="L";
            }
            else if (Member5XL.Checked)
            {
                shirtsize5="XL";
            }
            else
            {
                shirtsize5 = null;
            }


                        string shirtsize6;
            if (Member6S.Checked)
            {
                shirtsize6="S";
            }
            else if (Member6M.Checked)
            {
                shirtsize6="M";
            }
            else if (Member6L.Checked)
            {
                shirtsize6="L";
            }
            else if (Member6XL.Checked)
            {
                shirtsize6="XL";
            }
            else
            {
                shirtsize6 = null;
            }




            string education1;
            if (Member1MBA.Checked)
            {
                education1="MBA";
            }
            else if (Member1Graduate.Checked)
            {
                education1="Graduate";
            }
            else if (Member1Undergraduate.Checked)
            {
                education1="Undergraduate";
            }
            else
            {
                education1=null;
            }


                        string education2;
            if (Member2MBA.Checked)
            {
                education2="MBA";
            }
            else if (Member2Graduate.Checked)
            {
                education2="Graduate";
            }
            else if (Member2Undergraduate.Checked)
            {
                education2="Undergraduate";
            }
            else
            {
                education2=null;
            }

                        string education3;
            if (Member3MBA.Checked)
            {
                education3="MBA";
            }
            else if (Member3Graduate.Checked)
            {
                education3="Graduate";
            }
            else if (Member3Undergraduate.Checked)
            {
                education3="Undergraduate";
            }
            else
            {
                education3=null;
            }

                        string education4;
            if (Member4MBA.Checked)
            {
                education4="MBA";
            }
            else if (Member4Graduate.Checked)
            {
                education4="Graduate";
            }
            else if (Member4Undergraduate.Checked)
            {
                education4="Undergraduate";
            }
            else
            {
                education4=null;
            }

                        string education5;
            if (Member5MBA.Checked)
            {
                education5="MBA";
            }
            else if (Member5Graduate.Checked)
            {
                education5="Graduate";
            }
            else if (Member5Undergraduate.Checked)
            {
                education5="Undergraduate";
            }
            else
            {
                education5=null;
            }


                        string education6;
            if (Member6MBA.Checked)
            {
                education6="MBA";
            }
            else if (Member6Graduate.Checked)
            {
                education6="Graduate";
            }
            else if (Member6Undergraduate.Checked)
            {
                education6="Undergraduate";
            }
            else
            {
                education6=null;
            }



            try
            {
                //Enable transaction support
                m_objDA.TransactionEnable();
                m_objDA.TransactionBegin();



                m_objDA.UpdateFacultyDetails(SessionManager.UserID, 1, FacultyName.Text, FacultyEmail.Text, FacultyTitle.Text, ValueCheck(Attend));

                m_objDA.UpdateMemberDetails(SessionManager.UserID, 1, Member1Name.Text, Member1Email.Text, shirtsize1, education1, Member1AOS.Text, Member1Year.Text, ValueCheck(Member1IB), ValueCheck(Member1Trading),ValueCheck(Member1Equities),ValueCheck(Member1Derivatives),ValueCheck(Member1RM),ValueCheck(Member1PM),ValueCheck(Member1Consulting),ValueCheck(Member1FI), Member1Description.Text);
                m_objDA.UpdateMemberDetails(SessionManager.UserID, 2, Member2Name.Text, Member2Email.Text, shirtsize2, education2, Member2AOS.Text, Member2Year.Text, ValueCheck(Member2IB), ValueCheck(Member2Trading), ValueCheck(Member2Equities), ValueCheck(Member2Derivatives), ValueCheck(Member2RM), ValueCheck(Member2PM), ValueCheck(Member2Consulting), ValueCheck(Member2FI), Member2Description.Text);
                m_objDA.UpdateMemberDetails(SessionManager.UserID, 3, Member3Name.Text, Member3Email.Text, shirtsize3, education3, Member3AOS.Text, Member3Year.Text, ValueCheck(Member3IB), ValueCheck(Member3Trading), ValueCheck(Member3Equities), ValueCheck(Member3Derivatives), ValueCheck(Member3RM), ValueCheck(Member3PM), ValueCheck(Member3Consulting), ValueCheck(Member3FI), Member3Description.Text);
                m_objDA.UpdateMemberDetails(SessionManager.UserID, 4, Member4Name.Text, Member4Email.Text, shirtsize4, education4, Member4AOS.Text, Member4Year.Text, ValueCheck(Member4IB), ValueCheck(Member4Trading), ValueCheck(Member4Equities), ValueCheck(Member4Derivatives), ValueCheck(Member4RM), ValueCheck(Member4PM), ValueCheck(Member4Consulting), ValueCheck(Member4FI), Member4Description.Text);
                m_objDA.UpdateMemberDetails(SessionManager.UserID, 5, Member5Name.Text, Member5Email.Text, shirtsize5, education5, Member5AOS.Text, Member5Year.Text, ValueCheck(Member5IB), ValueCheck(Member5Trading), ValueCheck(Member5Equities), ValueCheck(Member5Derivatives), ValueCheck(Member5RM), ValueCheck(Member5PM), ValueCheck(Member5Consulting), ValueCheck(Member5FI), Member5Description.Text);
                m_objDA.UpdateMemberDetails(SessionManager.UserID, 6, Member6Name.Text, Member6Email.Text, shirtsize6, education6, Member6AOS.Text, Member6Year.Text, ValueCheck(Member6IB), ValueCheck(Member6Trading), ValueCheck(Member6Equities), ValueCheck(Member6Derivatives), ValueCheck(Member6RM), ValueCheck(Member6PM), ValueCheck(Member6Consulting), ValueCheck(Member6FI), Member6Description.Text);




                //commit db changes
                m_objDA.TransactionCommit();
                //display successful msg
                this.lblUserMessage.Text = "Profile information successfully updated";
            }

 
            //end try
            catch (Exception ex)
            {
                m_objDA.TransactionRollback();
                string strError = string.Format("Exception occurred in Registration.buttonSaveValues_Click ... {0}", ex.Message);
                this.m_errorList.Add(strError);
                Utilities.CriticalErrorNotification(base.AnyErrorsNoHTML(this.m_errorList), base.m_bSendErrorEmails);

            }
            finally
            {
            }
        }   //buttonSaveValues_Click
           
    
        #endregion

        #region Fill Screen
        private void FillScreen()
        {

            m_objDA.GetFacultyDetails(SessionManager.UserID);
            DataRow row = m_objDA.UserDataDT.Rows[0];
            FacultyName.Text = Convert.ToString(row["Name"]);
            FacultyEmail.Text = Convert.ToString(row["Email"]);
            FacultyTitle.Text = Convert.ToString(row["Title"]);
            Attend.Checked = (Convert.ToInt16(row["WillAttend"]) == 1);

            m_objDA.GetMemberDetails(SessionManager.UserID);
            DataRow row1 = m_objDA.UserDataDT.Rows[0];
            DataRow row2 = m_objDA.UserDataDT.Rows[1];
            DataRow row3 = m_objDA.UserDataDT.Rows[2];
            DataRow row4 = m_objDA.UserDataDT.Rows[3];
            DataRow row5 = m_objDA.UserDataDT.Rows[4];
            DataRow row6 = m_objDA.UserDataDT.Rows[5];

            Member1Name.Text = Convert.ToString(row1["FullName"]);
            Member1Email.Text = Convert.ToString(row1["Email"]);
            if (String.Compare(Convert.ToString(row1["ShirtSize"]), "S") == 0)
            {
                Member1S.Checked = true;
                Member1M.Checked = false;
                Member1L.Checked = false;
                Member1XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row1["ShirtSize"]), "M") == 0)
            {
                Member1S.Checked = false;
                Member1M.Checked = true;
                Member1L.Checked = false;
                Member1XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row1["ShirtSize"]), "L") == 0)
            {
                Member1S.Checked = false;
                Member1M.Checked = false;
                Member1L.Checked = true;
                Member1XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row1["ShirtSize"]), "XL") == 0)
            {
                Member1S.Checked = false;
                Member1M.Checked = false;
                Member1L.Checked = false;
                Member1XL.Checked = true;
            }
            else
            {
                Member1S.Checked = false;
                Member1M.Checked = false;
                Member1L.Checked = false;
                Member1XL.Checked = false;
            }

            if (String.Compare(Convert.ToString(row1["Education"]), "MBA") == 0)
            {
                Member1MBA.Checked = true;
                Member1Graduate.Checked = false;
                Member1Undergraduate.Checked = false;
            }

            else if (String.Compare(Convert.ToString(row1["Education"]), "Graduate") == 0)
            {
                Member1MBA.Checked = false;
                Member1Graduate.Checked = true;
                Member1Undergraduate.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row1["Education"]), "Undergraduate") == 0)
            {
                Member1MBA.Checked = false;
                Member1Graduate.Checked = false;
                Member1Undergraduate.Checked = true;
            }
            else
            {
                Member1MBA.Checked = false;
                Member1Graduate.Checked = false;
                Member1Undergraduate.Checked = false;
            }

            Member1AOS.Text = Convert.ToString(row1["AreaofStudy"]);
            Member1Year.Text = Convert.ToString(row1["ExpectedGraduation"]);

            if (Convert.ToInt16(row1["InvestmentBanking"]) == 1)
            {
                Member1IB.Checked = true;
            }
            else
            {
                Member1IB.Checked = false;
            }

            if (Convert.ToInt16(row1["Trading"]) == 1)
            {
                Member1Trading.Checked = true;
            }
            else
            {
                Member1Trading.Checked = false;
            }

            if (Convert.ToInt16(row1["Equities"]) == 1)
            {
                Member1Equities.Checked = true;
            }
            else
            {
                Member1Equities.Checked = false;
            }

            if (Convert.ToInt16(row1["Derivatives"]) == 1)
            {
                Member1Derivatives.Checked = true;
            }
            else
            {
                Member1Derivatives.Checked = false;
            }

            if (Convert.ToInt16(row1["RiskManagement"]) == 1)
            {
                Member1RM.Checked = true;
            }
            else
            {
                Member1RM.Checked = false;
            }

            if (Convert.ToInt16(row1["PortfolioManagement"]) == 1)
            {
                Member1PM.Checked = true;
            }
            else
            {
                Member1PM.Checked = false;
            }

            if (Convert.ToInt16(row1["Consulting"]) == 1)
            {
                Member1Consulting.Checked = true;
            }
            else
            {
                Member1Consulting.Checked = false;
            }

            if (Convert.ToInt16(row1["FixedIncome"]) == 1)
            {
                Member1FI.Checked = true;
            }
            else
            {
                Member1FI.Checked = false;
            }

            Member1Description.Text = Convert.ToString(row1["Paragraph"]);




            Member2Name.Text = Convert.ToString(row2["FullName"]);
            Member2Email.Text = Convert.ToString(row2["Email"]);
            if (String.Compare(Convert.ToString(row2["ShirtSize"]), "S") == 0)
            {
                Member2S.Checked = true;
                Member2M.Checked = false;
                Member2L.Checked = false;
                Member2XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row2["ShirtSize"]), "M") == 0)
            {
                Member2S.Checked = false;
                Member2M.Checked = true;
                Member2L.Checked = false;
                Member2XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row2["ShirtSize"]), "L") == 0)
            {
                Member2S.Checked = false;
                Member2M.Checked = false;
                Member2L.Checked = true;
                Member2XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row2["ShirtSize"]), "XL") == 0)
            {
                Member2S.Checked = false;
                Member2M.Checked = false;
                Member2L.Checked = false;
                Member2XL.Checked = true;
            }
            else
            {
                Member2S.Checked = false;
                Member2M.Checked = false;
                Member2L.Checked = false;
                Member2XL.Checked = false;
            }

            if (String.Compare(Convert.ToString(row2["Education"]), "MBA") == 0)
            {
                Member2MBA.Checked = true;
                Member2Graduate.Checked = false;
                Member2Undergraduate.Checked = false;
            }

            else if (String.Compare(Convert.ToString(row2["Education"]), "Graduate") == 0)
            {
                Member2MBA.Checked = false;
                Member2Graduate.Checked = true;
                Member2Undergraduate.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row2["Education"]), "Undergraduate") == 0)
            {
                Member2MBA.Checked = false;
                Member2Graduate.Checked = false;
                Member2Undergraduate.Checked = true;
            }
            else
            {
                Member2MBA.Checked = false;
                Member2Graduate.Checked = false;
                Member2Undergraduate.Checked = false;
            }

            Member2AOS.Text = Convert.ToString(row2["AreaofStudy"]);
            Member2Year.Text = Convert.ToString(row2["ExpectedGraduation"]);

            if (Convert.ToInt16(row2["InvestmentBanking"]) == 1)
            {
                Member2IB.Checked = true;
            }
            else
            {
                Member2IB.Checked = false;
            }

            if (Convert.ToInt16(row2["Trading"]) == 1)
            {
                Member2Trading.Checked = true;
            }
            else
            {
                Member2Trading.Checked = false;
            }

            if (Convert.ToInt16(row2["Equities"]) == 1)
            {
                Member2Equities.Checked = true;
            }
            else
            {
                Member2Equities.Checked = false;
            }

            if (Convert.ToInt16(row2["Derivatives"]) == 1)
            {
                Member2Derivatives.Checked = true;
            }
            else
            {
                Member2Derivatives.Checked = false;
            }

            if (Convert.ToInt16(row2["RiskManagement"]) == 1)
            {
                Member2RM.Checked = true;
            }
            else
            {
                Member2RM.Checked = false;
            }

            if (Convert.ToInt16(row2["PortfolioManagement"]) == 1)
            {
                Member2PM.Checked = true;
            }
            else
            {
                Member2PM.Checked = false;
            }

            if (Convert.ToInt16(row2["Consulting"]) == 1)
            {
                Member2Consulting.Checked = true;
            }
            else
            {
                Member2Consulting.Checked = false;
            }

            if (Convert.ToInt16(row2["FixedIncome"]) == 1)
            {
                Member2FI.Checked = true;
            }
            else
            {
                Member2FI.Checked = false;
            }

            Member2Description.Text = Convert.ToString(row2["Paragraph"]);



            Member3Name.Text = Convert.ToString(row3["FullName"]);
            Member3Email.Text = Convert.ToString(row3["Email"]);
            if (String.Compare(Convert.ToString(row3["ShirtSize"]), "S") == 0)
            {
                Member3S.Checked = true;
                Member3M.Checked = false;
                Member3L.Checked = false;
                Member3XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row3["ShirtSize"]), "M") == 0)
            {
                Member3S.Checked = false;
                Member3M.Checked = true;
                Member3L.Checked = false;
                Member3XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row3["ShirtSize"]), "L") == 0)
            {
                Member3S.Checked = false;
                Member3M.Checked = false;
                Member3L.Checked = true;
                Member3XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row3["ShirtSize"]), "XL") == 0)
            {
                Member3S.Checked = false;
                Member3M.Checked = false;
                Member3L.Checked = false;
                Member3XL.Checked = true;
            }
            else
            {
                Member3S.Checked = false;
                Member3M.Checked = false;
                Member3L.Checked = false;
                Member3XL.Checked = false;
            }

            if (String.Compare(Convert.ToString(row3["Education"]), "MBA") == 0)
            {
                Member3MBA.Checked = true;
                Member3Graduate.Checked = false;
                Member3Undergraduate.Checked = false;
            }

            else if (String.Compare(Convert.ToString(row3["Education"]), "Graduate") == 0)
            {
                Member3MBA.Checked = false;
                Member3Graduate.Checked = true;
                Member3Undergraduate.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row3["Education"]), "Undergraduate") == 0)
            {
                Member3MBA.Checked = false;
                Member3Graduate.Checked = false;
                Member3Undergraduate.Checked = true;
            }
            else
            {
                Member3MBA.Checked = false;
                Member3Graduate.Checked = false;
                Member3Undergraduate.Checked = false;
            }

            Member3AOS.Text = Convert.ToString(row3["AreaofStudy"]);
            Member3Year.Text = Convert.ToString(row3["ExpectedGraduation"]);

            if (Convert.ToInt16(row3["InvestmentBanking"]) == 1)
            {
                Member3IB.Checked = true;
            }
            else
            {
                Member3IB.Checked = false;
            }

            if (Convert.ToInt16(row3["Trading"]) == 1)
            {
                Member3Trading.Checked = true;
            }
            else
            {
                Member3Trading.Checked = false;
            }

            if (Convert.ToInt16(row3["Equities"]) == 1)
            {
                Member3Equities.Checked = true;
            }
            else
            {
                Member3Equities.Checked = false;
            }

            if (Convert.ToInt16(row3["Derivatives"]) == 1)
            {
                Member3Derivatives.Checked = true;
            }
            else
            {
                Member3Derivatives.Checked = false;
            }

            if (Convert.ToInt16(row3["RiskManagement"]) == 1)
            {
                Member3RM.Checked = true;
            }
            else
            {
                Member3RM.Checked = false;
            }

            if (Convert.ToInt16(row3["PortfolioManagement"]) == 1)
            {
                Member3PM.Checked = true;
            }
            else
            {
                Member3PM.Checked = false;
            }

            if (Convert.ToInt16(row3["Consulting"]) == 1)
            {
                Member3Consulting.Checked = true;
            }
            else
            {
                Member3Consulting.Checked = false;
            }

            if (Convert.ToInt16(row3["FixedIncome"]) == 1)
            {
                Member3FI.Checked = true;
            }
            else
            {
                Member3FI.Checked = false;
            }

            Member3Description.Text = Convert.ToString(row3["Paragraph"]);



            Member4Name.Text = Convert.ToString(row4["FullName"]);
            Member4Email.Text = Convert.ToString(row4["Email"]);
            if (String.Compare(Convert.ToString(row4["ShirtSize"]), "S") == 0)
            {
                Member4S.Checked = true;
                Member4M.Checked = false;
                Member4L.Checked = false;
                Member4XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row4["ShirtSize"]), "M") == 0)
            {
                Member4S.Checked = false;
                Member4M.Checked = true;
                Member4L.Checked = false;
                Member4XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row4["ShirtSize"]), "L") == 0)
            {
                Member4S.Checked = false;
                Member4M.Checked = false;
                Member4L.Checked = true;
                Member4XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row4["ShirtSize"]), "XL") == 0)
            {
                Member4S.Checked = false;
                Member4M.Checked = false;
                Member4L.Checked = false;
                Member4XL.Checked = true;
            }
            else
            {
                Member4S.Checked = false;
                Member4M.Checked = false;
                Member4L.Checked = false;
                Member4XL.Checked = false;
            }

            if (String.Compare(Convert.ToString(row4["Education"]), "MBA") == 0)
            {
                Member4MBA.Checked = true;
                Member4Graduate.Checked = false;
                Member4Undergraduate.Checked = false;
            }

            else if (String.Compare(Convert.ToString(row4["Education"]), "Graduate") == 0)
            {
                Member4MBA.Checked = false;
                Member4Graduate.Checked = true;
                Member4Undergraduate.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row4["Education"]), "Undergraduate") == 0)
            {
                Member4MBA.Checked = false;
                Member4Graduate.Checked = false;
                Member4Undergraduate.Checked = true;
            }
            else
            {
                Member4MBA.Checked = false;
                Member4Graduate.Checked = false;
                Member4Undergraduate.Checked = false;
            }

            Member4AOS.Text = Convert.ToString(row4["AreaofStudy"]);
            Member4Year.Text = Convert.ToString(row4["ExpectedGraduation"]);

            if (Convert.ToInt16(row4["InvestmentBanking"]) == 1)
            {
                Member4IB.Checked = true;
            }
            else
            {
                Member4IB.Checked = false;
            }

            if (Convert.ToInt16(row4["Trading"]) == 1)
            {
                Member4Trading.Checked = true;
            }
            else
            {
                Member4Trading.Checked = false;
            }

            if (Convert.ToInt16(row4["Equities"]) == 1)
            {
                Member4Equities.Checked = true;
            }
            else
            {
                Member4Equities.Checked = false;
            }

            if (Convert.ToInt16(row4["Derivatives"]) == 1)
            {
                Member4Derivatives.Checked = true;
            }
            else
            {
                Member4Derivatives.Checked = false;
            }

            if (Convert.ToInt16(row4["RiskManagement"]) == 1)
            {
                Member4RM.Checked = true;
            }
            else
            {
                Member4RM.Checked = false;
            }

            if (Convert.ToInt16(row4["PortfolioManagement"]) == 1)
            {
                Member4PM.Checked = true;
            }
            else
            {
                Member4PM.Checked = false;
            }

            if (Convert.ToInt16(row4["Consulting"]) == 1)
            {
                Member4Consulting.Checked = true;
            }
            else
            {
                Member4Consulting.Checked = false;
            }

            if (Convert.ToInt16(row4["FixedIncome"]) == 1)
            {
                Member4FI.Checked = true;
            }
            else
            {
                Member4FI.Checked = false;
            }

            Member4Description.Text = Convert.ToString(row4["Paragraph"]);




            Member5Name.Text = Convert.ToString(row5["FullName"]);
            Member5Email.Text = Convert.ToString(row5["Email"]);
            if (String.Compare(Convert.ToString(row5["ShirtSize"]), "S") == 0)
            {
                Member5S.Checked = true;
                Member5M.Checked = false;
                Member5L.Checked = false;
                Member5XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row5["ShirtSize"]), "M") == 0)
            {
                Member5S.Checked = false;
                Member5M.Checked = true;
                Member5L.Checked = false;
                Member5XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row5["ShirtSize"]), "L") == 0)
            {
                Member5S.Checked = false;
                Member5M.Checked = false;
                Member5L.Checked = true;
                Member5XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row5["ShirtSize"]), "XL") == 0)
            {
                Member5S.Checked = false;
                Member5M.Checked = false;
                Member5L.Checked = false;
                Member5XL.Checked = true;
            }
            else
            {
                Member5S.Checked = false;
                Member5M.Checked = false;
                Member5L.Checked = false;
                Member5XL.Checked = false;
            }

            if (String.Compare(Convert.ToString(row5["Education"]), "MBA") == 0)
            {
                Member5MBA.Checked = true;
                Member5Graduate.Checked = false;
                Member5Undergraduate.Checked = false;
            }

            else if (String.Compare(Convert.ToString(row5["Education"]), "Graduate") == 0)
            {
                Member5MBA.Checked = false;
                Member5Graduate.Checked = true;
                Member5Undergraduate.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row5["Education"]), "Undergraduate") == 0)
            {
                Member5MBA.Checked = false;
                Member5Graduate.Checked = false;
                Member5Undergraduate.Checked = true;
            }
            else
            {
                Member5MBA.Checked = false;
                Member5Graduate.Checked = false;
                Member5Undergraduate.Checked = false;
            }

            Member5AOS.Text = Convert.ToString(row5["AreaofStudy"]);
            Member5Year.Text = Convert.ToString(row5["ExpectedGraduation"]);

            if (Convert.ToInt16(row5["InvestmentBanking"]) == 1)
            {
                Member5IB.Checked = true;
            }
            else
            {
                Member5IB.Checked = false;
            }

            if (Convert.ToInt16(row5["Trading"]) == 1)
            {
                Member5Trading.Checked = true;
            }
            else
            {
                Member5Trading.Checked = false;
            }

            if (Convert.ToInt16(row5["Equities"]) == 1)
            {
                Member5Equities.Checked = true;
            }
            else
            {
                Member5Equities.Checked = false;
            }

            if (Convert.ToInt16(row5["Derivatives"]) == 1)
            {
                Member5Derivatives.Checked = true;
            }
            else
            {
                Member5Derivatives.Checked = false;
            }

            if (Convert.ToInt16(row5["RiskManagement"]) == 1)
            {
                Member5RM.Checked = true;
            }
            else
            {
                Member5RM.Checked = false;
            }

            if (Convert.ToInt16(row5["PortfolioManagement"]) == 1)
            {
                Member5PM.Checked = true;
            }
            else
            {
                Member5PM.Checked = false;
            }

            if (Convert.ToInt16(row5["Consulting"]) == 1)
            {
                Member5Consulting.Checked = true;
            }
            else
            {
                Member5Consulting.Checked = false;
            }

            if (Convert.ToInt16(row5["FixedIncome"]) == 1)
            {
                Member5FI.Checked = true;
            }
            else
            {
                Member5FI.Checked = false;
            }

            Member5Description.Text = Convert.ToString(row5["Paragraph"]);




            Member6Name.Text = Convert.ToString(row6["FullName"]);
            Member6Email.Text = Convert.ToString(row6["Email"]);
            if (String.Compare(Convert.ToString(row6["ShirtSize"]), "S") == 0)
            {
                Member6S.Checked = true;
                Member6M.Checked = false;
                Member6L.Checked = false;
                Member6XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row6["ShirtSize"]), "M") == 0)
            {
                Member6S.Checked = false;
                Member6M.Checked = true;
                Member6L.Checked = false;
                Member6XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row6["ShirtSize"]), "L") == 0)
            {
                Member6S.Checked = false;
                Member6M.Checked = false;
                Member6L.Checked = true;
                Member6XL.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row6["ShirtSize"]), "XL") == 0)
            {
                Member6S.Checked = false;
                Member6M.Checked = false;
                Member6L.Checked = false;
                Member6XL.Checked = true;
            }
            else
            {
                Member6S.Checked = false;
                Member6M.Checked = false;
                Member6L.Checked = false;
                Member6XL.Checked = false;
            }

            if (String.Compare(Convert.ToString(row6["Education"]), "MBA") == 0)
            {
                Member6MBA.Checked = true;
                Member6Graduate.Checked = false;
                Member6Undergraduate.Checked = false;
            }

            else if (String.Compare(Convert.ToString(row6["Education"]), "Graduate") == 0)
            {
                Member6MBA.Checked = false;
                Member6Graduate.Checked = true;
                Member6Undergraduate.Checked = false;
            }
            else if (String.Compare(Convert.ToString(row6["Education"]), "Undergraduate") == 0)
            {
                Member6MBA.Checked = false;
                Member6Graduate.Checked = false;
                Member6Undergraduate.Checked = true;
            }
            else
            {
                Member6MBA.Checked = false;
                Member6Graduate.Checked = false;
                Member6Undergraduate.Checked = false;
            }

            Member6AOS.Text = Convert.ToString(row6["AreaofStudy"]);
            Member6Year.Text = Convert.ToString(row6["ExpectedGraduation"]);

            if (Convert.ToInt16(row6["InvestmentBanking"]) == 1)
            {
                Member6IB.Checked = true;
            }
            else
            {
                Member6IB.Checked = false;
            }

            if (Convert.ToInt16(row6["Trading"]) == 1)
            {
                Member6Trading.Checked = true;
            }
            else
            {
                Member6Trading.Checked = false;
            }

            if (Convert.ToInt16(row6["Equities"]) == 1)
            {
                Member6Equities.Checked = true;
            }
            else
            {
                Member6Equities.Checked = false;
            }

            if (Convert.ToInt16(row6["Derivatives"]) == 1)
            {
                Member6Derivatives.Checked = true;
            }
            else
            {
                Member6Derivatives.Checked = false;
            }

            if (Convert.ToInt16(row6["RiskManagement"]) == 1)
            {
                Member6RM.Checked = true;
            }
            else
            {
                Member6RM.Checked = false;
            }

            if (Convert.ToInt16(row6["PortfolioManagement"]) == 1)
            {
                Member6PM.Checked = true;
            }
            else
            {
                Member6PM.Checked = false;
            }

            if (Convert.ToInt16(row6["Consulting"]) == 1)
            {
                Member6Consulting.Checked = true;
            }
            else
            {
                Member6Consulting.Checked = false;
            }

            if (Convert.ToInt16(row6["FixedIncome"]) == 1)
            {
                Member6FI.Checked = true;
            }
            else
            {
                Member6FI.Checked = false;
            }

            Member6Description.Text = Convert.ToString(row6["Paragraph"]);


        }





        #endregion

    }   //public partial class Registration
}