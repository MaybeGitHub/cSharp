<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VCarrito.ascx.cs" Inherits="LibreriaAgapea.ControladoresObjetos.VCarrito" %>
<html>
    <body>
        <asp:Table ID="table_Libro" runat="server" Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Width="60%">
                    <asp:Label ID="label_Titulo" runat="server" Text="Titulo" style="margin-left:3%" Width="100%"></asp:Label>
                </asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Center" Width="20%">
                    <asp:ImageButton ID="imgButton_Down" runat="server" ImageUrl="~/Imagenes/Botonera/menos.jpg" />                    
                    <asp:TextBox ID="label_Cantidad" runat="server" ReadOnly="true" Width="30px" style="text-align:center"></asp:TextBox>
                    <asp:ImageButton ID="imgButton_Up" runat="server" ImageUrl="~/Imagenes/Botonera/mas.jpg" />
                </asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Right" Width ="15%">
                    <asp:TextBox ID="label_Precio" runat="server" Width="50px" ReadOnly="True" style="text-align:center"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Center" Width="5%">
                    <asp:ImageButton ID="imgButton_Borrar" runat="server" ImageUrl="~/Imagenes/Botonera/equis.jpg" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </body>
</html>
