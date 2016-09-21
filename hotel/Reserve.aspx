<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Reserve.aspx.cs" Inherits="Reserve" MasterPageFile="~/Site.master"  %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="space">
    <asp:LinkButton ID="LinkButtonSample" ForeColor="Gray" runat="server" OnClick="LinkButtonSample_Click">Clear and Generate Random Data</asp:LinkButton>
    </div>

    <DayPilot:DayPilotScheduler 
        ID="DayPilotScheduler1" 
        runat="server" 
        
        DataStartField="eventstart" 
        DataEndField="eventend" 
        DataTextField="name" 
        DataIdField="id" 
        DataResourceField="resource_id" 
        
        CellGroupBy="Month"
        Scale="Day"
        
        EventMoveHandling="CallBack" 
        OnEventMove="DayPilotScheduler1_EventMove" 
        >
    </DayPilot:DayPilotScheduler>
    
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Reset</asp:LinkButton>
    
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">User ID</td>
            <td class="auto-style4">
                <asp:TextBox ID="ID" runat="server" Height="16px" Width="237px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Password</td>
            <td class="auto-style4">
                <asp:TextBox ID="PW" runat="server" Width="236px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Room Number</td>
            <td class="auto-style4">
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:daypilot %>" SelectCommand="SELECT [name] FROM [resource]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Start Date</td>
            <td>
                <asp:TextBox ID="Start" runat="server" Height="16px" Width="238px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="Start_CalendarExtender" runat="server" TargetControlID="Start" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">End Date</td>
            <td>
                <asp:TextBox ID="End" runat="server" Height="16px" Width="236px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="End_CalendarExtender" runat="server" TargetControlID="End" />
            </td>
        </tr>
    </table>
    
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 34%;
        }
        .auto-style2 {
            width: 200px;
        }
        .auto-style3 {
            width: 200px;
            height: 23px;
        }
        .auto-style4 {
            height: 23px;
        }
    </style>
</asp:Content>


