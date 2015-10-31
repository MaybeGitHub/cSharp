<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VLibros.ascx.cs" Inherits="LibreriaAgapea.ControladoresObjetos.VLibros" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body style="width:350px; height:200px">
        <asp:Panel ID="body" runat="server" Width="100%" Height="100%">
            <asp:Table ID="table_Body" runat="server" Width="100%" Height="100%" CellPadding="5" CellSpacing="5">               
                <asp:TableRow runat="server" Height="90%" Width="100%">
                    <asp:TableCell runat="server" Width="30%" Height="100%">
                            <asp:ImageButton ID="imgbutton_Libro" runat="server" ImageUrl="~/Imagenes/NoImage.jpg" Width="100px" Height="120px"/>                                               
                    </asp:TableCell>                                                 
                    <asp:TableCell runat="server" Width="70%">
                        <asp:Label ID="label_Titulo" runat="server" Text="Title"></asp:Label>
                        <br /><asp:Label ID="label_Autor" runat="server" Text="Author"></asp:Label>
                        <br /><asp:Label ID="label_Categoria" runat="server" Text="Type"></asp:Label>
                        <br /><asp:Label ID="label_Editorial" runat="server" Text="editorial"></asp:Label>
                        <br /><asp:Label ID="label_ISBN0" runat="server" Text="ISBN0"></asp:Label>
                        <br /><asp:Label ID="label_ISBN1" runat="server" Text="ISBN1"></asp:Label>
                        <br /><asp:Label ID="label_Precio" runat="server" Text="Price €"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" Height="10%" ColumnSpan="2" HorizontalAlign="Center">
                        <asp:Button ID="button_Comprar" runat="server" Text="Comprar"/>
                    </asp:TableCell>
                </asp:TableRow>                
            </asp:Table>
        </asp:Panel>
</body>
</html>