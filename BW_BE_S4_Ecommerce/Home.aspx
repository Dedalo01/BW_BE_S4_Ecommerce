<%@ Page Title="Home" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Home" %>

<asp:Content ID="home" ContentPlaceHolderID="main" runat="server">
    <h1>STO FUNZIONANDO</h1>
        <div class="container">
        <div id="RowCards" class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3" runat="server">
            <div class="col">
                <div class="card h-100">
                  <img src="..." class="card-img-top" alt="...">
                  <div class="card-body">
                    <h5 class="card-title">Card title</h5>
                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                   
                          
                  </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
