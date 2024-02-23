<%@ Page Title="Details" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Details" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div>
        <h2 id="txtProduct" runat="server"></h2>
        <img id="img" alt=""  runat="server" />
        <p id="txtDescription" runat="server"></p>
        <p id="txtPrice" runat="server"></p>
        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="1" type="number" style="width: 50px;"></asp:TextBox>
        <asp:Button id="btnAddCart" runat="server" Text="Aggiungi al carrello" OnClick="btnAddCart_Click" />

       <div style="display: flex; flex-direction: column; justify-content: flex-end; align-items: flex-end;">
            <img src="Assets/Immagini/picmix.com_1744809.gif" alt="fortunato" style="margin-bottom: 10px;" />
            <img src="Assets/Immagini/advFun.png" alt="advJesusLovesU" style="width: 200px; margin-bottom: 10px;" />
        </div>
    </div>
</asp:Content>