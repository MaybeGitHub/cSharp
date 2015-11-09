<%@ Page Title="" Language="C#" MasterPageFile="~/Vistas/Index.Master" AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs" Inherits="LibreriaAgapea.Vistas.Facturacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <br />
    <asp:Table ID="table_Info" runat="server" HorizontalAlign="Center" Width="70%" CellPadding="10">        
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" HorizontalAlign="Right">
                <asp:Label ID="nombre" runat="server" Text="Nombre: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Left">
                <asp:Label ID="campo_nombre" runat="server" Text="campo_nombre"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Right">
                <asp:Label ID="apellido" runat="server" Text="Apellido: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Left">
                <asp:Label ID="campo_apellido" runat="server" Text="campo_apellido"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">            
            <asp:TableCell runat="server" HorizontalAlign="Right">
                <asp:Label ID="direccion" runat="server" Text="Direccion: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Left">
                <asp:Label ID="campo_direccion" runat="server" Text="campo_direccion"></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Right">
                <asp:Label ID="email" runat="server" Text="Email: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Left">
                <asp:Label ID="campo_email" runat="server" Text="campo_email"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                <asp:Label ID="cesta" runat="server" Text="Cesta"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                <asp:ListBox ID="list_Cesta" runat="server" Enabled="False" Width="100%"></asp:ListBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                <asp:Button ID="button_Volver" runat="server" Text="Volver Atras" />
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                <asp:Button ID="button_Pagar" runat="server" Text="Estan bien, Pagar" />                
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
</asp:Content>