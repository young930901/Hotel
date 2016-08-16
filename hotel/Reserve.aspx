<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Reserve.aspx.cs" Inherits="Reserve" MasterPageFile="~/Site.master"  %>
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
    
</asp:Content>
