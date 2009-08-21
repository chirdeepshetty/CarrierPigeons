<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Website.Models.CreateRequestResponse>" %>
<%@ Import Namespace="DomainModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create a Request</h2>
    <div>
        <%= Html.ValidationSummary("Please correct the following to continue...") %>
        <% if (ViewData["Message"] != null)
           { %>
        <%= ViewData["Message"]%><br />
        <%= Html.ActionLink("Go back to homepage...", "Index", "Home") %>
        <% }
           else
           {%>
        <% using (Html.BeginForm())
           { %>
        <p>
            Origin:
            <%= Html.TextBox("OriginPlace")%></p>
        <p>
            Destination:
            <%= Html.TextBox("DestinationPlace")%></p>
        <p>
            Reach Date:
            <%= Html.TextBox("DestinationDate")%></p>
        <h4>
            Package Details:
        </h4>
        <p>
            Description:
            <%= Html.TextBox("PackageDescription")%></p>
        <p>
            Dimensions:
            <%= Html.TextBox("PackageDimensions")%></p>
        <p>
            Weight:
            <%= Html.TextBox("PackageWeight")%></p>
        <input type="submit" value="Submit Request" />
        <% }
           } %>
         <h4>Your All Requests</h4>
        <table >
        <tr><th>Origin</th><th>Destination</th><th>Package</th>
        <%
            IEnumerable<DomainModel.Request> allRequests = (IList<DomainModel.Request>) ViewData["AllRequests"];
                 
            foreach (Request request in allRequests)
            {
            %>
              <tr><td><%=request.Origin.Place%>  
               <td> <%=request.Destination.Place%> [<%=request.Destination.Date.ToShortDateString() %>] 
               <td><b>Desc:</b> <%=request.Package.Description %> <br />
                    <b>Dim:</b> <%=request.Package.Dimensions %> <br />
                    <b>Wt:</b><%= request.Package.Weight %>               
               </tr>
            <%}
           
           %>    
        </table>
    </div>
</asp:Content>
