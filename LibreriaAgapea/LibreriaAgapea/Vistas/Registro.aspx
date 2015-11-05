<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="LibreriaAgapea.Vistas.Registro" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>La rue del webmaster</title>
    <style type="text/css">
        .auto-style1 {
            width: 119px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <table style="width: 100%">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="usuario_Label" runat="server" Text="Usuario"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="usuario_TxBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="usuario_FV" runat="server" ControlToValidate="usuario_TxBox" ErrorMessage="No se puede dejar este campo vacio" Font-Size="Small" ForeColor="#990033" Display="Dynamic">No se puede dejar este campo vacio</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="usuarioExiste_FV" runat="server" ControlToValidate="usuario_TxBox" Display="Dynamic" ErrorMessage="El usuario ya existe" Font-Size="Small" ForeColor="#990033" OnServerValidate="usuarioExiste_FV_ServerValidate">El usuario ya existe</asp:CustomValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="pass_Label" runat="server" Text="Contraseña"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="pass_TxBox" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="pass_FV" runat="server" ControlToValidate="pass_TxBox" ErrorMessage="No se puede dejar este campo vacio" Font-Size="Small" ForeColor="#990033" Display="Dynamic">No se puede dejar este campo vacio</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="passLong_V" runat="server" Font-Size="Small" ForeColor="#990033" OnServerValidate="passLong_V_ServerValidate" Display="Dynamic">La contraseña tiene que tener mas de 8 caracteres</asp:CustomValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="pass2_Label" runat="server" Text="Repite contraseña"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="pass2_TxBox" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="pass2_CV" runat="server" ControlToCompare="pass_TxBox" ControlToValidate="pass2_TxBox" ErrorMessage="Las contraseñas tienen que coincidir" Font-Size="Small" ForeColor="#990033">Las contraseñas tienen que coincidir</asp:CompareValidator>
                </td>
                <td>
                    <asp:CheckBox ID="alma_ChkBox" runat="server" ForeColor="#666699" Text="Acepto vender mi alma" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:CustomValidator ID="almaCheck_V" runat="server" ErrorMessage="Debes vender tu alma para continuar" Font-Size="Small" ForeColor="#6600CC" OnServerValidate="almaCheck_V_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:ImageButton ID="registrar_boton" runat="server" Height="29px" ImageUrl="~/Imagenes/registrar.png" OnClick="registrar_boton_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
        <asp:TextBox ID="TextBox1" runat="server" Height="171px" TextMode="MultiLine" Width="753px"></asp:TextBox>
    </form>
</body>
</html>