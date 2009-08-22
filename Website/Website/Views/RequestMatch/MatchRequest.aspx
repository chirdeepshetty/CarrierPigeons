<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Match
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% if (ViewData["Message"] != null)
           { %>
        <%= ViewData["Message"]%><br />
        <% }%>
        
    <h2>Matched Requests</h2>
<%    
   using (Html.BeginForm("AcceptRequest","RequestMatch")) {%>
    <table border="1">
        <tr align="center">
            <td>&nbsp;&nbsp</td>
            <td>User</td>
            <td>Email</td>
            <td>From</td>
            <td>To</td>
            <td>Discriptiom</td>
            <td>Dimension</td>
            <td>Weight</td>
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
                    <td><input type='checkbox' name="acceptRequest" value="<%=match.Id%>" /></td>
                    <td><%=match.Request.RequestedUser.Name.FirstName %></td>
                    <td><%=match.Request.RequestedUser.Email.EmailAddress%></td>
                    <td><%=match.Request.Origin.Place%></td>
                    <td><%=match.Request.Destination.Place%></td>
                    <td><%=match.Request.Package.Description%></td>
                    <td><%=match.Request.Package.Dimensions %></td>
                    <td><%=match.Request.Package.Weight%> </td>   
                </tr>
<%
            }
%>
    </table>
    <br />
    <input type="submit" value="Accept" />
<%   }%> 
</asp:Content>
