<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RptPatologias.aspx.cs" Inherits="GUI.Reportes.RptPatologias" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <div style="text-align: center;">
        <header>
            <asp:Label ID="lblTitle" runat="server" Text="-Reporte de Patologías" Font-Size="Large"></asp:Label>
        </header>
        <br />
    </div>

    <div style="text-align: center;">
        <asp:Chart ID="ctPatologias" runat="server">
            <Series>
                <asp:Series Name="Series" ChartType="Bar"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>

     <div class="form-row">
        <div class="form-group" style="text-align: right;">
            <button runat="server" id="btnExportarPDF" class="btn btn-primary fa fa-file-pdf-o" onserverclick="btnExportarPDF_ServerClick"></button>
        </div>
    </div>
</asp:Content>