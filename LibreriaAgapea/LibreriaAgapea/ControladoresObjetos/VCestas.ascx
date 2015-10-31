<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VCestas.ascx.cs" Inherits="LibreriaAgapea.ControladoresObjetos.VCestas" %>
<html>
    <body>
        <asp:Table ID="table_Cesta" runat="server">
            <asp:TableRow>
                <asp:TableCell Width="70%">
                    <asp:Label ID="label_Titulo" runat="server" Text="Titulo"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="label_Cantidad" runat="server" Text="x Cantidad"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="10%">
                    <asp:Button ID="button_Borrar" runat="server" Text="X" ForeColor="Red" BorderStyle="Inset"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </body>
    
</html>
