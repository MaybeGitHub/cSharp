<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Libreria_Aggapea.Vistas.WebForm1" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 246px;
        }
        .auto-style2 {
            width: 642px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>
    <asp:Label ID="welcomeUsuario" runat="server" Text="Bienvenido"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TreeView ID="catalogo_libros" runat="server" BorderStyle="Solid" ExpandDepth="0" ShowLines="True"></asp:TreeView>
                    
                </td>
                <td class="auto-style2">
        <asp:Table ID="expositor_libros" runat="server"></asp:Table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:TextBox ID="TextBox1" runat="server" Height="360px" TextMode="MultiLine" Width="755px"></asp:TextBox>
    </form>
</body>
</html>
