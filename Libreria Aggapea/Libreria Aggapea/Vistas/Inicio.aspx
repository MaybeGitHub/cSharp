<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Libreria_Aggapea.Vistas.WebForm1" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="welcomeUsuario" runat="server" Text="Bienvenido"></asp:Label>
    <div>
        <asp:Table ID="expositor_libros" runat="server"></asp:Table>
    </div>
        <asp:TextBox ID="TextBox1" runat="server" Height="360px" TextMode="MultiLine" Width="755px"></asp:TextBox>
    </form>
</body>
</html>
