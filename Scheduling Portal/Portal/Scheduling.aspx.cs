using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

    public partial class Scheduling: BasePage
    {
        // change names of cases below

        public string Day1MorningCase = "BP Commodities";
         public string Day1AfternoonCase = "Flow Traders ETF";
         public string Day2MorningCase = "Bridgewater Fixed Income";
         public string Day2AfternoonCase = "MATLAB Volatility Trading";

        public static string d1am = "BP";
        public static string d1pm = "FT";
        public static string d2am = "FI";
        public static string d2pm = "ML";

        #region Declarations
        private ArrayList m_errorList = new ArrayList();
        MSSqlDataAccess m_objDA = new MSSqlDataAccess();
        private int idx = 0; // 1 represents first student in the table, etc
        private int[] algo234 = new int[3];
        #endregion

        #region Constructors
        public Scheduling()
            : base(false)
        {
        }
        #endregion

        static public string AlgoTime(int Algonum)
        {
            switch (Algonum)
            {
                case 1:
                    return "9:00-9:30AM";
                case 2:
                    return "9:30-10:00AM";
                case 3:
                    return "10:30-11:00AM";
                case 4:
                    return "11:00-11:30AM";
                case 5:
                    return "1:00-1:30PM";
                case 6:
                    return "1:30-2:00PM";
                case 7:
                    return "2:30-3:00PM";
                case 8:
                    return "3:00-3:30PM";
                    //day two starts here
                case 9:
                    return "9:00-9:30AM";
                case 10:
                    return "9:30-10:00AM";
                case 11:
                    return "10:30-11:00AM";
                case 12:
                    return "11:00-11:30AM";
                case 13:
                    return "1:00-1:30PM"; 
                case 14:
                    return "1:30-2:00PM";
                case 15:
                    return "2:30-3:00PM";
                case 16:
                    return "3:00-3:30PM";
                default:
                    return "";

                        
            }
        }

        static public int Algo2Conflict(int algosubheat) // returns 1 if cannot compete in subheat1 of concurrent case, returns 2 if cannot compete in subheat2
        {
            switch (algosubheat)
            {
                case 5:
                    return 1;
                case 6:
                    return 1;
                case 7:
                    return 2;
                case 8:
                    return 2;
                default:
                    return 1;
            }
        }

        static public int Algo3Conflict(int algosubheat) // returns 1 if cannot compete in subheat1, returns 2 if cannot compete in subheat2
        {
            switch (algosubheat)
            {
                case 9:
                    return 1;
                case 10:
                    return 1;
                case 11:
                    return 2;
                case 12:
                    return 2;
                default:
                    return 1;
            }
        }

        static public int Algo4Conflict(int algosubheat) // returns 1 if cannot compete in subheat1, returns 2 if cannot compete in subheat2
        {
            switch (algosubheat)
            {
                case 13:
                    return 1;
                case 14:
                    return 1;
                case 15:
                    return 2;
                case 16:
                    return 2;
                default:
                    return 1;
            }
        }

        

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            this.lblErrorMsg.Text = string.Empty;
            this.lblUserMessage.Text = string.Empty;


            string connectionString = ConfigurationManager.AppSettings["ConnStringRotman"].ToString();
            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandText = "SELECT * FROM MemberRoles INNER JOIN Times ON Times.UserID = " + SessionManager.UserID.ToString() + " WHERE MemberRoles.UserID = " + SessionManager.UserID.ToString();
            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader();

            myReader.Read();
            Label AlgoTime1 = new Label();
            AlgoTime1.Text = AlgoTime((int)myReader["a1"]);
            Algolabel1.Controls.Add(AlgoTime1);

            Label AlgoTime2=  new Label();
            AlgoTime2.Text = AlgoTime((int)myReader["a2"]);
            Algolabel2.Controls.Add(AlgoTime2);
            algo234[0] = (int)myReader["a2"];

            Label AlgoTime3 = new Label();
            AlgoTime3.Text = AlgoTime((int)myReader["a3"]);
            Algolabel3.Controls.Add(AlgoTime3);
            algo234[1] = (int)myReader["a3"];

            Label AlgoTime4 = new Label();
            AlgoTime4.Text = AlgoTime((int)myReader["a4"]);
            Algolabel4.Controls.Add(AlgoTime4);
            algo234[2] = (int)myReader["a4"];
            
            Label bptime = new Label();
            if ((int)myReader["BPHeat"] == 1)
            {
                bptime.Text = "9:00-10:30AM";
            }
            else
            {
                bptime.Text = "10:30AM-12:00PM";
            }

            BPHeat.Controls.Add(bptime);



            do
            {

                idx++;

                // FRIDAY Morning
                PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));

                PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
                Label studentname1 = new Label();
                studentname1.Text = (string) myReader["FullName"];
                studentname1.ID = string.Format("Student{0}", idx);
                PlaceHolder1.Controls.Add(studentname1);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkprod = new CheckBox();
                checkprod.Checked = (1.CompareTo(myReader["BP"]) == 0);
                checkprod.ID = string.Format("BPprod{0}", idx);
                PlaceHolder1.Controls.Add(checkprod);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkref = new CheckBox();
                checkref.Checked = (2.CompareTo(myReader["BP"]) == 0);
                checkref.ID = string.Format("BPref{0}", idx);
                PlaceHolder1.Controls.Add(checkref);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checktrader = new CheckBox();
                checktrader.Checked = (3.CompareTo(myReader["BP"]) == 0);
                checktrader.ID = string.Format("BPtrader{0}", idx);
                PlaceHolder1.Controls.Add(checktrader);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkalgo1 = new CheckBox();
                checkalgo1.Checked = (1.CompareTo(myReader["Algo1"]) == 0);
                checkalgo1.ID = string.Format("Algo1Student{0}", idx);
                PlaceHolder1.Controls.Add(checkalgo1);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));



                //FRIDAY Afternoon
                PlaceHolder2.Controls.Add(new LiteralControl("<tr>"));

                PlaceHolder2.Controls.Add(new LiteralControl("<td>"));
                Label studentname2 = new Label();
                studentname2.Text = (string)myReader["FullName"];
                PlaceHolder2.Controls.Add(studentname2);
                PlaceHolder2.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checksp1 = new CheckBox();
                checksp1.Checked = (1.CompareTo(myReader["Cap"]) == 0);
                checksp1.ID = string.Format("Cap1Student{0}", idx);
                PlaceHolder2.Controls.Add(checksp1);
                PlaceHolder2.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checksp2 = new CheckBox();
                checksp2.Checked = (2.CompareTo(myReader["Cap"]) == 0);
                checksp2.ID = string.Format("Cap2Student{0}", idx);
                PlaceHolder2.Controls.Add(checksp2);
                PlaceHolder2.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkalgo2 = new CheckBox();
                checkalgo2.Checked = (1.CompareTo(myReader["Algo2"]) == 0);
                checkalgo2.ID = string.Format("Algo2Student{0}", idx);
                PlaceHolder2.Controls.Add(checkalgo2);
                PlaceHolder2.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkoutcrytrader = new CheckBox();
                checkoutcrytrader.Checked = (1.CompareTo(myReader["Quant"]) == 0);
                checkoutcrytrader.ID = string.Format("Outcrytrader{0}", idx);
                PlaceHolder2.Controls.Add(checkoutcrytrader);
                PlaceHolder2.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkoutcryanalyst = new CheckBox();
                checkoutcryanalyst.Checked = (2.CompareTo(myReader["Quant"]) == 0);
                checkoutcryanalyst.ID = string.Format("Outcryanalyst{0}", idx);
                PlaceHolder2.Controls.Add(checkoutcryanalyst);
                PlaceHolder2.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder2.Controls.Add(new LiteralControl("</tr>"));

                // Saturday MORNING

                PlaceHolder3.Controls.Add(new LiteralControl("<tr>"));

                PlaceHolder3.Controls.Add(new LiteralControl("<td>"));
                Label studentname3 = new Label();
                studentname3.Text = (string)myReader["FullName"];
                PlaceHolder3.Controls.Add(studentname3);
                PlaceHolder3.Controls.Add(new LiteralControl("</td>"));


                PlaceHolder3.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkcredit1 = new CheckBox();
                checkcredit1.Checked = (1.CompareTo(myReader["Credit"]) == 0);
                checkcredit1.ID = string.Format("Credit1Student{0}", idx);
                PlaceHolder3.Controls.Add(checkcredit1);
                PlaceHolder3.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder3.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkcredit2 = new CheckBox();
                checkcredit2.Checked = (2.CompareTo(myReader["Credit"]) == 0);
                checkcredit2.ID = string.Format("Credit2Student{0}", idx);
                PlaceHolder3.Controls.Add(checkcredit2);
                PlaceHolder3.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder3.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkalgo3 = new CheckBox();
                checkalgo3.Checked = (1.CompareTo(myReader["Algo3"]) == 0);
                checkalgo3.ID = string.Format("Algo3Student{0}", idx);
                PlaceHolder3.Controls.Add(checkalgo3);
                PlaceHolder3.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder3.Controls.Add(new LiteralControl("</tr>"));

                // Saturday AFTERNOON


                PlaceHolder4.Controls.Add(new LiteralControl("<tr>"));

                PlaceHolder4.Controls.Add(new LiteralControl("<td>"));
                Label studentname4 = new Label();
                studentname4.Text = (string)myReader["FullName"];
                PlaceHolder4.Controls.Add(studentname4);
                PlaceHolder4.Controls.Add(new LiteralControl("</td>"));


                PlaceHolder4.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkst1 = new CheckBox();
                checkst1.Checked = (1.CompareTo(myReader["ST"]) == 0);
                checkst1.ID = string.Format("ST1Student{0}", idx);
                PlaceHolder4.Controls.Add(checkst1);
                PlaceHolder4.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder4.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkst2 = new CheckBox();
                checkst2.Checked = (2.CompareTo(myReader["ST"]) == 0);
                checkst2.ID = string.Format("ST2Student{0}", idx);
                PlaceHolder4.Controls.Add(checkst2);
                PlaceHolder4.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder4.Controls.Add(new LiteralControl("<td style='text-align: center;'>"));
                CheckBox checkalgo4 = new CheckBox();
                checkalgo4.Checked = (1.CompareTo(myReader["Algo4"]) == 0);
                checkalgo4.ID = string.Format("Algo4Student{0}", idx);
                PlaceHolder4.Controls.Add(checkalgo4);
                PlaceHolder4.Controls.Add(new LiteralControl("</td>"));

                PlaceHolder4.Controls.Add(new LiteralControl("</tr>"));




            } while (myReader.Read());


            myReader.Close();
            myReader.Dispose();

            myConnection.Close();
        }

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


            this.lblUserMessage.Visible = false;


            int BP = 0;
            int Credit = 0;
            int Quant = 0;
            int Cap = 0;
            int ST = 0;
            int Algo1 = 0;
            int Algo2 = 0;
            int Algo3 = 0;
            int Algo4 = 0;
            string[] names = new string[6];
            List<int[]> arrList = new List<int[]>();

            int numproducer = 0;
            int numrefiner = 0;
            int numbptrader = 0;
            int numalgo1trader = 0;

            int numcredit1 = 0;
            int numcredit2 = 0;
            int numalgo2trader = 0;
            int numoutcrytrader = 0;
            int numoutcryanalyst = 0;

            int numcap1 = 0;
            int numcap2 = 0;
            int numalgo3trader = 0;

            int numst1 = 0;
            int numst2 = 0;
            int numalgo4trader = 0;


            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("cphPortalMaster");
            PlaceHolder PlaceHolder1 = (PlaceHolder)cph.FindControl("PlaceHolder1");
            PlaceHolder PlaceHolder2 = (PlaceHolder)cph.FindControl("PlaceHolder2");
            PlaceHolder PlaceHolder3 = (PlaceHolder)cph.FindControl("PlaceHolder3");
            PlaceHolder PlaceHolder4 = (PlaceHolder)cph.FindControl("PlaceHolder4");

            for (int i = 1; i <= idx; i++)
            {
                

                Label student = (Label) PlaceHolder1.FindControl(string.Format("Student{0}", i));
                names[i-1] = student.Text;
                int bpprod = ValueCheck((CheckBox)PlaceHolder1.FindControl(string.Format("BPprod{0}", i)));
                int bpref = ValueCheck((CheckBox)PlaceHolder1.FindControl(string.Format("BPref{0}", i)));
                int bptrader = ValueCheck((CheckBox)PlaceHolder1.FindControl(string.Format("BPtrader{0}", i)));

                int in_bp = 1;

                if (bpprod + bpref + bptrader == 0)
                {
                    in_bp = 0;
                }

                if (bpprod == 1 & bpref == 1)
                {
                    this.lblErrorMsg.Text = "Cannot assign one competitor as both producer and refiner for BP Commodities Case";
                    return;
                }

                if (bpprod == 1 & bptrader == 1)
                {
                    this.lblErrorMsg.Text = "Cannot assign one competitor as both producer and trader for BP Commodities Case";
                    return;
                }
                if (bpref == 1 & bptrader == 1)
                {
                    this.lblErrorMsg.Text = "Cannot assign one competitor as both refiner and trader for BP Commodities Case";
                    return;
                }

                if (bpprod == 1)
                {
                    BP = 1;
                    numproducer++;
                }
                else if (bpref == 1)
                {
                    BP = 2;
                    numrefiner++;
                }
                else if (bptrader == 1)
                {
                    BP = 3;
                    numbptrader++;
                }
                else
                {
                    BP = 0;
                }

                int credit1 = ValueCheck((CheckBox)PlaceHolder3.FindControl(string.Format("Credit1Student{0}", i)));
                int credit2 = ValueCheck((CheckBox)PlaceHolder3.FindControl(string.Format("Credit2Student{0}", i)));

                int in_credit = 1;

                if (credit1 + credit2 == 0)
                {
                    in_credit = 0;
                }


                if (credit1 == 1 & credit2 == 1)
                {
                    this.lblErrorMsg.Text = "Must assign two different team members for Heat #2 " + Day2MorningCase;
                    return;
                }

                if (credit1 == 1)
                {
                    Credit = 1;
                    numcredit1++;
                }
                else if (credit2 == 1)
                {
                    Credit = 2;
                    numcredit2++;
                }
                else
                {
                    Credit = 0;
                }

                int outcry1 = ValueCheck((CheckBox)PlaceHolder2.FindControl(string.Format("Outcrytrader{0}", i)));
                int outcry2 = ValueCheck((CheckBox)PlaceHolder2.FindControl(string.Format("Outcryanalyst{0}", i)));

                int in_outcry = 1;

                if (outcry1 + outcry2 == 0)
                {
                    in_outcry = 0;
                }


                if (outcry1 == 1 & outcry2 == 1)
                {
                    this.lblErrorMsg.Text = "Cannot assign one competitor as both trader and analyst for Quantitative Outcry Case";
                     return;
                }

                if (outcry1 == 1)
                {
                    Quant = 1;
                    numoutcrytrader++;
                }
                else if (outcry2 == 1)
                {
                    Quant = 2;
                    numoutcryanalyst++;
                }
                else
                {
                    Quant = 0;
                }

                int cap1 = ValueCheck((CheckBox)PlaceHolder2.FindControl(string.Format("Cap1Student{0}", i)));
                int cap2 = ValueCheck((CheckBox)PlaceHolder2.FindControl(string.Format("Cap2Student{0}", i)));

                int in_cap = 1;

                if (cap1 + cap2 == 0)
                {
                    in_cap = 0;
                }

                if (cap1 == 1 & cap2 == 1)
                {
                    this.lblErrorMsg.Text = "Must assign two different team members for Heat #2 " + Day1AfternoonCase;
                    return;
                }


                if (cap1 == 1)
                {
                    Cap = 1;
                    numcap1++;
                }
                else if (cap2 == 1)
                {
                    Cap = 2;
                    numcap2++;
                }
                else
                {
                    Cap = 0;
                }

                int st1 = ValueCheck((CheckBox)PlaceHolder4.FindControl(string.Format("ST1Student{0}", i)));
                int st2 = ValueCheck((CheckBox)PlaceHolder4.FindControl(string.Format("ST2Student{0}", i)));

                int in_st = 1;

                if (st1 + st2 == 0)
                {
                    in_st = 0;
                }


                if (st1 == 1 & st2 == 1)
                {
                    this.lblErrorMsg.Text = "Must assign two different team members for Heat #2 " + Day2AfternoonCase;
                    return;
                }


                if (st1 == 1)
                {
                    ST = 1;
                    numst1++;
                }
                else if (st2 == 1)
                {
                    ST = 2;
                    numst2++;
                }
                else
                {
                    ST = 0;
                }

                Algo1 = ValueCheck((CheckBox)PlaceHolder1.FindControl(string.Format("Algo1Student{0}", i)));
                if (Algo1 == 1)
                {
                    numalgo1trader++;
                }

                Algo2 = ValueCheck((CheckBox)PlaceHolder2.FindControl(string.Format("Algo2Student{0}", i)));
                if (Algo2 == 1)
                {
                    numalgo2trader++;
                }

                if (Algo2Conflict(algo234[0]) == 1)
                {
                    if (cap1 == 1 & Algo2 == 1)
                    {
                        this.lblErrorMsg.Text = "One member cannot be competing simultaneously in " + Day1AfternoonCase +  " Heat #1 and Algo Case Heat #2";
                        return;
                    }
                }
                else
                {
                    if (cap2 == 1 & Algo2 == 1)
                    {
                        this.lblErrorMsg.Text = "One member cannot be competing simultaneously in " + Day1AfternoonCase  + " Heat #2 and Algo Case Heat #2";
                        return;
                    }
                }

                Algo3 = ValueCheck((CheckBox)PlaceHolder3.FindControl(string.Format("Algo3Student{0}", i)));
                if (Algo3 == 1)
                {
                    numalgo3trader++;
                }
                if (Algo3Conflict(algo234[1]) == 1)
                {
                    if (credit1 == 1 & Algo3 == 1)
                    {
                        this.lblErrorMsg.Text = "One member cannot be competing simultaneously in " + Day2MorningCase + " Heat #1 and Algo Case Heat #3";
                        return;
                    }
                }
                else
                {
                    if (credit2 == 1 & Algo3 == 1)
                    {
                        this.lblErrorMsg.Text = "One member cannot be competing simultaneously in " + Day2MorningCase + " Heat #2 and Algo Case Heat #3";
                        return;
                    }
                }

                Algo4 = ValueCheck((CheckBox)PlaceHolder4.FindControl(string.Format("Algo4Student{0}", i)));
                if (Algo4 == 1)
                {
                    numalgo4trader++;
                }
                if (Algo4Conflict(algo234[2]) == 1)
                {
                    if (st1 == 1 & Algo4 == 1)
                    {
                        this.lblErrorMsg.Text = "One member cannot be competing simultaneously in " + Day2AfternoonCase + " Heat #1 and Algo Case Heat #4";
                        return;
                    }
                }
                else
                {
                    if (st2 == 1 & Algo4 == 1)
                    {
                        this.lblErrorMsg.Text = "One member cannot be competing simultaneously in " + Day2AfternoonCase + " Heat #2 and Algo Case Heat #4";
                        return;
                    }
                }

                int in_algo = 1;

                if (Algo1 + Algo2 + Algo3 + Algo4 == 0)
                {
                    in_algo = 0;
                }

                if (in_bp + in_credit + in_outcry + in_cap + in_st + in_algo < 2)
                {
                    this.lblErrorMsg.Text = "Each member must participate in at least two unique cases";
                    return;
                }



                arrList.Add(new int[] { BP, Credit, Quant, Cap, ST, Algo1, Algo2, Algo3, Algo4});




            }



            /*Checking Region*/

            //Friday Morning
            
            if (numbptrader != 2 | numproducer != 1 | numrefiner != 1)
            {
                this.lblErrorMsg.Text = "Must assign 1 producer, 1 refiner, and 2 traders for BP Commodities Case";
                return;
            }

            if (numalgo1trader != 1 )
            {
                this.lblErrorMsg.Text = "Must assign one trader for Algorithmic Case Heat #1";
                return;
            }

            //Saturday Morning
            if (numcredit1 != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors to " + Day2MorningCase + " Heat #1";
                return;
            }

            if (numcredit2 != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors to " + Day2MorningCase + " Heat #2";
                return;
            }

            if (numoutcrytrader != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors as traders for Quantitative Outcry Case";
                return;
            }

            if (numoutcryanalyst != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors as analysts for Quantitative Outcry Case";
                return;
            }

            if (numalgo2trader != 1)
            {
                this.lblErrorMsg.Text = "Must assign one trader for Algorithmic Case Heat #2";
                return;
            }


            //Friday Afternoon
            if (numcap1 != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors to " + Day1AfternoonCase + " Heat #1";
                return;
            }

            if (numcap2 != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors to " + Day1AfternoonCase + " Heat #2";
                return;
            }

            if (numalgo3trader != 1)
            {
                this.lblErrorMsg.Text = "Must assign one trader for Algorithmic Case Heat #3";
                return;
            }



            //Saturday Afternoon
            if (numst1 != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors to " + Day2AfternoonCase + " Heat #1";
                return;
            }

            if (numst2 != 2)
            {
                this.lblErrorMsg.Text = "Must assign two competitors to " + Day2AfternoonCase + " Heat #2";
                return;
            }

            if (numalgo4trader != 1)
            {
                this.lblErrorMsg.Text = "Must assign one trader for Algorithmic Case Heat #4";
                return;
            }


            for (int i = 1; i<=idx; i++)
            {
                
                try
                {
                //Enable transaction support
                m_objDA.TransactionEnable();
                m_objDA.TransactionBegin();

                
                    m_objDA.UpdateRoles(SessionManager.UserID, names[i-1], arrList[i-1][0], arrList[i-1][1], arrList[i-1][2], arrList[i-1][3], arrList[i-1][4], arrList[i-1][5], arrList[i-1][6], arrList[i-1][7], arrList[i-1][8] );


                //commit db changes
                m_objDA.TransactionCommit();
                //display successful msg
                this.lblUserMessage.Visible = true;
                this.lblUserMessage.Text = "Competition schedule preferences saved successfully";
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
            }





        }   //buttonSaveValues_Click


        #endregion

        #region Fill Screen
        private void FillScreen()
        {

 

        }





        #endregion

    }   //public partial class Scheduling
}