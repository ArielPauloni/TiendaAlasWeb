<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProfesionalTratamiento.aspx.cs" Inherits="GUI.Negocio.ABMs.ProfesionalTratamiento" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Profesional / Tratamiento" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div class="container">
        <div class="form-group col-md-12">
            <asp:Label ID="lblTratamiento" runat="server" Text="-Tratamiento" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlTratamientos" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddTratamientos_SelectedIndexChanged"></asp:DropDownList>
            <br />
        </div>

        <div class="form-group col-md-4">
            <asp:Panel runat="server" ScrollBars="Auto" Height="50%">
                <asp:GridView ID="grvProfesionales" runat="server" AllowSorting="True" Caption="-Profesionales"
                    AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="cod_Usuario" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvProfesionales_PageIndexChanging">
                    <AlternatingRowStyle BackColor="#CCFFFF" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_cod_Usuario" runat="server" Text='<%#Eval("cod_Usuario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="-Apellido">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Apellido" runat="server" Text='<%#Eval("Apellido") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="-Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="-Seleccionar" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_ProfesionalSeleccionado" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>

        <div class="form-group col-md-2" style="text-align: center;">
            <br />
            <br />
            <br />
            <button runat="server" id="btnRelacionarTratProf" class="btn btn-primary fa fa-link" onserverclick="btnRelacionarTratProf_ServerClick"></button>
            <br />
            <br />
            <button runat="server" id="btnQuitarRelTratProf" class="btn btn-primary fa fa-unlink" onserverclick="btnQuitarRelTratProf_ServerClick"></button>
            <br />
        </div>

        <div class="form-group col-md-4">
            <asp:Panel runat="server" ScrollBars="Auto" Height="50%">
                <asp:GridView ID="grvProfesionalRelacionados" runat="server" AllowSorting="True" Caption="-Profesionales"
                    AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="cod_Usuario" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvProfesionalRelacionados_PageIndexChanging">
                    <AlternatingRowStyle BackColor="#CCFFFF" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_cod_Usuario" runat="server" Text='<%#Eval("cod_Usuario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="-Apellido">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Apellido" runat="server" Text='<%#Eval("Apellido") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="-Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="-Seleccionar" HeaderStyle-CssClass="th" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_ProfesionalSeleccionado" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
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
