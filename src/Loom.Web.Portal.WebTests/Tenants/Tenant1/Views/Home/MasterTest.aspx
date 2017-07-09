<%@ Page Language="C#" AutoEventWireup="false" Inherits="Loom.Web.Portal.UI.PortalView" ViewTemplate="Site" %>

<loom:Content runat="server" BoxId="headerBox">
    <h1>This is the header content</h1>
</loom:Content>

<loom:Content runat="server" BoxId="mainBox">
    <h2>This is the main section</h2>
    <loom:Button runat="server">Test</loom:Button>
</loom:Content>

<loom:Content runat="server" BoxId="footerBox">
    <h5>This is the footer content</h5>
</loom:Content>