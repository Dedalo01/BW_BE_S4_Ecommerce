<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="InsertProduct.aspx.cs" Inherits="BW_BE_S4_Ecommerce.InsertProduct" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:Label ID="LblProdottiTotali" runat="server" Text=""></asp:Label>
<br />
NOME:<asp:TextBox ID="TxtNome" runat="server"></asp:TextBox>
DESCRIZIONE:<asp:TextBox ID="TxtDescrizione" runat="server"></asp:TextBox>
PREZZO:<asp:TextBox ID="TxtPrezzo" runat="server"></asp:TextBox>
IMMAGINEURL:<asp:TextBox ID="TxtImmagineUrl" runat="server"></asp:TextBox>


<asp:Button ID="BtnCrea" runat="server" Text="Crea" OnClick="BtnCrea_Click"/>
</asp:Content>
