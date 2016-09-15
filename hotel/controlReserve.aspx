<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="controlReserve.aspx.cs" Inherits="hotel.controlReservation"  MasterPageFile="~/Site.master"%>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

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
    
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">Insert Room</td>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Room</td>
                <td class="auto-style5">
                    <asp:TextBox ID="Room1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Submit Room" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Insert Event</td>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Name</td>
                <td class="auto-style5">
                    <asp:TextBox ID="Name" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">Start Date</td>
                <td class="auto-style6">
                    <asp:TextBox ID="Start" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="Start_CalendarExtender" runat="server" TargetControlID="Start" />
                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style3">End Date</td>
                <td class="auto-style5">
                    <asp:TextBox ID="End" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="End_CalendarExtender" runat="server" TargetControlID="End" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Room</td>
                <td class="auto-style5">
                    <asp:DropDownList ID="Room2" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:daypilot %>" SelectCommand="SELECT [name] FROM [resource]"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:Button ID="Submit_Event" runat="server" Text="Submit Event" OnClick="Submit_Event_Click" />
                </td>
            </tr>
            <tr>
                <td>Delete</td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td></td>
                </tr>
            <tr>
                <td>Room</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Submit_Delete1" runat="server" OnClick="Submit_Delete1_Click" Text="Submit Delete" />
                </td>
                </tr>
            <tr>
                <td>Event</td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="name">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:daypilot %>" SelectCommand="SELECT [name], [resource_id], [eventstart], [eventend] FROM [event]"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:Button ID="Submit_Delete2" runat="server" OnClick="Submit_Delete2_Click" Text="Submit Delete" />
                </td>
                </tr>
            </table>
    
    
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style3 {
            width: 96px;
        }
        .auto-style4 {
            height: 23px;
            width: 96px;
        }
        .auto-style5 {
            width: 194px;
        }
        .auto-style6 {
            height: 23px;
            width: 194px;
        }
    </style>
</asp:Content>

