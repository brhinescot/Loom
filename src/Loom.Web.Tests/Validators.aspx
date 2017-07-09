<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Validators.aspx.cs" Inherits="Loom.Web.Tests.Validators" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="font-family: Tahoma, Sans-Serif; font-size: small;">
<form id="form1" runat="server">
    <div>
        <strong>
            A comment is required for selections other than 'Bad'.
        </strong><br/>
        Select<br/>
        <asp:DropDownList ID="dropDownList" runat="server" Width="155px">
            <asp:ListItem>Good</asp:ListItem>
            <asp:ListItem>Good2</asp:ListItem>
            <asp:ListItem>Bad</asp:ListItem>
        </asp:DropDownList><br/>
        Comment<br/>
        <asp:TextBox ID="dropDownComment" runat="server" BackColor="White"
                     BorderColor="#B7B7FF" BorderStyle="Solid" BorderWidth="1px">
        </asp:TextBox>
        <loom:ConditionalRequiredFieldValidator ID="ConditionalRequiredFieldValidator1"
                                                runat="server" ControlToCompare="dropDownList"
                                                ControlToValidate="dropDownComment" ErrorBackColor="Yellow"
                                                ErrorMessage="A comment is required for selections other than 'Bad'."
                                                ValueToCompare="Bad" Operator="NotEqual" ErrorBorderColor="Red"
                                                ErrorBorderStyle="Solid" ErrorBorderWidth="1px">
        </loom:ConditionalRequiredFieldValidator><br/>
        <br/>
        <br/>
        <strong>A state is required if a license is entered.</strong><br/>
        License<br/>
        <asp:TextBox ID="licenseNumber" runat="server"></asp:TextBox><br/>
        State<br/>
        <asp:DropDownList ID="stateList" runat="server" Width="155px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="AZ">California</asp:ListItem>
            <asp:ListItem Value="CA">Arizona</asp:ListItem>
            <asp:ListItem Value="NV">Nevada</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<loom:ConditionalRequiredFieldValidator ID="Conditionalrequiredfieldvalidator5"
                                                      runat="server" ControlToCompare="licenseNumber" ControlToValidate="stateList"
                                                      ErrorMessage="A state is required if a license is entered."
                                                      Operator="NotEqual">
        </loom:ConditionalRequiredFieldValidator><br/>
        <br/>
        <br/>
        <strong>
            A comment is required for values less than 10.
        </strong><br/>
        Value <br/>
        <asp:TextBox ID="integerInput" runat="server"></asp:TextBox><br/>
        Comment<br/>
        <asp:TextBox ID="integerComment" runat="server"></asp:TextBox>
        &nbsp;<loom:ConditionalRequiredFieldValidator ID="Conditionalrequiredfieldvalidator2"
                                                      runat="server" ControlToCompare="integerInput" ControlToValidate="integerComment" ErrorMessage="A comment is required for values less than 10."
                                                      ValueToCompare="10" Operator="LessThan" Type="Double">
        </loom:ConditionalRequiredFieldValidator><br/>
        <br/>
        <br/>
        <strong>
            A comment is required for dates before 2/10/2007.
        </strong><br/>
        Date<br/>
        <asp:TextBox ID="dateInput" runat="server"></asp:TextBox><br/>
        Comment<br/>
        <asp:TextBox ID="dateComment" runat="server"></asp:TextBox>
        &nbsp;<loom:ConditionalRequiredFieldValidator ID="Conditionalrequiredfieldvalidator3"
                                                      runat="server" ControlToCompare="dateInput" ControlToValidate="dateComment" ErrorMessage="A comment is required for dates before 2/10/2007."
                                                      ValueToCompare="02/10/2007" Operator="LessThan" Type="Date">
        </loom:ConditionalRequiredFieldValidator><br/>
        <br/>
        <br/>
        <strong>A priority is required for high severity items.</strong><br/>
        Severity<br/>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Minimal</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
        </asp:DropDownList><br/>
        Priority<br/>
        <asp:DropDownList ID="DropDownList2" runat="server" BackColor="#66FFCC">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<loom:ConditionalRequiredFieldValidator ID="ConditionalRequiredFieldValidator4"
                                                      runat="server" ControlToCompare="DropDownList1" ControlToValidate="DropDownList2"
                                                      ErrorMessage="A priority is required for high severity items." InitialValue="Select"
                                                      Operator="Equal" ValueToCompare="High" ErrorBackColor="Yellow">
        </loom:ConditionalRequiredFieldValidator><br/>
        <br/>
        <br/>
        <strong>The check box must be checked.</strong><br/>
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Do you check me?"/>
        <br/>
        <asp:TextBox ID="CheckComment" runat="server" BackColor="White"
                     BorderColor="#B7B7FF" BorderStyle="Solid" BorderWidth="1px">
        </asp:TextBox>
        &nbsp;<loom:ConditionalRequiredFieldValidator ID="ConditionalRequiredFieldValidator6"
                                                      runat="server" ControlToCompare="CheckBox1" ControlToValidate="CheckComment"
                                                      ErrorMessage="A comment is required when checked."
                                                      Operator="Equal" ValueToCompare="true" ErrorBackColor="Yellow">
        </loom:ConditionalRequiredFieldValidator>
        <br/>
        <br/>
        <asp:Button ID="Button1" runat="server" Text="Button"/>
    </div>
</form>
</body>
</html>