<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Idioma.aspx.cs" Inherits="GUI.Servicios.Idioma" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="form-group col-md-12">
        <br />
        <asp:Label ID="lblIdioma" runat="server" Text="-Idioma" Font-Bold="true"></asp:Label>
        :<asp:DropDownList ID="ddlIdiomas" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control form-control-select" OnSelectedIndexChanged="ddlIdiomas_SelectedIndexChanged" Width="159px"></asp:DropDownList>
    </div>

    <div class="form-group col-md-12">
        <asp:GridView ID="grvTexto" runat="server" AllowSorting="True" Caption="-Textos"
            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="idFrase" PageSize="20" EnableTheming="True" OnPageIndexChanging="grvTexto_PageIndexChanging" OnRowCancelingEdit="grvTexto_RowCancelingEdit" OnRowEditing="grvTexto_RowEditing" OnRowUpdating="grvTexto_RowUpdating">
            <AlternatingRowStyle BackColor="#CCFFFF" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Edit" class="btn btn-mini" CommandName="Edit"><i class="fa fa-pencil" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <%--<asp:Button ID="btn_Edit" runat="server" Text="-Edit" CommandName="Edit" />--%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" ID="btn_Update" class="btn btn-mini" CommandName="Update"><i class="fa fa-check" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btn_Cancel" class="btn btn-mini" CommandName="Cancel"><i class="fa fa-undo" aria-hidden="true"></i>
                        </asp:LinkButton>
                        <%--<asp:Button ID="btn_Update" runat="server" Text="-Update" CommandName="Update" />
                        <asp:Button ID="btn_Cancel" runat="server" Text="-Cancel" CommandName="Cancel" />--%>
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
                <%--<asp:BoundField DataField="idFrase" />
                <asp:BoundField DataField="Texto" HeaderText="-Texto">
                    <HeaderStyle CssClass="th" />
                </asp:BoundField>--%>
            </Columns>
        </asp:GridView>
    </div>

    <div class="form-group col-md-12">
        <br />
        <asp:Button ID="btnCrearNuevoIdioma" CssClass="btn btn-primary" runat="server" Text="-Crear Nuevo Idioma" OnClick="btnCrearNuevoIdioma_Click" />
    </div>
</asp:Content>

