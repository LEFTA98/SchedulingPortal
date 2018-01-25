<%@ Page Language="C#" MasterPageFile="~/RotmanITCPortal.Master" Title="Default Error"
    AutoEventWireup="true" CodeBehind="DefaultError.aspx.cs" Inherits="RotmanTrading.DefaultError" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPortalMaster">
    <table>
        <tr>
            <td align="center">
                <h3>Error Page</h3>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrorMsg" CssClass="redtext" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
				<br>
				An error has occurred.  
				<br>
				Please contact ... for more information.
				<br>
				<br>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
    </table>

</asp:Content>
