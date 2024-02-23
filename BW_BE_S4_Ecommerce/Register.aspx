<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body class="bg-info">
    <div class="container-fluid bg-info vh-100 d-flex justify-content-center align-items-center">
        <div class="col-md-4">
            <div class="bg-light border border-dark rounded p-4">
                <h1 class="text-center mb-4">Registrazione</h1>
                <form id="form1" runat="server">
                    <div class="mb-3">
                        <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Nome"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Cognome"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="E-Mail"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Password"></asp:Label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="Conferma Password"></asp:Label>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Username"></asp:Label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="Button1" CssClass="btn btn-primary d-block mx-auto" runat="server" OnClick="Button1_Click" Text="Registrati" />
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="Annulla" OnClick="Annulla_Click" CssClass="btn btn-primary d-block mx-auto" />
                    <asp:Panel ID="pnlEmailExistsMessage" runat="server" Visible="false" CssClass="text-center mt-3">
                        <p>L'email è già stata utilizzata. Vuoi fare il login?</p>
                        <asp:Button ID="btnLogin" CssClass="btn btn-primary" runat="server" Text="Accedi" OnClick="btnLogin_Click" />

                    </asp:Panel>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
