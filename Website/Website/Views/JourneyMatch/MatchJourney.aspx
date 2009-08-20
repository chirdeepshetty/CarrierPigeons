<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DomainModel.Journey>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MatchJourney
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MatchJourney</h2>
    
    <table border="1">
        <tr align="center">
            <td>&nbsp;&nbsp</td>
            <td>User</td>
            <td>Email</td>
            <td>From</td>
            <td>To</td>
        </tr>
<%
            int count = 1;
            string checkbox = "";
            string from = "";
            string to = "";
            foreach (DomainModel.Match match in (IEnumerable)ViewData["MatchList"])
            {
%>
                <tr>
                    <td><input type='checkbox' name="<%=match.Id%>" ></td>
                    <td><%=match.Journey.Traveller.Name.FirstName%></td>
                    <td><%=match.Journey.Traveller.Email.EmailAddress%></td>
                    <td><%=match.Journey.Origin.Place%></td>
                    <td><%=match.Journey.Destination.Place%></td>
                </tr>
<%
    }
%>
    </table>    
</asp:Content>
