<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carro.aspx.cs" Inherits="LibreriaAgapea.Vistas.Carro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="welcome" runat="server" Text="Welcome"></asp:Label>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="pruebaLibros" Height="56px" Width="297px"></asp:TextBox>
        <asp:Button runat="server" ID="pruebaPostBack" Text="Clickeame" />
    </div>
    </form>
</body>
</html>
