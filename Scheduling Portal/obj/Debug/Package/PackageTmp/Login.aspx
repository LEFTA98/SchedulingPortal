<%@ Page Language="C#" MasterPageFile="~/RotmanITCPortal.Master" Title="Login" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" Inherits="RotmanTrading.Login" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPortalMaster">
    <table>
        <tr>
            <td align="center">
                <h3>Participant Portal</h3>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrorMsg" CssClass="redtext" runat="server"></asp:Label>
                <asp:Label ID="lblUserMessage" CssClass="greentext" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
	            <table>
	                <tr>
		                <td>Username:</td>
		                <td align="center">
		                    <asp:TextBox ID="txtLoginName" runat="server" columns="21"/>
		                </td>
                        <td>
                            <asp:RequiredFieldValidator ID="reqtxtLoginName" runat="server" 
                                Visible="true" ValidationGroup="valGrpLogin"
                                ControlToValidate="txtLoginName" Display="Dynamic" 
                                ErrorMessage="User Name is Required!"></asp:RequiredFieldValidator>
                        </td>
		            </tr>
	                <tr>
		                <td>Password: &nbsp</td>
		                <td align="center">
		                    <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" columns="21"/>

		                </td>
                        <td>
                             <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" 
                                Visible="true" ValidationGroup="valGrpLogin"
                                ControlToValidate="txtPassword" Display="Dynamic" 
                                ErrorMessage="Password is Required!"></asp:RequiredFieldValidator>
                        </td>
		            </tr>
		            <tr>
		                <td colspan="2">
                            <asp:Button ID="btnLogin" runat="server" Text="Log In" 
                                OnClick="btnLogin_Click" CausesValidation="true"
                                ValidationGroup="valGrpLogin" />
		                </td>
	                </tr>
	            </table>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
    </table>
</asp:Content>
