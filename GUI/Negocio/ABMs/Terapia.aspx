<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terapia.aspx.cs" Inherits="GUI.Negocio.ABMs.Terapia" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Terapias" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div class="container">
        <div class="form-row">
            <div class="form-group col-md-4">
                <asp:Label ID="lblDescripcionTerapia" runat="server" Text="-Terapia"></asp:Label>
                <asp:TextBox ID="txtDescripcionTerapia" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <asp:Label ID="lblDuracion" runat="server" Text="-Duracion"></asp:Label>
                <asp:DropDownList ID="ddlDuracion" CssClass="form-control" runat="server">
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>60</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>90</asp:ListItem>
                    <asp:ListItem>105</asp:ListItem>
                    <asp:ListItem>120</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-4">
                <asp:Label ID="lblPrecio" runat="server" Text="-Costo"></asp:Label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />

        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_ServerClick"></button>
            <button runat="server" id="btnCancelar" class="btn btn-secondary fa fa-window-close-o" onserverclick="btnCancelar_ServerClick"></button>
        </div>
        <br />
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvTerapia" runat="server" AllowSorting="True" Caption="-Terapias"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="cod_Terapia" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTerapia_PageIndexChanging" OnRowCancelingEdit="grvTerapia_RowCancelingEdit" OnRowEditing="grvTerapia_RowEditing" OnRowUpdating="grvTerapia_RowUpdating" OnRowDataBound="grvTerapia_RowDataBound">
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
                        <asp:Label ID="lbl_cod_Terapia" runat="server" Text='<%#Eval("cod_Terapia") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Descripcion" runat="server" Text='<%#Eval("DescripcionTerapia") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Descripcion" runat="server" Text='<%#Eval("DescripcionTerapia") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Duracion (Minutos)" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Duracion" runat="server" Text='<%#Eval("Duracion") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_Duracion" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Precio" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Precio" runat="server" Text='<%#"$" + Eval("Precio") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Precio" runat="server" Text='<%#Eval("Precio") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal Mensaje -->
    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>
</asp:Content>
