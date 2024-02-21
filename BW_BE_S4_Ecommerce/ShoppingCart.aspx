<%@ Page Title="Carrello" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="BW_BE_S4_Ecommerce.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="container">
        <div class="row">
            <asp:Repeater ID="CartRepeater" runat="server" OnItemCommand="CartRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="col-12 d-flex gap-3 align-items-center mb-3">

                        <div>
                            <p><%# Eval("Nome") %></p>
                        </div>

                        <div>
                            <p><%# Eval("Prezzo") %></p>
                        </div>

                        <div>
                            <p><%# Eval("Quantita") %></p>
                        </div>

                        <div>
                            <asp:Button runat="server" ID="RimuoviBtn" CommandName="Rimuovi" Text="Rimuovi" CommandArgument='<%# Eval("ProdottoId") %>' />
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>

<asp:Repeater ID="rptCartItems" OnItemCommand="rptCartItems_ItemCommand" runat="server">
    <ItemTemplate>
        <div>
            <span><%# Eval("Nome") %></span>
            <span><%# Eval("Descrizione") %></span>
            <asp:Button  runat="server" CommandName="Delete"  CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger w-75" Text="Delete" OnClientClick="return confirm('sicuro?');" />
        </div>
    </ItemTemplate>
</asp:Repeater>





            <asp:Label ID="DebugLabel" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblProdotto" runat="server" Text=""></asp:Label>
          
        </div>

    </div>
</asp:Content>
