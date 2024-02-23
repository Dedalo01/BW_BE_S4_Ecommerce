<%@ Page Title="Details" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Details" %>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div>
        <h2 id="txtProduct" runat="server" style="margin-left: 20px;"></h2>
        <img id="img" alt="" runat="server" width="500" height="500" style="margin-left: 20px;" />
        <p id="txtDescription" runat="server" style="margin-left: 20px;"></p>
        <p id="txtPrice" runat="server" style="margin-left: 20px;"></p>
        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="1" type="number" style="width: 100px; margin-left: 20px;"></asp:TextBox>
        <asp:Button id="btnAddCart" runat="server" Text="Aggiungi al carrello" OnClick="btnAddCart_Click" style="margin-left: 20px; margin-left: 20px;" />

       <div style="display: flex; flex-direction: column; justify-content: flex-end; align-items: flex-end;">
            <img src="Assets/Immagini/picmix.com_1744809.gif" alt="fortunato" style="margin-bottom: 10px;" />
            <img src="Assets/Immagini/advFun.png" alt="advJesusLovesU" style="width: 200px; margin-bottom: 10px;" />
        </div>
    </div>
</asp:Content>