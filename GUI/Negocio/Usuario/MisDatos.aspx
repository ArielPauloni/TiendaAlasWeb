<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisDatos.aspx.cs" Inherits="GUI.Negocio.Usuario.MisDatos" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>
<%@ Register Src="~/User_Controls/UC_Procesando.ascx" TagPrefix="uc1" TagName="UC_Procesando" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">

    <style>
        div.custom_file_upload {
            width: 50px;
            height: 30px;
            margin: 20px 0;
        }

        div.file_upload {
            /*width: 150px;*/
            width: 200px;
            height: 30px;
            background: #7abcff;
            background: -moz-linear-gradient(top, #7abcff 0%, #60abf8 44%, #4096ee 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#7abcff), color-stop(44%,#60abf8), color-stop(100%,#4096ee));
            background: -webkit-linear-gradient(top, #7abcff 0%,#60abf8 44%,#4096ee 100%);
            background: -o-linear-gradient(top, #7abcff 0%,#60abf8 44%,#4096ee 100%);
            background: linear-gradient(top, #7abcff 0%,#60abf8 44%,#4096ee 100%);
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#7abcff', endColorstr='#4096ee',GradientType=0 );
            display: inline;
            position: absolute;
            overflow: hidden;
            cursor: pointer;
            -webkit-border-top-right-radius: 5px;
            -webkit-border-bottom-right-radius: 5px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomright: 5px;
            border-radius: 5px;
            font-weight: bold;
            color: #FFF;
            text-align: center;
            padding-top: 5px;
        }

            div.file_upload:before {
                content: '<%=getFileText()%>';
                position: absolute;
                left: 0;
                right: 0;
                text-align: center;
                cursor: pointer;
            }

            div.file_upload input {
                position: relative;
                height: 30px;
                width: 250px;
                display: inline;
                cursor: pointer;
                opacity: 0;
            }
    </style>

    <br />
    <br />

    <div class="container">
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblNombre" runat="server" Text="-Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblApellido" runat="server" Text="-Apellido"></asp:Label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblAlias" runat="server" Text="-Alias"></asp:Label>
                <asp:TextBox ID="txtAlias" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblMail" runat="server" Text="-Mail"></asp:Label>
                <asp:TextBox ID="txtMail" CssClass="form-control" placeholder="usuario@mail.com" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblPass1" runat="server" Text="-Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass1" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblPass2" runat="server" Text="-Repita Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass2" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblTelefono" runat="server" Text="-Telefono"></asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="form-control" placeholder="11 4444-44444" TextMode="Phone" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblFechaNacimiento" runat="server" Text="-Fecha Nacimiento"></asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblFotoPerfil" runat="server" Text="-Foto de Perfil"></asp:Label>
                <asp:Literal ID="litFotoPerfil" runat="server"></asp:Literal>
            </div>
            <div class="form-group col-md-6">
                <div class="custom_file_upload">
                    <div id="divArchivo" class="file_upload" runat="server">
                        <asp:FileUpload ID="fuCargarFotoPerfil" runat="server" />
                    </div>
                </div>
                <%--<asp:FileUpload ID="fuCargarFotoPerfil" runat="server" />--%>
                <button runat="server" id="btnCargarFotoPerfil" class="btn btn-primary fa fa-upload" onserverclick="btnCargarFotoPerfil_ServerClick" onclick="processingShow();"></button>
            </div>
        </div>

        <div runat="server" visible="false">
            <asp:DropDownList ID="ddlTipoUsuario" runat="server"></asp:DropDownList>
        </div>

    </div>
    <div class="form-row">
        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_Click" onclick="processingShow();"></button>
        </div>
    </div>

    <!-- Modal Mensajes -->
    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
        </div>
    </div>

    <!-- Modal Procesando -->
    <div class="modal fade" id="processingModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_Procesando runat="server" ID="UC_Procesando" />
        </div>
    </div>

    <script type="text/javascript">
        function processingShow() {
            $('#processingModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>
