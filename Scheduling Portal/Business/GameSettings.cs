using System;
//NOTE: THIS FILE SHOULD BE RENDERED OBSOLETE WITH THE USE OF THE NEW TABLE FORMAT FOR CASE DETAILS ...
namespace RotmanTrading.Business
{
    public class GameSettings
    {
        public enum GameImplementations
        {
            NotDefined = -1,
            Regular = 1,
            August082012 = 2,
            February2013 = 3,
            February2014 = 4
        };

        //Notes:
        //Below are set in Scores.aspx.cs

        //For GameSettings.GameImplementation == GameSettings.GameImplementations.August082012:
        //BP Comm Details is now called Case1Details
        //CreditRisk Details is now called Case2Details
        //ThomsonReutersQED Details is now called Case3Details

        //For GameSettings.GameImplementation == GameSettings.GameImplementations.February2013:
        //BPCommodities & ThompsonReuters are being swapped
        //Case1Details = ThompsonReuters (3/6)
        //Case2Details = Options (5/10)
        //Case3Details = BPCommodities (4/8)

        static GameSettings()
        {
            GameImplementation = GameImplementations.February2014;

            if (GameImplementation == GameImplementations.Regular)
            {
                NumberOfHeats = 2;
                TRQEDNumberOfSubHeats = 6;
                BPCommodityNumberOfSubHeats = 3;
                CreditRiskNumberOfSubHeats = 6;
                SandTNumberOfSubHeats = 6;
                OutcryNumberOfSubHeats = 2;
                GameNameCreditRisk = "Options Trading";
                GameNameThomsonReuters = "Thomson Reuters QED";
                GameNameSandT = "Sales & Trading";
                GameNameBPCommodities = "BP Commodities";
                GameNameAlgo = "CIBC Algo";
                GameNameQuantOutcry = "Quant Outcry";
                GameNameOpenOutcry = "Open Outcry";
                IncludeAlgo = true;
            }
            else if (GameImplementation == GameImplementations.August082012)
            {
                NumberOfHeats = 1;
                TRQEDNumberOfSubHeats = 5;  //Bloomberg M&A
                BPCommodityNumberOfSubHeats = 5;    //Enel Energy
                CreditRiskNumberOfSubHeats = 10;    //CC&G Options
                SandTNumberOfSubHeats = 10;
                OutcryNumberOfSubHeats = 2;
                GameNameCreditRisk = "CC&G Options";
                GameNameThomsonReuters = "Bloomberg M&A";
                GameNameSandT = "Sales & Trading";
                GameNameBPCommodities = "Enel Energy";
                GameNameAlgo = "CIBC Algo";
                GameNameQuantOutcry = "Quant Outcry";
                GameNameOpenOutcry = "Open Outcry";
                IncludeAlgo = false;
            }
            else if (GameImplementation == GameImplementations.February2013)
            {
                //Swapping BPCommodity & Thomson Reuters 
                NumberOfHeats = 2;
                NumberOfHeatsThomsonReuters = 1;
                TRQEDNumberOfSubHeats = 8;
                BPCommodityNumberOfSubHeats = 3;
                CreditRiskNumberOfSubHeats = 5;
                SandTNumberOfSubHeats = 5;
                OutcryNumberOfSubHeats = 2;

                GameNameCreditRisk = "Options";
                GameNameThomsonReuters = "BP Commodities";
                GameNameSandT = "Sales & Trader";
                GameNameBPCommodities = "Thomson Reuters M&A Case";
                GameNameAlgo = "CIBC Algo";
                GameNameQuantOutcry = "Quant Outcry";
                GameNameOpenOutcry = "Open Outcry";
                IncludeAlgo = true;
            }
            else if (GameImplementation == GameImplementations.February2014)
            {
                NumberOfHeats = 2;
                NumberOfHeatsThomsonReuters = 2;    //was 1
                NumberOfHeatsBPCommodity = 1;
                CreditRiskNumberOfSubHeats = 5; //no change
                TRQEDNumberOfSubHeats = 5;  //was 8
                SandTNumberOfSubHeats = 5;  //no change
                BPCommodityNumberOfSubHeats = 8;    //was 3
                OutcryNumberOfSubHeats = 2; //no change

                GameNameCreditRisk = "Options";                     //Case2Details.aspx //changes made
                GameNameThomsonReuters = "CIBC Yield Curve Case";   //Case3Details.aspx //changes made
                GameNameSandT = "Sales & Trader";                                       //changes made
                GameNameBPCommodities = "BP Commodities";           //Case1Details.aspx //changes made
                GameNameQuantOutcry = "Quant Outcry";   //
                GameNameOpenOutcry = "Open Outcry";
                GameNameAlgo = "Algo";  //
                IncludeAlgo = true;
            }
        }

        public static GameImplementations GameImplementation { get; set; }

        public static int NumberOfHeats { get; set; }
        public static int NumberOfHeatsThomsonReuters { get; set; }
        public static int TRQEDNumberOfSubHeats { get; set; }
        public static int BPCommodityNumberOfSubHeats { get; set; }
        public static int CreditRiskNumberOfSubHeats { get; set; }
        public static int SandTNumberOfSubHeats { get; set; }
        public static int OutcryNumberOfSubHeats { get; set; }
        public static int NumberOfHeatsBPCommodity { get; set; }

        public static string GameNameCreditRisk { get; set; }
        public static string GameNameThomsonReuters { get; set; }
        public static string GameNameSandT { get; set; }
        public static string GameNameBPCommodities { get; set; }
        public static string GameNameAlgo { get; set; }
        public static string GameNameQuantOutcry { get; set; }
        public static string GameNameOpenOutcry { get; set; }

        public static bool IncludeAlgo { get; set; }
    }
}