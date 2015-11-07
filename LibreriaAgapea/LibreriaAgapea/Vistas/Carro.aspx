<%@ Page Language="C#" MasterPageFile="~/Vistas/Index.Master" AutoEventWireup="true" CodeBehind="Carro.aspx.cs" Inherits="LibreriaAgapea.Vistas.Carro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:Panel ID="Panel1" runat="server" Width="40%" style="margin:0 auto">
        <asp:TextBox runat="server" TextMode="MultiLine" ID="pruebaLibros" Width="100%"></asp:TextBox>
    </asp:Panel>    
</asp:Content>