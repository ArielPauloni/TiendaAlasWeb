<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TratamientoTerapias.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GUI.Negocio.ABMs.TratamientoTerapias" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="form-group col-md-12">
            <br />
            <br />
            <asp:Label ID="lblTratamientos" runat="server" Text="-Tratamientos"></asp:Label>
            <asp:DropDownList ID="ddlTratamientos" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddlTratamientos_SelectedIndexChanged"></asp:DropDownList>
            <br />
        </div>

        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblCantidadSesiones" runat="server" Text="-Cantidad de Sesiones"></asp:Label>
                <asp:DropDownList ID="ddlCantidadSesiones" CssClass="form-control" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblDescripcionTerapia" runat="server" Text="-Terapia"></asp:Label>
                <asp:DropDownList ID="ddlTerapias" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <br />

        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_ServerClick"></button>
        </div>
        <br />
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvTerapias" runat="server" AllowSorting="True" Caption="-Terapias" CssClass="table table-responsive"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Item1" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTerapias_PageIndexChanging" OnRowCancelingEdit="grvTerapias_RowCancelingEdit" OnRowEditing="grvTerapias_RowEditing" OnRowDeleting="grvTerapias_RowDeleting" OnRowDataBound="grvTerapias_RowDataBound">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Edit" CssClass="btn btn-mini" CommandName="Edit"><i class="fa fa-edit" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Undo" CssClass="btn btn-mini" CommandName="Cancel"><i class="fa fa-undo" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_Delete" CssClass="btn btn-mini" CommandName="Delete"><i class="fa fa-trash" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID Terapia">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Cod_Terapia" runat="server" Text='<%#Eval("Item1.Cod_Terapia") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CantidadSesiones" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CantidadSesiones" runat="server" Text='<%#Eval("Item2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DescripcionTerapia">
                    <ItemTemplate>
                        <asp:Label ID="lbl_DescripcionTerapia" runat="server" Text='<%#Eval("Item1.DescripcionTerapia") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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
