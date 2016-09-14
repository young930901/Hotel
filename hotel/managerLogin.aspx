<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managerLogin.aspx.cs" Inherits="hotel.manager" MasterPageFile="~/Site.master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">ID</td>
            <td>
                <asp:TextBox ID="ID" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Password</td>
            <td>
                <asp:TextBox ID="PW" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
    </table>


    
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />


    
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 25%;
        }
        .auto-style2 {
            width: 109px;
        }
    </style>
</asp:Content>

