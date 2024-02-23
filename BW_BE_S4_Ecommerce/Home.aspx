<%@ Page Title="Home" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Home" %>

<asp:Content ID="home" ContentPlaceHolderID="main" runat="server">
    <h1>STO FUNZIONANDO</h1>
   <%--<div id="myCarousel" class="carousel slide" data-ride="carousel" runat="server">
    <div class="carousel-inner">
        <div class="carousel-item active">
            <img src="https://lh3.googleusercontent.com/proxy/TqPkghmgamwOfOKP_zwVQgshf2NvlCaOqcz4lO44VgFcoKFOXA0vwrR0Bk2G9m-MUO_oYXnsmg5WS7LmPEAeJ8e-XA4pZleedMVPJXU7" class="d-block w-100" alt="Slide 1" />
        </div>
        <div class="carousel-item">
            <img src="https://lh3.googleusercontent.com/proxy/TqPkghmgamwOfOKP_zwVQgshf2NvlCaOqcz4lO44VgFcoKFOXA0vwrR0Bk2G9m-MUO_oYXnsmg5WS7LmPEAeJ8e-XA4pZleedMVPJXU7" class="d-block w-100" alt="Slide 2" />
        </div>
        <div class="carousel-item">
            <img src="https://lh3.googleusercontent.com/proxy/TqPkghmgamwOfOKP_zwVQgshf2NvlCaOqcz4lO44VgFcoKFOXA0vwrR0Bk2G9m-MUO_oYXnsmg5WS7LmPEAeJ8e-XA4pZleedMVPJXU7" class="d-block w-100 " alt="Slide 3" />
        </div>
    </div>
    <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="sr-only">Previous</span>
    </a>
    <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="sr-only">Next</span>
    </a>
</div>--%>
<div id="carouselExampleControlsNoTouching" class="carousel slide" data-bs-touch="false" style="width: 80%;  margin: auto;">
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img src="https://lh3.googleusercontent.com/proxy/TqPkghmgamwOfOKP_zwVQgshf2NvlCaOqcz4lO44VgFcoKFOXA0vwrR0Bk2G9m-MUO_oYXnsmg5WS7LmPEAeJ8e-XA4pZleedMVPJXU7" class=" i d-block w-100 h-100" alt="Slide 1">
    </div>
    <div class="carousel-item">
      <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8QtZUdWbQzjCQqWrSFLugs3fR2pkIktzxVw&usqp=CAU" class=" id-block w-100 h-100" alt="Slide 2">
    </div>
    <div class="carousel-item">
      <img src="https://brand-news.it/wp-content/uploads/2024/01/Immagine-campagna-Wallapop_3.jpg" class=" id-block  w-100 h-100" alt="Slide 3">
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControlsNoTouching" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControlsNoTouching" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>


    <div class="container mt-3">
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
