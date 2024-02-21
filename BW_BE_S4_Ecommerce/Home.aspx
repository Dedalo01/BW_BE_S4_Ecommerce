<%@ Page Title="Home" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Home" %>

<asp:Content ID="home" ContentPlaceHolderID="main" runat="server">
    <h1>STO FUNZIONANDO</h1>
    <div class="container">
        <div id="RowCards" class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3" runat="server">
            <asp:Repeater ID="ProductRepeater" runat="server" OnItemCommand="ProductRepeater_ItemCommand">
              <ItemTemplate>
    <div class="col">
        <div class="card h-100">
            <img src='<%# Eval("ImmagineUrl") %>' class="card-img-top img-fluid" alt='<%# Eval("Nome") %>' style="height: 500px;">
            <div class="card-body d-flex flex-column">
                <h5 class="card-title text-center mb-3"><%# Eval("Nome") %></h5>
                <p class="card-text text-center">Prezzo: <%# Eval("Prezzo") %></p>
                <div class="text-center">
                    <a href='<%# "Details.aspx?product=" + Eval("Id") %>' class="btn btn-primary">Dettagli</a>
                    <%--<asp:Button ID="DeleteButtonClick" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="Cancella" CssClass="btn" OnClientClick="return confirm('Sei sicuro di voler eliminare questo prodotto?')" />--%>
                </div>
            </div>
        </div>
    </div>
</ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div class="text-center">
        <asp:PlaceHolder ID="paginationContainer" runat="server" />
    </div>
</asp:Content>
<%--  --%>