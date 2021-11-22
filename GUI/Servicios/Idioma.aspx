<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Idioma.aspx.cs" Inherits="GUI.Servicios.Idioma" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="form-group col-md-12">
        <br />
        <br />
        <asp:Label ID="lblIdioma" runat="server" Text="-Idioma" Font-Bold="true"></asp:Label>
        :<asp:DropDownList ID="ddlIdiomas" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddlIdiomas_SelectedIndexChanged" Width="159px"></asp:DropDownList>
        <br />
        <button runat="server" id="btnMostrarFiltros" class="btn btn-primary fa fa-filter" onserverclick="btnMostrarFiltros_Click">
        </button>
    </div>

    <div id="divFiltros" runat="server" class="form-group col-md-12 bg-info" visible="false">
        <asp:Label ID="lblBuscarTexto" runat="server" Text="Label">: </asp:Label>
        <asp:DropDownList ID="ddlTipoBusqueda" runat="server"></asp:DropDownList>
        <asp:TextBox ID="txtTexto" runat="server"></asp:TextBox>
        <button id="btnFiltrar" runat="server" class="btn btn-info btn-sm fa fa-filter" onserverclick="btnFiltrar_Click"></button>
        <button id="btnLimpiarFiltros" runat="server" class="btn btn-info btn-sm fa fa-undo" onserverclick="btnLimpiarFiltros_Click"></button>
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvTexto" runat="server" AllowSorting="True" Caption="-Textos"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="idFrase" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTexto_PageIndexChanging" OnRowCancelingEdit="grvTexto_RowCancelingEdit" OnRowEditing="grvTexto_RowEditing" OnRowUpdating="grvTexto_RowUpdating" OnRowDataBound="grvTexto_RowDataBound">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Edit" class="btn btn-mini" CommandName="Edit"><i class="fa fa-edit" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Update" class="btn btn-mini" CommandName="Update"><i class="fa fa-check" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_Undo" class="btn btn-mini" CommandName="Cancel"><i class="fa fa-undo" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_idFrase" runat="server" Text='<%#Eval("idFrase") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Texto" runat="server" Text='<%#Eval("Texto") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Texto" runat="server" Text='<%#Eval("Texto") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="form-group col-md-12">
        <br />
        <asp:Button ID="btnCrearNuevoIdioma" CssClass="btn btn-primary" runat="server" Text="-Crear Nuevo Idioma" OnClick="btnCrearNuevoIdioma_Click" />
    </div>

    <!-- Modal Mensaje -->
    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-labelledby="MensajeModalTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
             <asp:UpdatePanel runat="server" ID="UpPanelDialog" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

     <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal();
        }
    </script>
</asp:Content>

