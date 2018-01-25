<%@ Page Language="C#" MasterPageFile="~/RotmanITCPortal.Master" Title="Registration"
    AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="RotmanTrading.Portal.Admin.Registration" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPortalMaster">
    <h1 style="text-align:center">Team Profile Registration</h1>
    <center><asp:Label ID="School" Font-Size="Medium" runat="server"></asp:Label></center>
    <div style= "padding-left:100px; padding-right:100px;">
        <p style="text-align:justify "><font size="2"> All teams must be composed of 4-6 students currently enrolled in undergraduate or graduate classes at the academic institution that they are representing. Each team must have a faculty advisor, regardless of whether they attend or not. <br />RITC will compile a participant booklet, which will be available to all competitors, faculty and sponsors. Submitting a profile will help identify you amongst other competitors for networking opportunities with students and sponsors that have similar interest. Requests to abstain from submitting a profile will be handled on a case by case basis. If your school does not allow you to retain school e-mail addresses when you graduate, it is suggested you use a more accessible public address. </font></p>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
            <center><asp:Label ID="lblErrorMsg" CssClass="redtext" runat="server"></asp:Label>
    <asp:Label ID="lblUserMessage" CssClass="greentext" runat="server"></asp:Label></center>
        <h2 style="text-align:left">Faculty Advisor</h2>
        <p><b>Full Name: </b><asp:Textbox ID="FacultyName" Columns="60" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="FacultyEmail" Columns="60" runat="server"></asp:Textbox></p> 
        <p><b>Title: </b><asp:Textbox ID="FacultyTitle" Columns="66" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <b>Advisor will be attending the competition </b> &nbsp&nbsp <asp:CheckBox ID="Attend" runat="server" /></p>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left">Team Member #1</h2>
        <p><b>Full Name: </b><asp:Textbox ID="Member1Name" Columns="30" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="Member1Email" Columns="40" runat="server"></asp:Textbox></p> 
        <p><b>T-Shirt Size</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>S </b><asp:CheckBox ID="Member1S" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>M </b><asp:CheckBox ID="Member1M" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>L </b><asp:CheckBox ID="Member1L" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp XL </b><asp:CheckBox ID="Member1XL" runat="server" /></p>
        <p><b>MBA </b><asp:CheckBox ID="Member1MBA" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Graduate </b><asp:CheckBox ID="Member1Graduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Undergraduate </b><asp:CheckBox ID="Member1Undergraduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Area of Study: </b><asp:Textbox ID="Member1AOS" Columns="32" runat="server"></asp:Textbox><b>&nbsp&nbsp&nbsp&nbsp Expected Graduation: </b><asp:Textbox ID="Member1Year" Columns="10" runat="server"></asp:Textbox></p> 
        <p><b>Area(s) of Interest: </b>&nbsp&nbsp&nbsp&nbsp&nbsp <b>Investment Banking </b><asp:CheckBox ID="Member1IB" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Trading </b><asp:CheckBox ID="Member1Trading" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Equities </b><asp:CheckBox ID="Member1Equities" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Derivatives </b><asp:CheckBox ID="Member1Derivatives" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Risk Management </b><asp:CheckBox ID="Member1RM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Portfolio Management </b><asp:CheckBox ID="Member1PM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Consulting </b><asp:CheckBox ID="Member1Consulting" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Fixed Income </b><asp:CheckBox ID="Member1FI" runat="server" /></p>
        <p><b>Personal Paragraph </b></p>
        <asp:TextBox id="Member1Description" rows="6" columns="108" TextMode="multiline" runat="server" />
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left">Team Member #2</h2>
        <p><b>Full Name: </b><asp:Textbox ID="Member2Name" Columns="30" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="Member2Email" Columns="40" runat="server"></asp:Textbox></p> 
        <p><b>T-Shirt Size</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>S </b><asp:CheckBox ID="Member2S" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>M </b><asp:CheckBox ID="Member2M" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>L </b><asp:CheckBox ID="Member2L" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp XL </b><asp:CheckBox ID="Member2XL" runat="server" /></p>
        <p><b>MBA </b><asp:CheckBox ID="Member2MBA" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Graduate </b><asp:CheckBox ID="Member2Graduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Undergraduate </b><asp:CheckBox ID="Member2Undergraduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Area of Study: </b><asp:Textbox ID="Member2AOS" Columns="32" runat="server"></asp:Textbox><b>&nbsp&nbsp&nbsp&nbsp Expected Graduation: </b><asp:Textbox ID="Member2Year" Columns="10" runat="server"></asp:Textbox></p> 
        <p><b>Area(s) of Interest: </b>&nbsp&nbsp&nbsp&nbsp&nbsp <b>Investment Banking </b><asp:CheckBox ID="Member2IB" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Trading </b><asp:CheckBox ID="Member2Trading" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Equities </b><asp:CheckBox ID="Member2Equities" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Derivatives </b><asp:CheckBox ID="Member2Derivatives" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Risk Management </b><asp:CheckBox ID="Member2RM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Portfolio Management </b><asp:CheckBox ID="Member2PM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Consulting </b><asp:CheckBox ID="Member2Consulting" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Fixed Income </b><asp:CheckBox ID="Member2FI" runat="server" /></p>
        <p><b>Personal Paragraph </b></p>
        <asp:TextBox id="Member2Description" rows="6" columns="108" TextMode="multiline" runat="server" />
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left">Team Member #3</h2>
        <p><b>Full Name: </b><asp:Textbox ID="Member3Name" Columns="30" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="Member3Email" Columns="40" runat="server"></asp:Textbox></p> 
        <p><b>T-Shirt Size</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>S </b><asp:CheckBox ID="Member3S" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>M </b><asp:CheckBox ID="Member3M" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>L </b><asp:CheckBox ID="Member3L" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp XL </b><asp:CheckBox ID="Member3XL" runat="server" /></p>
        <p><b>MBA </b><asp:CheckBox ID="Member3MBA" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Graduate </b><asp:CheckBox ID="Member3Graduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Undergraduate </b><asp:CheckBox ID="Member3Undergraduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Area of Study: </b><asp:Textbox ID="Member3AOS" Columns="32" runat="server"></asp:Textbox><b>&nbsp&nbsp&nbsp&nbsp Expected Graduation: </b><asp:Textbox ID="Member3Year" Columns="10" runat="server"></asp:Textbox></p> 
        <p><b>Area(s) of Interest: </b>&nbsp&nbsp&nbsp&nbsp&nbsp <b>Investment Banking </b><asp:CheckBox ID="Member3IB" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Trading </b><asp:CheckBox ID="Member3Trading" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Equities </b><asp:CheckBox ID="Member3Equities" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Derivatives </b><asp:CheckBox ID="Member3Derivatives" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Risk Management </b><asp:CheckBox ID="Member3RM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Portfolio Management </b><asp:CheckBox ID="Member3PM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Consulting </b><asp:CheckBox ID="Member3Consulting" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Fixed Income </b><asp:CheckBox ID="Member3FI" runat="server" /></p>
        <p><b>Personal Paragraph </b></p>
        <asp:TextBox id="Member3Description" rows="6" columns="108" TextMode="multiline" runat="server" />
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left">Team Member #4</h2>
        <p><b>Full Name: </b><asp:Textbox ID="Member4Name" Columns="30" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="Member4Email" Columns="40" runat="server"></asp:Textbox></p> 
        <p><b>T-Shirt Size</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>S </b><asp:CheckBox ID="Member4S" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>M </b><asp:CheckBox ID="Member4M" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>L </b><asp:CheckBox ID="Member4L" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp XL </b><asp:CheckBox ID="Member4XL" runat="server" /></p>
        <p><b>MBA </b><asp:CheckBox ID="Member4MBA" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Graduate </b><asp:CheckBox ID="Member4Graduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Undergraduate </b><asp:CheckBox ID="Member4Undergraduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Area of Study: </b><asp:Textbox ID="Member4AOS" Columns="32" runat="server"></asp:Textbox><b>&nbsp&nbsp&nbsp&nbsp Expected Graduation: </b><asp:Textbox ID="Member4Year" Columns="10" runat="server"></asp:Textbox></p> 
        <p><b>Area(s) of Interest: </b>&nbsp&nbsp&nbsp&nbsp&nbsp <b>Investment Banking </b><asp:CheckBox ID="Member4IB" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Trading </b><asp:CheckBox ID="Member4Trading" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Equities </b><asp:CheckBox ID="Member4Equities" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Derivatives </b><asp:CheckBox ID="Member4Derivatives" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Risk Management </b><asp:CheckBox ID="Member4RM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Portfolio Management </b><asp:CheckBox ID="Member4PM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Consulting </b><asp:CheckBox ID="Member4Consulting" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Fixed Income </b><asp:CheckBox ID="Member4FI" runat="server" /></p>
        <p><b>Personal Paragraph </b></p>
        <asp:TextBox id="Member4Description" rows="6" columns="108" TextMode="multiline" runat="server" />
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left">Team Member #5</h2>
        <p><b>Full Name: </b><asp:Textbox ID="Member5Name" Columns="30" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="Member5Email" Columns="40" runat="server"></asp:Textbox></p> 
        <p><b>T-Shirt Size</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>S </b><asp:CheckBox ID="Member5S" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>M </b><asp:CheckBox ID="Member5M" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>L </b><asp:CheckBox ID="Member5L" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp XL </b><asp:CheckBox ID="Member5XL" runat="server" /></p>
        <p><b>MBA </b><asp:CheckBox ID="Member5MBA" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Graduate </b><asp:CheckBox ID="Member5Graduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Undergraduate </b><asp:CheckBox ID="Member5Undergraduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Area of Study: </b><asp:Textbox ID="Member5AOS" Columns="32" runat="server"></asp:Textbox><b>&nbsp&nbsp&nbsp&nbsp Expected Graduation: </b><asp:Textbox ID="Member5Year" Columns="10" runat="server"></asp:Textbox></p> 
        <p><b>Area(s) of Interest: </b>&nbsp&nbsp&nbsp&nbsp&nbsp <b>Investment Banking </b><asp:CheckBox ID="Member5IB" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Trading </b><asp:CheckBox ID="Member5Trading" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Equities </b><asp:CheckBox ID="Member5Equities" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Derivatives </b><asp:CheckBox ID="Member5Derivatives" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Risk Management </b><asp:CheckBox ID="Member5RM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Portfolio Management </b><asp:CheckBox ID="Member5PM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Consulting </b><asp:CheckBox ID="Member5Consulting" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Fixed Income </b><asp:CheckBox ID="Member5FI" runat="server" /></p>
        <p><b>Personal Paragraph </b></p>
        <asp:TextBox id="Member5Description" rows="6" columns="108" TextMode="multiline" runat="server" />
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
        <h2 style="text-align:left">Team Member #6</h2>
        <p><b>Full Name: </b><asp:Textbox ID="Member6Name" Columns="30" runat="server"></asp:Textbox> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>E-mail: </b><asp:Textbox ID="Member6Email" Columns="40" runat="server"></asp:Textbox></p> 
        <p><b>T-Shirt Size</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>S </b><asp:CheckBox ID="Member6S" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>M </b><asp:CheckBox ID="Member6M" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>L </b><asp:CheckBox ID="Member6L" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp XL </b><asp:CheckBox ID="Member6XL" runat="server" /></p>
        <p><b>MBA </b><asp:CheckBox ID="Member6MBA" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Graduate </b><asp:CheckBox ID="Member6Graduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Undergraduate </b><asp:CheckBox ID="Member6Undergraduate" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Area of Study: </b><asp:Textbox ID="Member6AOS" Columns="32" runat="server"></asp:Textbox><b>&nbsp&nbsp&nbsp&nbsp Expected Graduation: </b><asp:Textbox ID="Member6Year" Columns="10" runat="server"></asp:Textbox></p> 
        <p><b>Area(s) of Interest: </b>&nbsp&nbsp&nbsp&nbsp&nbsp <b>Investment Banking </b><asp:CheckBox ID="Member6IB" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Trading </b><asp:CheckBox ID="Member6Trading" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Equities </b><asp:CheckBox ID="Member6Equities" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Derivatives </b><asp:CheckBox ID="Member6Derivatives" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Risk Management </b><asp:CheckBox ID="Member6RM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Portfolio Management </b><asp:CheckBox ID="Member6PM" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<b>Consulting </b><asp:CheckBox ID="Member6Consulting" runat="server" /><b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Fixed Income </b><asp:CheckBox ID="Member6FI" runat="server" /></p>
        <p><b>Personal Paragraph </b></p>
        <asp:TextBox id="Member6Description" rows="6" columns="108" TextMode="multiline" runat="server" />
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
    <asp:Button ID="buttonSaveValues" runat="server" Text="Save" OnClick="buttonSaveValues_Click" />
    </div>
</asp:Content>