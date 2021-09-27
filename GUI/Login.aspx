<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GUI.Login" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <style>
        .btnShowHidePass {
            background: #fafafa;
            margin: -5px;
            outline: none;
            border: 1px solid #eeeeee;
            padding: 15px;
            border-left: hidden;
        }
    </style>
    <br />
    <div class="wrap">
        <div class="login-form">
            <div class="form-header">
                <asp:Label ID="lblLogin" runat="server" Text="-Login page" Font-Bold="true"></asp:Label>
            </div>
            <!--Alias Input-->
            <div class="form-group">
                <label id="lblAlias" runat="server">-Alias</label>
                <asp:TextBox ID="txtAlias" runat="server" CssClass="form-input" AutoPostBack="true"></asp:TextBox>
            </div>
            <!--Password Input-->
            <div class="form-group">
                <label id="lblPassword" runat="server">-pass</label>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-inputPass" TextMode="Password"></asp:TextBox>
                <button id="btnShowHidePass" runat="server" class="btnShowHidePass fa fa-eye" onserverclick="ShowHidePass_click"></button>
            </div>
            <!--Login Button + Recupero pass + Recordar datos-->
            <div class="form-group">
                <asp:Button CssClass="form-button" ID="btnLogin" runat="server" Text="-LOGIN" OnClick="btnLogin_Click" />
            </div>
            <div class="form-group">
                <asp:CheckBox ID="chkRecordarDatos" runat="server" />
                &nbsp;<asp:Label ID="lblRecordarDatos" runat="server" Text="-Recordarme"></asp:Label>
            </div>
            <div class="form-footer">
                <asp:Label ID="lblRecuperoPass" runat="server" Text="-Olvidó su contraseña?"></asp:Label>
                &nbsp;<asp:LinkButton ID="lnkRecuperoPass" runat="server" Text="-Recuperar contraseña" OnClick="lnkRecuperoPass_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
