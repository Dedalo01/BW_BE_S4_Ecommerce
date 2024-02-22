<%@ Page Title="Details" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Details" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div>
    <h2 id="txtProduct" runat="server"></h2>
    <img id="img" alt=""  runat="server" />
    <p id="txtDescription" runat="server"></p>
    <p id="txtPrice" runat="server"></p>
    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="1" type="number"></asp:TextBox>
    <asp:Button id="btnAddCart" runat="server" Text="Aggiungi al carrello" OnClick="btnAddCart_Click" />
        <asp:Button id="btnDelete" runat="server" Text="Cancella Prodotto" OnClick="btnDelete_Click" />
        <asp:Button id="btnEdit" runat="server" Text="Modifica Prodotto" OnClick="btnEdit_Click" />



    </div>
</asp:Content>
