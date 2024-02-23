<%@ Page Title="Home" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Home" %>

<asp:Content ID="home" ContentPlaceHolderID="main" runat="server">
     <!-- Hero con video -->
    <div class="hero-video">
        <video controls autoplay loop muted width="100%">
            <source src="Assets/Immagini/Ecommerce-video.mp4" controls="controls" type="video/mp4">
            Il tuo browser non supporta la riproduzione di video.
        </video>
        <div class="overlay"></div>
        <div class="hero-content">
            <h1>Benvenuto nel nostro negozio online</h1>
            <p>Le nostre migliori offerte del momento</p>
        </div>
    </div>

    <div class="container">
        <div id="RowCards" class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3" runat="server">



            <asp:Repeater ID="ProductRepeater" runat="server" OnItemCommand="ProductRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <img src='<%# Eval("ImmagineUrl") %>' class="card-img-top" alt='<%# Eval("Nome") %>'>
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title"><%# Eval("Nome") %></h5>
                                <p class="card-text">Prezzo: <%# Eval("Prezzo") %></p>
                                <a href='<%# "Details.aspx?product=" + Eval("Id") %>' class="btn btn-primary mt-auto">Dettagli</a>
                                <%--<asp:Button ID="DeleteButtonClick" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="Cancella" CssClass="btn" OnClientClick="return confirm('Sei sicuro di voler eliminare questo prodotto?')" />--%>
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
