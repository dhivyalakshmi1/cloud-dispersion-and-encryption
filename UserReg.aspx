﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.master" AutoEventWireup="true" CodeFile="UserReg.aspx.cs" Inherits="UserReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
    <br />
</p>
<p>
</p>
<table style="width: 100%; color: #000000; font-weight: 700">
    <tr>
        <td rowspan="14" style="width: 173px">
            <asp:Image ID="Image1" runat="server" Height="290px" 
                ImageUrl="~/icons/vector-10-512.png" Width="274px" />
        </td>
        <td colspan="2" style="text-align: center">
            <asp:Label ID="Label1" runat="server" style="font-size: large" 
                Text="New User Registration"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label3" runat="server" Text="Gender"></asp:Label>
        </td>
        <td>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label4" runat="server" Text="Age"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label5" runat="server" Text="Mobile"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label12" runat="server" Text="Address"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label9" runat="server" Text="User Name"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label10" runat="server" Text="Password"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox9" runat="server" TextMode="Password"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Label ID="Label11" runat="server" Text="Retype Password"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox10" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ControlToCompare="TextBox9" ControlToValidate="TextBox10" 
                ErrorMessage="Password Mismatch" ForeColor="Red"></asp:CompareValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                style="font-weight: 700; font-style: italic" Text="Clear" />
        </td>
        <td>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                style="font-style: italic; font-weight: 700" Text="Submit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 150px">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<p>
</p>
<p>
</p>
<p>
</p>
<p>
</p>
</asp:Content>

