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

 <div id="contentTot" runat="server" class="my-3 mx-4"></div>
 <ul id="htmlContent" runat="server" class="m-auto w-50">
     <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
         <ItemTemplate>
             <li class="d-flex justify-content-between">
                 <p class="whiteTest" ><%# Eval("Nome") %></p>
                 <div class="d-flex mb-2 align-items-baseline">
                     <p class="d-flex me-1 whiteTest"><%# Eval("Prezzo") %>€</p>
                     <asp:Button runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
                         CssClass="btn btn-danger w-75 " Text="🗑" OnClientClick="return confirm('Sei sicuro di voler eliminare questo elemento?');" />
                 </div>
             </li>
         </ItemTemplate>
     </asp:Repeater>
 </ul>
 
 <div class="bottoneDelete">
     <asp:Button runat="server" ID="btnClearSession" CssClass="btn btn-danger" Text="Svuota Carrello" OnClick="btnClearSession_Click" />
 </div>





            <asp:Label ID="DebugLabel" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblProdotto" runat="server" Text=""></asp:Label>
          
        </div>

    </div>
</asp:Content>
