<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="container-fluid bg-info vh-100 d-flex justify-content-center align-items-center">
        <div class="col-4 bg-light p-4 rounded shadow">
            <h1 class="text-center mb-4">Effettua il Login</h1>
            <form id="form1" runat="server">
                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Username"></asp:Label>
                    <div>
                        <asp:TextBox ID="usernameBox" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Password"></asp:Label>
                    <div>
                        <asp:TextBox ID="passwordBox" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" CssClass="btn btn-primary d-block mx-auto" />
                <asp:Label ID="Label3" runat="server" Text="" CssClass="text-danger"></asp:Label>
                <asp:Button ID="btnVaiHome" runat="server" Text="Vai alla Home" OnClick="btnVaiHome_Click" CssClass="btn btn-primary d-block mx-auto mt-3" />
            </form>
        </div>
    </div>
</body>
</html>
