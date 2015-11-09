<%@ Page Language="C#" MasterPageFile="~/Vistas/Index.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="LibreriaAgapea.Vistas.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">        
    <table style="width: 100%">
        <tr>
            <td class="auto-style1">
                <asp:Label ID="label_Nombre" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="text_Nombre" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="usuarioExiste_FV" runat="server" ControlToValidate="text_Nombre" Display="Dynamic" ErrorMessage="El usuario ya existe" Font-Size="Small" ForeColor="#990033" OnServerValidate="usuarioExiste_FV_ServerValidate">El usuario ya existe</asp:CustomValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="label_Apellido" runat="server" Text="Apellido"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="text_Apellido" runat="server"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="label_Contraseña" runat="server" Text="Contraseña"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="text_Contraseña" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CustomValidator ID="passLong_V" runat="server" Font-Size="Small" ForeColor="#990033" OnServerValidate="passLong_V_ServerValidate" Display="Dynamic">La contraseña tiene que tener mas de 8 caracteres</asp:CustomValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="label_Contraseña2" runat="server" Text="Repite contraseña"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="text_Contraseña2" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="pass2_CV" runat="server" ControlToCompare="text_Contraseña" ControlToValidate="text_Contraseña2" ErrorMessage="Las contraseñas tienen que coincidir" Font-Size="Small" ForeColor="#990033">Las contraseñas tienen que coincidir</asp:CompareValidator>
            </td>
            <td>
                <asp:CheckBox ID="alma_ChkBox" runat="server" ForeColor="#666699" Text="Acepto vender mi alma" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="label_Direccion" runat="server" Text="Direccion"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="text_Direccion" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CustomValidator ID="almaCheck_V" runat="server" ErrorMessage="Debes vender tu alma para continuar" Font-Size="Small" ForeColor="#6600CC" OnServerValidate="almaCheck_V_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="label_Email" runat="server" Text="Email"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="text_Email" runat="server"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:ImageButton ID="registrar_boton" runat="server" Height="29px" ImageUrl="~/Imagenes/registrar.png" OnClick="registrar_boton_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>