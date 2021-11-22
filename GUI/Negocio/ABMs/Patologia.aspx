<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Patologia.aspx.cs" Inherits="GUI.Negocio.ABMs.Patologia" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Patologías" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div class="container">
        <div class="form-group col-md-12">
            <asp:Label ID="lblDescripcionPatologia" runat="server" Text="-Patologia"></asp:Label>
            <asp:TextBox ID="txtDescripcionPatologia" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <br />
        
        <div class="form-group">
            <button runat="server" id="btnGuardar" class="btn btn-primary fa fa-check-circle" onserverclick="btnGuardar_Click"></button>
            <button runat="server" id="btnCancelar" class="btn btn-secondary fa fa-window-close-o" onserverclick="btnCancelar_Click"></button>
        </div>
        <br />
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvPatologia" runat="server" AllowSorting="True" Caption="-Patologias"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="cod_Patologia" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvPatologia_PageIndexChanging" OnRowCancelingEdit="grvPatologia_RowCancelingEdit" OnRowEditing="grvPatologia_RowEditing" OnRowUpdating="grvPatologia_RowUpdating" OnRowDataBound="grvPatologia_RowDataBound">
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
                        <asp:Label ID="lbl_cod_Patologia" runat="server" Text='<%#Eval("cod_Patologia") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="-Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Descripcion" runat="server" Text='<%#Eval("DescripcionPatologia") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Descripcion" runat="server" Text='<%#Eval("DescripcionPatologia") %>'></asp:TextBox>
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