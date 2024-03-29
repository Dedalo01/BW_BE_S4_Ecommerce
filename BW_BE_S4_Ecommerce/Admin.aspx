﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="BW_BE_S4_Ecommerce.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body class="bg-info d-flex justify-content-center align-items-center">
    <form id="form1" runat="server">



        <div class="border border-black p-3 rounded m-2 bg-white">
            <div>
                <asp:Button ID="Button1" runat="server" Text="Home" OnClick="Button1_Click" />
            </div>


            <h3>Aggiungi Prodotto</h3>

            <div>
                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Nome"></asp:Label>
                    <div>
                        <asp:TextBox ID="nomeProdottoBox" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Descrizione"></asp:Label>
                    <div>
                        <asp:TextBox ID="descrizioneProdottoBox" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Prezzo"></asp:Label>
                    <div>
                        <asp:TextBox ID="prezzoProdottoBox" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="ImmageUrl"></asp:Label>
                    <div>
                        <asp:TextBox ID="immageUrlProdottoBox" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="Login" runat="server" Text="Aggiungi Prodotto" OnClick="Inserimento_Click" CssClass="btn btn-primary d-block mx-auto" />
            </div>

        </div>





        <div class="border border-black p-3 rounded m-2 bg-white">
            <h3>Prodotti</h3>

            <ul class="p-0">
                <asp:Repeater ID="ProductRepeaterAdmin" runat="server" OnItemCommand="ProductRepeaterAdmin_ItemCommand">
                    <ItemTemplate>
                        <li class="d-flex flex-column mt-3 border p-1 rounded">
                            <div>
                                <div class="d-flex justify-content-center">
                                    <img src='<%# Eval("ImmagineUrl") %>' alt="Immagine del prodotto" width="250" />

                                    <div class="d-flex flex-column justify-content-between">
                                        <strong>Nome:</strong>
                                        <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                        <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Eval("Nome") %>' Visible="false"></asp:TextBox><br />

                                        <strong>Prezzo:</strong>
                                        <asp:Label ID="PrezzoLabel" runat="server" Text='<%# Eval("Prezzo") %>' Visible="true"></asp:Label>
                                        <asp:TextBox ID="PrezzoTextBox" runat="server" Text='<%# Eval("Prezzo") %>' Visible="false"></asp:TextBox><br />

                                        <strong>Immagine url:</strong>
                                        <asp:Label ID="ImmagineUrlLabel" runat="server" Text='<%# Eval("ImmagineUrl") %>' Visible="true"></asp:Label>
                                        <asp:TextBox ID="ImmagineUrlTextBox" runat="server" Text='<%# Eval("ImmagineUrl") %>' Visible="false"></asp:TextBox><br />

                                        <strong>Descrizione:</strong>
                                        <asp:Label ID="DescrizioneLabel" runat="server" Text='<%# Eval("Descrizione") %>' Visible="true"></asp:Label>
                                        <asp:TextBox ID="DescrizioneTextBox" runat="server" Text='<%# Eval("Descrizione") %>' Visible="false"></asp:TextBox><br />


                                        <div class="d-flex justify-content-center">
                                            <asp:Button ID="EditButton" runat="server" CssClass="btn btn-primary" Text="Modifica Prodotto" CommandName="Modifica" CommandArgument='<%# Eval("ID") %>' />
                                            <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-danger" Text="Cancella Prodotto" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                            <asp:Button ID="ConfirmButton" runat="server" CssClass="btn btn-primary" Text="Conferma Modifiche" CommandName="Confirm" CommandArgument='<%# Eval("ID") %>' Visible="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </li>

                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </form>
</body>

</html>
