using System;
using System.Data;
using System.Configuration;
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
    public class Enumerations
    {

        public enum SortDirection
        {
            NotDefined = -1,
            Ascending = 1,
            Descending = 2,
        };

        public enum OutcryTypes
        {
            Undefined = 0,
            OpenOutcry = 1,
            QuantOutcry = 2
        };

        public enum CaseID
        {
            c1 = 1,
            c2 = 2,
            c3 = 3,
            c4 = 4,
            c5 = 5,
            c6 = 6,
            c7 = 7,
            c8 = 8,
            c9 = 9,
            c10 = 10,
            c11 = 11, //algo
        };

        public enum PortalSystemTypes
        {
            Undefined = 0,
            sandt = 1,  //for weight
            commod = 2,  //for weight
            qo = 3,  //for weight
            oo = 4,  //for weight
            g1 = 5,
            g2 = 6,
            g3 = 7,
            g4 = 8,
            g5 = 9,
            g6 = 10,
            g7 = 11,
            g8 = 12,
            g9 = 13,
            g10 = 14,
            g11 = 15,
            g12 = 16,
            g13 = 17,
            g14 = 18,
            g15 = 19,
            g16 = 20,
            g17 = 21,
            g18 = 22,
            sandtview = 23,
            commodview = 24,
            qoview = 25,
            ooview = 26,
            finalview = 27,
            qo1final = 28,
            qo2final = 29,
            oo1final = 30,
            oo2final = 31,
            crview = 32,
            cr = 33,  //for weight
            algo = 34,  //for weight
            algoView = 35,
            ThomsonQED = 36,  //for weight
            ThomsonQEDView = 37,
        };

        public enum UserTypes
        {
            NotDefined = -1,
            Admin = 1,
            Ticket = 2,
            Student = 3,
        };

        public enum PageOps
        {
            NotDefined = -1,
            Add = 1,
            Edit = 2,
            Delete = 3,
        }

        public enum GameIDs
        {
            NotDefined = -1,
            Commod1 = 1,
            Commod2 = 2,
            Commod3 = 3,
            Commod4 = 4,
            Commod5 = 5,
            Commod6 = 6,
            Commod7 = 7,
            Commod8 = 8,
            SandT1 = 9,
            SandT2 = 10,
            SandT3 = 11,
            SandT4 = 12,
            SandT5 = 13,
            SandT6 = 14,
            SandT7 = 15,
            SandT8 = 16,
            SandT9 = 17,
            SandT10 = 18,
            SandT11 = 19,
            SandT12 = 20,
            //if below enums change, change BasePage.ADJUST_OO_AND_QO_GAME_IDS
            OO1Final,   //21
            OO2Final,   //22
            QO1Final,   //23
            QO2Final,   //24
            CR1,        //25
            CR2,
            CR3,
            CR4,
            CR5,
            CR6,
            CR7,
            CR8,
            CR9,
            CR10,
            CR11,
            CR12,   //36
            TRQED1, //37
            TRQED2,
            TRQED3,
            TRQED4,
            TRQED5,
            TRQED6,
            TRQED7,
            TRQED8,
            TRQED9,
            TRQED10,
            TRQED11,
            TRQED12,
            Algo,
            NoMoreGames,    // = 50
        }

    }   //public class Enumerations
}
