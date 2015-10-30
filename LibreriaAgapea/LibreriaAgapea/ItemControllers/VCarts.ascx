<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VCarts.ascx.cs" Inherits="LibreriaAgapea.ItemControllers.VCarts" %>
<html>
    <body>
        <asp:Table ID="table_Cart" runat="server">
            <asp:TableRow>
                <asp:TableCell Width="70%">
                    <asp:Label ID="label_Title" runat="server" Text="Titulo"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="20%">
                    <asp:Label ID="label_Count" runat="server" Text="x Cantidad"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="10%">
                    <asp:Button ID="button_Erase" runat="server" Text="X" ForeColor="Red" BorderStyle="Inset"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </body>
    
</html>
