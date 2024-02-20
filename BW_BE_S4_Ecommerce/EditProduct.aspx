<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="BW_BE_S4_Ecommerce.EditProduct" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        NOME:<asp:TextBox ID="TxtNome" runat="server"></asp:TextBox>
        DESCRIZIONE:<asp:TextBox ID="TxtDescrizione" runat="server"></asp:TextBox>
        PREZZO:<asp:TextBox ID="TxtPrezzo" runat="server"></asp:TextBox>
        IMMAGINE:<asp:TextBox ID="TxtImmagineUrl" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click" />
</asp:Content>
