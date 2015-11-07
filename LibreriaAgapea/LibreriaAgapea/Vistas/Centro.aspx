<%@ Page Language="C#" MasterPageFile="~/Vistas/Index.Master" AutoEventWireup="true" CodeBehind="Centro.aspx.cs" Inherits="LibreriaAgapea.Vistas.Centro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:Panel ID="navegador" runat="server" style="height:15%" HorizontalAlign="Center">
        <asp:Panel ID="buscador" runat="server" HorizontalAlign="Center">
            <asp:Label ID="label_Buscador" runat="server" Text="Buscar Libros" style="margin-right:10px" Height="100%"></asp:Label>
            <asp:TextBox ID="text_Buscador" runat="server" Width="50%"></asp:TextBox>
            <asp:Button ID="button_Buscador" runat="server" Text="Buscar"/> 
            <br /><asp:Table ID="table_Radios" runat="server" HorizontalAlign="Center">
                <asp:TableRow ID="row_Radios" runat="server" HorizontalAlign="Center">
                    <asp:TableCell runat="server">
                        <asp:RadioButton ID="titulo" runat="server" GroupName="filtros" Checked="true" Text="Titulo"/>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:RadioButton ID="autor" runat="server" GroupName="filtros" Text="Autor"/>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:RadioButton ID="editorial" runat="server" GroupName="filtros" Text="Editorial"/>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:RadioButton ID="isbn10" runat="server" GroupName="filtros" Text="ISBN10"/>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:RadioButton ID="isbn13" runat="server" GroupName="filtros" Text="ISBN13"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table> 
        </asp:Panel>               
    </asp:Panel>
    <asp:panel ID="body" runat="server" style="height:60%" Width="100%">        
        <asp:panel ID="panel_Tree" runat="server" style="float:left; width:15%" BorderColor="RosyBrown" BorderWidth="1px">
            <asp:TreeView ID="tree_Categorias" runat="server" ShowLines="True" Width="100%">
            </asp:TreeView>
        </asp:panel>
        <asp:panel ID="panel_Libros" runat="server" style="float:left; width:70%">
                
            <asp:Table ID="table_Libros" runat="server" HorizontalAlign="Center">
            </asp:Table>
                
        </asp:panel>
        <asp:Panel ID="panel_Cesta" runat="server" style="float:left;width:14%">
            <asp:Table ID="table_Cesta" runat="server" CellSpacing="0"></asp:Table>
        </asp:Panel>
    </asp:panel>
</asp:Content>
