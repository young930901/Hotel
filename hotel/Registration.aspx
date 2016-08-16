<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="hotel.Registration" MasterPageFile="~/Site.Master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auto-style12">
    
        <div class="auto-style12">
            <h3 class="auto-style13"><strong>Registration form</strong></h3>
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style9">User Name</td>
                <td class="auto-style10">
                    <asp:TextBox ID="TextBoxUN" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style11">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUN" ErrorMessage="Please enter username" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Email</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style14">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Please enter email" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Email is not valid" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Password</td>
                <td class="auto-style7">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="TextBoxPass" runat="server"></asp:TextBox>
                    <ajaxToolkit:PasswordStrength ID="TextBoxPass_PasswordStrength" runat="server" MinimumLowerCaseCharacters="1" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="5" TargetControlID="TextBoxPass" />
                </td>
                <td class="auto-style2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPass" ErrorMessage="Please enter password" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Confirm Password</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxConPass" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style14">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxConPass" ErrorMessage="Confirm passwor is require" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxPass" ControlToValidate="TextBoxConPass" ErrorMessage="Password doesn't match" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Countrytry</td>
                <td class="auto-style6">
                    <asp:DropDownList ID="SelectCountry" runat="server">
                        <asp:ListItem>Selecte Country</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
                        <asp:ListItem>France</asp:ListItem>
                        <asp:ListItem>China</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style14">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="SelectCountry" ErrorMessage="Please select country" ForeColor="Red" InitialValue="Selecte Country"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="BSumit" runat="server" OnClick="BSumit_Click" Text="Sumit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Reset1" type="reset" value="Reset" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        #Text1 {
            width: 160px;
        }
        #Text2 {
            width: 158px;
        }
    </style>
</asp:Content>

