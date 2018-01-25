using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RotmanTrading.Business;
using RotmanTrading.Portal;

namespace RotmanTrading.Portal.Admin
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// 2/2011 changes: Change "Credit Risk" to "Interest Rates" and add "Thomson
    /// QED" so we can weight the new case in the final score.
    /// </summary>
    public partial class MainControl : BasePage
    {
        #region Declarations
        private ArrayList m_errorList = new ArrayList();
        MSSqlDataAccess m_objDA = new MSSqlDataAccess();
        #endregion

        #region Constructors
        public MainControl()
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

            //Page access control
            if (SessionManager.UserTypeID != Enumerations.UserTypes.Admin)
            {
                base.PageUnauthorizedAccess();
            }

            lblErrorMsg.Text = string.Empty;
            lblUserMessage.Text = string.Empty;

        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                
            }	//end try
            finally
            {
                if (m_errorList.Count > 0)
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text += AnyErrors(m_errorList);
                }
            }	//end finally
        }	//end OnPreRender
        #endregion

 
        #region Button Event Handlers

        protected void savestudent_Click(object sender, EventArgs e)
        {
            //Perform validations
            Page.Validate();
            if (!Page.IsValid)
            {
                this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
                return;
            }

            if (filestudent != null)
            {
                if (filestudent.PostedFile.ContentLength != 0)
                {


                    try
                    {
                        string strLineData;
                        string[] arrImportData;
                        string strDelim = ",";
                        char[] arrDelim = strDelim.ToCharArray();
                        StreamReader objSR;
                        Stream objStream;
                        objStream = filestudent.FileContent;
                        objSR = new StreamReader(objStream);
                        objSR.BaseStream.Seek(0, SeekOrigin.Begin);
                        objSR.BaseStream.Position = 0;
                        //read process line-by-line; loop until all lines have been read & imported.
                        //Enable transaction support
                        m_objDA.TransactionEnable();
                        m_objDA.TransactionBegin();
                        int idx = 0;

                        while (objSR.Peek() >= 0)
                        {
                            strLineData = objSR.ReadLine();
                            arrImportData = strLineData.Split(arrDelim);

                            // skip first line
                            if (idx == 0)
                            {
                                strLineData = objSR.ReadLine();
                                arrImportData = strLineData.Split(arrDelim);
                                idx += 1;
                            }

                            m_objDA.UpdateStudentData(arrImportData[0], arrImportData[1]);

                        }
                        m_objDA.TransactionCommit();
                        //save file

                        //display successful msg
                        this.lblUserMessage.Text = "Student List Updated";

                    }

                    //end try
                    catch (Exception ex)
                    {
                        m_objDA.TransactionRollback();
                        string strError = string.Format("Exception occurred in MainControl.savestudent_Click ... {0}", ex.Message);
                        this.m_errorList.Add(strError);
                        Utilities.CriticalErrorNotification(base.AnyErrorsNoHTML(this.m_errorList), base.m_bSendErrorEmails);

                    }
                    finally
                    {
                    }
                }
            }
        }   //savestudent_Click

        protected void buttonSaveValues_Click(object sender, EventArgs e)
        {
            //Perform validations
            Page.Validate();
            if (!Page.IsValid)
            {
                this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
                return;
            }

            if (flucsv != null)
            {
                if (flucsv.PostedFile.ContentLength != 0)
                {


                    try
                    {
                        string strLineData;
                        string[] arrImportData;
                        string strDelim = ",";
                        char[] arrDelim = strDelim.ToCharArray();
                        StreamReader objSR;
                        Stream objStream;
                        objStream = flucsv.FileContent;
                        objSR = new StreamReader(objStream);
                        objSR.BaseStream.Seek(0, SeekOrigin.Begin);
                        objSR.BaseStream.Position = 0;
                        //read process line-by-line; loop until all lines have been read & imported.
                        //Enable transaction support
                        m_objDA.TransactionEnable();
                        m_objDA.TransactionBegin();
                        int idx = 0;

                        while (objSR.Peek() >= 0)
                        {
                            strLineData = objSR.ReadLine();
                            arrImportData = strLineData.Split(arrDelim);

                            // skip first line
                            if (idx == 0)
                            {
                                strLineData = objSR.ReadLine();
                                arrImportData = strLineData.Split(arrDelim);
                                idx += 1;
                            }

                            m_objDA.UpdateUserData(arrImportData[0], arrImportData[1], arrImportData[2]);

                        }
                        m_objDA.TransactionCommit();
                        //save file

                        //display successful msg
                        this.lblUserMessage.Text = "User List Updated";

                    }

                    //end try
                    catch (Exception ex)
                    {
                        m_objDA.TransactionRollback();
                        string strError = string.Format("Exception occurred in MainControl.buttonSaveValues_Click ... {0}", ex.Message);
                        this.m_errorList.Add(strError);
                        Utilities.CriticalErrorNotification(base.AnyErrorsNoHTML(this.m_errorList), base.m_bSendErrorEmails);

                    }
                    finally
                    {
                    }
                }
            }
        }   //buttonSaveValues_Click



        protected void savebpalgotimes_Click(object sender, EventArgs e)
        {
            //Perform validations
            Page.Validate();
            if (!Page.IsValid)
            {
                this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
                return;
            }

            if (bpalgotimesfile != null)
            {
                if (bpalgotimesfile.PostedFile.ContentLength != 0)
                {


                    try
                    {
                        string strLineData;
                        string[] arrImportData;
                        string strDelim = ",";
                        char[] arrDelim = strDelim.ToCharArray();
                        StreamReader objSR;
                        Stream objStream;
                        objStream = bpalgotimesfile.FileContent;
                        objSR = new StreamReader(objStream);
                        objSR.BaseStream.Seek(0, SeekOrigin.Begin);
                        objSR.BaseStream.Position = 0;
                        //read process line-by-line; loop until all lines have been read & imported.
                        //Enable transaction support
                        m_objDA.TransactionEnable();
                        m_objDA.TransactionBegin();
                        int idx = 0;

                        while (objSR.Peek() >= 0)
                        {
                            strLineData = objSR.ReadLine();
                            arrImportData = strLineData.Split(arrDelim);

                            // skip first line
                            if (idx == 0)
                            {
                                strLineData = objSR.ReadLine();
                                arrImportData = strLineData.Split(arrDelim);
                                idx += 1;
                            }

                            // the ID is +1 because ID = 1 is the admin, which never changes
                            m_objDA.UpdateTimesData(Int32.Parse(arrImportData[0])+1, Int32.Parse(arrImportData[1]), Int32.Parse(arrImportData[2]), Int32.Parse(arrImportData[3])+4, Int32.Parse(arrImportData[4])+8, Int32.Parse(arrImportData[5])+12);

                        }
                        m_objDA.TransactionCommit();
                        //save file

                        //display successful msg
                        this.lblUserMessage.Text = "Times Updated";

                    }

                    //end try
                    catch (Exception ex)
                    {
                        m_objDA.TransactionRollback();
                        string strError = string.Format("Exception occurred in MainControl.asavebpalgotimes_Click ... {0}", ex.Message);
                        this.m_errorList.Add(strError);
                        Utilities.CriticalErrorNotification(base.AnyErrorsNoHTML(this.m_errorList), base.m_bSendErrorEmails);

                    }
                    finally
                    {
                    }
                }
            }

        }


        //protected void buttonFacultyUpdate_Click(object sender, EventArgs e)
        //{   
        //    //Perform validations
        //    Page.Validate();
        //    if (!Page.IsValid)
        //    {
        //        this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
        //        return;
        //    }

        //    m_objDA.DownloadFaculty();
        //    int number = m_objDA.GetNumFaculty();
        //    string line;
        //    line = "School Name,Name,Email,Title,Will Attend?\r\n";
        //    string path = ConfigurationManager.AppSettings["MemberProfiles"].ToString() + "\\FacultyInfo.csv"; 
        //    System.IO.File.WriteAllText(@path, line);

        //    for (int i = 0; i < number; i++)
        //    {
        //        DataRow row = m_objDA.UserDataDT.Rows[i];
        //        line = Convert.ToString(row["SchoolName"]) + "," + Convert.ToString(row["Name"]) + "," + Convert.ToString(row["Email"]) + "," + Convert.ToString(row["Title"]) + "," + Convert.ToString(row["WillAttend"]);
        //        using (System.IO.StreamWriter file =
        //        new System.IO.StreamWriter(@path, true))
        //        {
        //            file.WriteLine(line);
        //        }
        //    }
        //    this.lblUserMessage.Text = "Faculty CSV File Saved";


        //}

            //protected void buttonStudentUpdate_Click(object sender, EventArgs e)
            //{
            //    //Perform validations
            //    Page.Validate();
            //    if (!Page.IsValid)
            //    {
            //        this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
            //        return;
            //    }

            //    m_objDA.DownloadMember();
            //    int number = m_objDA.GetNumStudents();
            //    string line;
            //    string path = ConfigurationManager.AppSettings["MemberProfiles"].ToString() + "\\StudentInfo.csv"; 
            //    line = "School Name,Name,Email,T-Shirt Size,Education,Area of Study,Expected Graduation, Investment Banking, Trading, Equities, Derivatives, Risk Management, Portfolio Management, Consulting, Fixed Income, Paragraph\r\n";
            //    System.IO.File.WriteAllText(@path, line);

            //    for (int i = 0; i < number; i++)
            //    {
            //        DataRow row = m_objDA.UserDataDT.Rows[i];
            //        line = Convert.ToString(row["SchoolName"]) + "," + Convert.ToString(row["FullName"]) + "," + Convert.ToString(row["Email"]) + "," + Convert.ToString(row["ShirtSize"]) + "," + Convert.ToString(row["Education"]) + "," + Convert.ToString(row["AreaofStudy"]) + "," + Convert.ToString(row["ExpectedGraduation"]) + "," + Convert.ToString(row["InvestmentBanking"]) + "," + Convert.ToString(row["Trading"]) + "," + Convert.ToString(row["Equities"]) + "," + Convert.ToString(row["Derivatives"]) + "," + Convert.ToString(row["RiskManagement"]) + "," + Convert.ToString(row["PortfolioManagement"]) + "," + Convert.ToString(row["Consulting"]) + "," + Convert.ToString(row["FixedIncome"]) + "," + Convert.ToString(row["Paragraph"]);
            //        using (System.IO.StreamWriter file =
            //        new System.IO.StreamWriter(@path, true))
            //        {
            //            file.WriteLine(line);
            //        }
            //    }
            //    this.lblUserMessage.Text = "Student Profile CSV File Saved";


            //}


        protected string BPRoleName(int role)
        {
            switch (role)
            {
                case 1:
                    return "Producer";
                case 2:
                    return "Refiner";
                case 3:
                    return "Trader";
                default:
                    return "";
            }
        }

        protected string QuantRoleName(int role)
        {
            switch (role)
            {
                case 1:
                    return "Trader";
                case 2:
                    return "Analyst";
                default:
                    return "";
            }
        }

        protected void buttonStudentSchedule_Click(object sender, EventArgs e)
        {
            //Perform validations
            Page.Validate();
            if (!Page.IsValid)
            {
                this.m_errorList.Add(BaseBusiness.ERR_MSG_VALIDATION_FAILED);
                return;
            }

            m_objDA.DownloadSchedule();
            int number = m_objDA.SchedNumStudents();
            string line;
            line = "Institution Name,Student Name,BPRole,QuantHeat1Role,BP1,BP2,Case2H1,Case2H2,Case3H1,Case3H2,Case4H1,Case4H2\r\n";
            string path = Server.MapPath("~/CompetitionSchedule.csv");
            System.IO.File.WriteAllText(@path, line);

            for (int i = 0; i < number; i++)
            {
                DataRow row = m_objDA.UserDataDT.Rows[i];
                int bpheat = Convert.ToInt32(row["BPHeat"]);
                int bprolenum = Convert.ToInt32(row["BP"]);
                int creditnum = Convert.ToInt32(row["Credit"]);
                int quantnum = Convert.ToInt32(row["Quant"]);
                int capnum = Convert.ToInt32(row["Cap"]);
                int stnum = Convert.ToInt32(row["ST"]);
                int Algo1 = Convert.ToInt32(row["Algo1"]);
                int Algo2 = Convert.ToInt32(row["Algo2"]);
                int Algo3 = Convert.ToInt32(row["Algo3"]);
                int Algo4 = Convert.ToInt32(row["Algo4"]);
                int a1 = Convert.ToInt32(row["a1"]);
                int a2 = Convert.ToInt32(row["a2"]);
                int a3 = Convert.ToInt32(row["a3"]);
                int a4 = Convert.ToInt32(row["a4"]);
                string BP1 = "";
                string BP2 = "";
                string CR1 = "";
                string CR2 = "";
                string CAPIQ1 = "";
                string CAPIQ2 = "";
                string ST1 = "";
                string ST2 = "";

                
                    if (bpheat == 1)
                    {
                        if (bprolenum != 0)
                        {
                            BP1 = "9:00 BP 1";
                        }
                        if (Algo1 == 1)
                        {
                            BP2 = "Algo " + Scheduling.AlgoTime(a1);
                        }
                    }

                    else // bpheat == 2
                    {
                        if (bprolenum != 0)
                        {
                            BP2 = "10:30 BP 2";
                        }
                        if (Algo1 == 1)
                        {
                            BP1 = "Algo " + Scheduling.AlgoTime(a1);
                        }
                    }

                    if (creditnum == 1)
                    {
                        CR1 = "9:00 " + Scheduling.d2am + " 1";
                        if (Algo3 == 1)
                        {
                            CR2 = "Algo " + Scheduling.AlgoTime(a3);
                        }
                    }

                    else if (creditnum == 2)
                    {
                        CR2 = "10:30 " + Scheduling.d2am + " 2";
                        if (Algo3 == 1)
                        {
                            CR1 = "Algo " + Scheduling.AlgoTime(a3);
                        }
                    }

                    else // not participating in CR
                    {
                        if (Algo3 == 1)
                        {
                            if (Scheduling.Algo3Conflict(a3) == 1) // algo during CR 1
                            {
                                CR1 = "Algo " + Scheduling.AlgoTime(a3);
                            }

                            else // algo during CR 2
                            {
                                CR2 = "Algo " + Scheduling.AlgoTime(a3);
                            }
                        }
                    }

                    if (capnum == 1)
                    {
                        CAPIQ1 = "1:00 "+ Scheduling.d1pm + " 1";
                        if (Algo2 == 1)
                        {
                            CAPIQ2 = "Algo " + Scheduling.AlgoTime(a2);
                        }
                    }

                    else if (capnum == 2)
                    {
                        CAPIQ2 = "2:30 " + Scheduling.d1pm + " 2";
                        if (Algo2 == 1)
                        {
                            CAPIQ1 = "Algo " + Scheduling.AlgoTime(a2);
                        }
                    }

                    else // not participating in CAP
                    {
                        if (Algo2 == 1)
                        {
                            if (Scheduling.Algo2Conflict(a2) == 1) // algo during Cap 1
                            {
                                CAPIQ1 = "Algo " + Scheduling.AlgoTime(a2);
                            }

                            else // algo during CAP 2
                            {
                                CAPIQ2 = "Algo " + Scheduling.AlgoTime(a2);
                            }
                        }
                    }

                    if (stnum == 1)
                    {
                        ST1 = "1:00 " + Scheduling.d2pm + " 1";
                        if (Algo4 == 1)
                        {
                            ST2 = "Algo " + Scheduling.AlgoTime(a4);
                        }
                    }

                    else if (stnum == 2)
                    {
                        ST2 = "2:30 " + Scheduling.d2pm + " 2";
                        if (Algo4 == 1)
                        {
                            ST1 = "Algo " + Scheduling.AlgoTime(a4);
                        }
                    }

                    else // not participating in ST
                    {
                        if (Algo4 == 1)
                        {
                            if (Scheduling.Algo4Conflict(a4) == 1) // algo during ST 1
                            {
                                ST1 = "Algo " + Scheduling.AlgoTime(a4);
                            }

                            else // algo duringST 2
                            {
                                ST2 = "Algo " + Scheduling.AlgoTime(a4);
                            }
                        }
                    }

                    






                line = Convert.ToString(row["SchoolName"]) + "," + Convert.ToString(row["FullName"]) + "," + BPRoleName(bprolenum) + "," + QuantRoleName(quantnum) + "," + BP1 + "," + BP2 + "," + CAPIQ1 + "," + CAPIQ2 + "," + CR1 + "," + CR2 + "," + ST1 + "," + ST2;
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@path, true))
                {
                    file.WriteLine(line);
                }
            }
            this.lblUserMessage.Text = "Student Schedule CSV File Saved";


        }



        #endregion
    }   //public partial class MainControl
}
