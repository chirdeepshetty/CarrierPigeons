<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Website.Models.CreateRequestResponse>" %>

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
        <%= ViewData["Message"] %>
        <% } %>
        <% using (Html.BeginForm())
           { %>
        <p>
            Origin:
            <%= Html.TextBox("OriginPlace") %></p>
        <p>
            Start Date:
            <%= Html.TextBox("OriginDate")%></p>
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
        <% } %>
    </div>
</asp:Content>
