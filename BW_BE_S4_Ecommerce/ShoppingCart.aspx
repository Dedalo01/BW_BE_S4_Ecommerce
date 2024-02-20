<%@ Page Title="Carrello" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="BW_BE_S4_Ecommerce.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div>
        <asp:GridView ID="Carrello" runat="server" AutoGenerateColumns="false" OnRowCommand="Carrello_RowCommand">
            <Columns>
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Prezzo" HeaderText="Prezzo" />
                <asp:ButtonField ButtonType="Button" CommandName="Rimuovi" Text="Rimuovi" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
