<%@ Page Language="C#" MasterPageFile="~/Vistas/Index.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LibreriaAgapea.Vistas.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    
    <asp:Panel ID="central" runat="server" Width="45%" style="margin:0 auto">
        <asp:Table ID="table_Componentes" runat="server" Width="100%">

            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right" Width="35%">
                     Login 
                </asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Left" ColumnSpan="2">
                    <asp:TextBox ID="nombre" runat="server"></asp:TextBox><asp:RequiredFieldValidator ControlToValidate="nombre" ID="usuario_FV" Display="Dynamic" runat="server" Font-Size="Small" ForeColor="#990033" ErrorMessage="No se puede dejar el campo vacio"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="usuario_CV" runat="server" ControlToValidate="nombre" Display="Dynamic" ErrorMessage="Los datos son incorrectos" Font-Size="Small" ForeColor="#990033" OnServerValidate="usuario_CV_ServerValidate"></asp:CustomValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right">
                    Password
                </asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Left" ColumnSpan="2">
                    <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="contraseña_FV" runat="server" ControlToValidate="password" Display="Dynamic" Font-Size="Small" ForeColor="#990033" ErrorMessage="No se puede dejar el campo vacio"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Right">                    
                    <asp:Button ID="salir" runat="server" Text="Volver" PostBackUrl="~/Vistas/Centro.aspx" />
                </asp:TableCell>
                <asp:TableCell runat="server" Width="10%"></asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Left">
                    <asp:Button ID="entrar" runat="server" Text="Entrar" OnClick="entrar_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>
