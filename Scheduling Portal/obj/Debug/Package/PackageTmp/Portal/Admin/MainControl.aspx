<%@ Page Language="C#" MasterPageFile="~/RotmanITCPortal.Master" Title="Admin Control"
    AutoEventWireup="true" CodeBehind="MainControl.aspx.cs" Inherits="RotmanTrading.Portal.Admin.MainControl" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPortalMaster">
    
    <div style="padding-left:100px; padding-right:100px; padding-top: 10px">
    <asp:Label ID="lblErrorMsg" CssClass="redtext" runat="server"></asp:Label>
    <asp:Label ID="lblUserMessage" CssClass="greentext" runat="server"></asp:Label>
    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 65px">
    <table border="1" style="border-color:Black;border-width:1px;" rules="all">
        <td>Upload User List Here</td>
        <td><asp:FileUpload ID="flucsv" runat="server" /></td>
        <td><asp:Button ID="buttonSaveValues" runat="server" Text="Save" OnClick="buttonSaveValues_Click" /></td>    
    </tr>

    <tr>
        <td>Upload Student List Here</td>
        <td><asp:FileUpload ID="filestudent" runat="server" /></td>
        <td><asp:Button ID="savestudent" runat="server" Text="Save" OnClick="savestudent_Click" /></td>

    </tr>

    <tr>

    <td>Upload BP Heats/Algo Times Here</td>
    <td><asp:FileUpload ID="bpalgotimesfile" runat="server" /></td>
    <td><asp:Button ID="bpalgotimes" runat="server" Text="Save" OnClick="savebpalgotimes_Click" /></td>
    </tr>

    </table>

    </div>

    <div style="padding-left:100px; padding-right:100px; padding-top: 10px">

    <table border="1" style="border-color:Black;border-width:1px;" rules="all">       
    
<%--    <tr>
        <td>Download Faculty Profiles</td>
        <td><asp:Button ID="buttonFacultyUpdate" runat="server" Text="Download" OnClick="buttonFacultyUpdate_Click" Enabled ="false"/></td>
    </tr>

    <tr>
        <td>Download Student Profiles</td>
        <td><asp:Button ID="buttonStudentUpdate" runat="server" Text="Download" OnClick="buttonStudentUpdate_Click" Enabled ="false"/></td>
   
    </tr>--%>

    <tr>
        <td>Download Student Timetables</td>
        <td><asp:Button ID="buttonStudentTimetable" runat="server" Text="Download" OnClick="buttonStudentSchedule_Click" /></td>
    </tr>

    </table>    
    
      
    </div>    
    
</asp:Content>
