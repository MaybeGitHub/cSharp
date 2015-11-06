<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LibreriaAgapea.Vistas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="path" runat="server">
            <asp:Table ID="table_Path" runat="server"></asp:Table>
        </asp:Panel>
        <asp:Panel ID="central" runat="server" Width="40%" style="margin-left: 30%">
            Login <asp:TextBox ID="nombre" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="usuario_FV" runat="server" ControlToValidate="nombre" Display="Dynamic" ErrorMessage="No se puede dejar este campo vacio" Font-Size="Small" ForeColor="#990033">No se puede dejar este campo vacio</asp:RequiredFieldValidator>
            <asp:CustomValidator ID="usuario_CV" runat="server" ControlToValidate="nombre" Display="Dynamic" ErrorMessage="El usuario ya existe" Font-Size="Small" ForeColor="#990033" OnServerValidate="usuario_CV_ServerValidate">Los datos son incorrectos</asp:CustomValidator>
            <br />Password <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="password_FV" runat="server" ControlToValidate="password" Display="Dynamic" ErrorMessage="No se puede dejar este campo vacio" Font-Size="Small" ForeColor="#990033">No se puede dejar este campo vacio</asp:RequiredFieldValidator>
            <br /><asp:Button ID="entrar" runat="server" Text="Entrar" OnClick="entrar_Click" />
        </asp:Panel> 
    </div>
    <asp:TextBox ID="cajaInfo" runat="server" Height="200px" TextMode="MultiLine" Width="100%"></asp:TextBox>
    </form>
</body>
</html>
