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
                <td class="auto-style2">
                    <asp:Label ID="busqueda_Label" runat="server" Text="Busca Libro : "></asp:Label>
                    <asp:TextBox ID="busqueda_Tx" runat="server" BorderStyle="Solid" Height="20px"  Width="406px" BorderColor="Black" BorderWidth="1px" Wrap="False"></asp:TextBox>
                    <asp:Button ID="busqueda_Btn" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnClick="busqueda_Btn_Click" Text="Buscar" Width="79px" />
                    <asp:Table ID="tablaRadios" runat="server" Width="572px"></asp:Table>
                    <asp:TextBox ID="mostrarResultado_Tx" runat="server" Height="20px" TextMode="MultiLine" Width="575px" Visible="False" ReadOnly="True"></asp:TextBox>
                </td>
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
                <td>
                    <asp:Table ID="expositor_cesta" runat="server"></asp:Table>
                </td>
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
