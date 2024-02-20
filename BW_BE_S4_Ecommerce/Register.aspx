<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
     <script src="Scripts/bootstrap.min.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="d-flex justify-content-center align-items-center flex-column">
            <h1>Registrazione</h1>


   <form id="form1" runat="server" class="col-4 border border-black rounded p-2">
       <div class="mb-3">
            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Nome"></asp:Label>
            <div >
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>


       <div class="mb-3">
            <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Cognome"></asp:Label>
            <div >
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

       <div class="mb-3">
            <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="E-Mail"></asp:Label>
            <div >
               <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

       <div class="mb-3">
            <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Password"></asp:Label>
            <div >
                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
        </div>

       <div class="mb-3">
           <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="Conferma Password"></asp:Label>
           <div >
               <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
          </div>
        </div>

      <div class="mb-3">
        <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Username"></asp:Label>
        <div >
            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
     </div>

    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" OnClick="Button1_Click" Text="Registrati" />
</form>



   <asp:Panel ID="pnlEmailExistsMessage" runat="server" Visible="false">
    <p>L'email è già stata utilizzata. Vuoi fare il login?</p>
    <asp:Button ID="btnLogin" CssClass="btn btn-primary" runat="server" Text="Accedi" OnClick="btnLogin_Click" />
   </asp:Panel>
    </div>
    
</body>
</html>
