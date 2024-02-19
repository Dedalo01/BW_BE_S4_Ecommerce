<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
        <asp:Label ID="Label1" runat="server" Text="Nome"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

         <asp:Label ID="Label2" runat="server" Text="Cognome"></asp:Label>
 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

                <asp:Label ID="Label4" runat="server" Text="E-Mail"></asp:Label>
<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>

                <asp:Label ID="Label5" runat="server" Text="Password" TextMode="Password"></asp:Label>
<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="Username"></asp:Label>
<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
       
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
       
    </form>
</body>
</html>
