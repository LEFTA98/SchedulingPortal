<%@ Page Language="C#" MasterPageFile="~/RotmanITCPortal.Master" Title="Scheduling"
    AutoEventWireup="true" CodeBehind="Scheduling.aspx.cs" Inherits="RotmanTrading.Portal.Admin.Scheduling" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPortalMaster">
    <h3 style="text-align:center">Competition Scheduling</h3>
    <center><asp:Label ID="School" Font-Size="20px" runat="server"></asp:Label></center>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
            <center><asp:Label ID="lblErrorMsg" CssClass="redtext" runat="server"></asp:Label>
            <asp:Label ID="lblUserMessage" CssClass="greentext" runat="server" Visible ="false"></asp:Label></center>

        <h2 style="text-align:left"><u>Friday Morning:</u> <%= this.Day1MorningCase %>, Schonfeld Algo Heat #1</h2>
        <p style ="text-align:left"><%= this.Day1MorningCase %>: Designate one member as producer, one member as refiner, and two members as traders</p>
        <p style ="text-align:left">Algorithmic Trading Case: Designate one member to compete in Heat #1</p>
        <table style="border-color:Black;border-width:1px;"
                    cellpadding="2" cellspacing="0">
                    <tr>
                        <th></th>
                        <th colspan ="3" style = "background-color:forestgreen; color: white"><%= this.Day1MorningCase %> (<asp:PlaceHolder ID="BPHeat" runat="server"></asp:PlaceHolder>)</th>
                        <th colspan ="1" style = "background-color:darkblue; color: white">&nbsp&nbsp Algo (<asp:PlaceHolder ID="Algolabel1" runat="server"></asp:PlaceHolder>)&nbsp&nbsp</th>
                    </tr>
                    <tr>
                        <th></th>
	                    <th>&nbsp&nbsp&nbsp Producer (1)&nbsp&nbsp&nbsp</th>
                        <th>&nbsp&nbsp&nbsp Refiner (1)&nbsp&nbsp&nbsp&nbsp</th>
                        <th>&nbsp&nbsp&nbsp&nbsp Trader (2)&nbsp&nbsp&nbsp</th>
	                    <th>Trader (1)</th>
                    </tr>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </table>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left"><u>Friday Afternoon:</u> <%= this.Day1AfternoonCase %>, Schonfeld Algo Heat #2, S&P Global Quant Outcry</h2>
        <p style ="text-align:left"><%= this.Day1AfternoonCase %>: Designate two members for Heat #1 (1:00-2:30PM), and two different members for Heat #2 (2:30-4:00PM)</p>
        <p style ="text-align:left">Algorithmic Trading Case: Designate one member to compete in Heat #2</p>
        <p style ="text-align:left">Quantitative Outcry: Designate two members as traders, and two other members as analysts for the first heat (they will switch roles for the second heat)</p>
        <table style="border-color:Black;border-width:1px;"
                    cellpadding="2" cellspacing="0">
                    <tr>
                        <th></th>
                        <th colspan ="2" style = "background-color:orangered; color: white">&nbsp&nbsp <%= this.Day1AfternoonCase %> (1:00-2:30PM, 2:30-4:00PM)&nbsp&nbsp</th>
                        <th colspan ="1" style = "background-color:darkblue; color: white">&nbsp&nbsp Algo (<asp:PlaceHolder ID="Algolabel2" runat="server"></asp:PlaceHolder>)&nbsp&nbsp&nbsp</th>
                        <th colspan ="2" style = "background-color:mediumaquamarine; color: white">&nbsp&nbsp Quantitative Outcry (4:00-6:30PM)&nbsp&nbsp</th>
                    </tr>
                    <tr>
                        <th></th>
	                    <th>&nbsp&nbsp Heat #1 (2)&nbsp&nbsp</th>
                        <th>&nbsp&nbsp Heat #2 (2)&nbsp&nbsp</th>
                        <th>Trader (1)</th>
	                    <th>Trader (2)</th>
                        <th>Analyst (2)</th>
                    </tr>
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
        </table>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left"><u>Saturday Morning:</u> <%= this.Day2MorningCase %>, Schonfeld Algo Heat #3</h2>
        <p style ="text-align:left"><%= this.Day2MorningCase %> Case: Designate two members for Heat #1 (9:00-10:30AM), and two different members for Heat #2 (10:30AM-12:00PM)</p>
        <p style ="text-align:left">Algorithmic Trading Case: Designate one member to compete in Heat #3</p>
        <table style="border-color:Black;border-width:1px;"
                    cellpadding="2" cellspacing="0">
                    <tr>
                        <th></th>
                        <th colspan ="2" style = "background-color:darkred; color: white">&nbsp&nbsp <%= this.Day2MorningCase %> (9:00-10:30AM, 10:30AM-12:00PM)&nbsp&nbsp</th>
                        <th colspan ="1" style = "background-color:darkblue; color: white">&nbsp&nbsp Algo (<asp:PlaceHolder ID="Algolabel3" runat="server"></asp:PlaceHolder>)&nbsp&nbsp&nbsp</th>
                    </tr>
                    <tr>
                        <th></th>
	                    <th>&nbsp&nbsp Heat #1 (2)&nbsp&nbsp</th>
                        <th>&nbsp&nbsp Heat #2 (2)&nbsp&nbsp</th>
                        <th>Trader (1)</th>
                    </tr>
                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
        </table>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left"><u>Saturday Afternoon:</u> <%= this.Day2AfternoonCase %>, Schonfeld Algo Heat #4</h2>
        <p style ="text-align:left"><%= this.Day2AfternoonCase %>: Designate two members for Heat #1 (1:00-2:30PM), and two different members for Heat #2 (2:30-4:00PM)</p>
        <p style ="text-align:left">Algorithmic Trading Case: Designate one member to compete in Heat #4</p>
        <table style="border-color:Black;border-width:1px;"
                    cellpadding="2" cellspacing="0">
                    <tr>
                        <th></th>
                        <th colspan ="2" style = "background-color:mediumpurple; color: white">&nbsp&nbsp <%= this.Day2AfternoonCase %> (1:00-2:30PM, 2:30-4:00PM)&nbsp&nbsp</th>
                        <th colspan ="1" style = "background-color:darkblue; color: white">&nbsp&nbsp Algo (<asp:PlaceHolder ID="Algolabel4" runat="server"></asp:PlaceHolder>)&nbsp&nbsp&nbsp</th>

                    </tr>
                    <tr>
                        <th></th>
	                    <th>&nbsp&nbsp Heat #1 (2)&nbsp&nbsp</th>
                        <th>&nbsp&nbsp Heat #2 (2)&nbsp&nbsp</th>
                        <th>Trader (1)</th>
                    </tr>
                    <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
        </table>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
    <center><asp:Button ID="buttonSaveValues" runat="server" Text="Save Timeslot Preferences" style="font-weight: bold; font-size: 12px;" OnClick="buttonSaveValues_Click" /></center>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        </div>

</asp:Content>