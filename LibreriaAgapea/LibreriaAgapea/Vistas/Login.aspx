<%@ Page Language="C#" MasterPageFile="~/Vistas/Index.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LibreriaAgapea.Vistas.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    
    <asp:Panel ID="central" runat="server" Width="40%" style="margin:0 auto">
        Login <asp:TextBox ID="nombre" runat="server"></asp:TextBox>
        <asp:CustomValidator ID="usuario_CV" runat="server" ControlToValidate="nombre" Display="Dynamic" ErrorMessage="El usuario ya existe" Font-Size="Small" ForeColor="#990033" OnServerValidate="usuario_CV_ServerValidate">Los datos son incorrectos</asp:CustomValidator>
        <br />Password <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        <br /><asp:Button ID="entrar" runat="server" Text="Entrar" OnClick="entrar_Click" />
    </asp:Panel>

</asp:Content>
