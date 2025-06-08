<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporte.aspx.cs" Inherits="presentacion.pages.reporte" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Consultas</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap CSS + JS + jQuery -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
          rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        body {
            padding: 40px;
            font-family: 'Segoe UI', sans-serif;
            opacity: 0;
            transition: opacity 0.5s ease-in;
        }
        body.page-loaded {
            opacity: 1;
        }

        .titulo {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 30px;
        }

        .form-label {
            font-weight: 500;
        }

        .row.mb-3 > .col-md-3,
        .row.mb-3 > .col-md-3,
        .row.mb-3 > .col-md-3,
        .row.mb-3 > .col-md-3 {
            margin-bottom: 1rem;
        }

        .form-control, .form-select {
            border: 2px solid black;
            border-radius: 0;
        }

        .btn-primary, .btn-success, .btn-danger {
            border-radius: 0;
            font-weight: bold;
        }

        .btn-regresar {
            background-color: transparent;
            border: 2px solid black;
            color: black;
            font-weight: bold;
            padding: 8px 16px;
            margin-bottom: 20px;
            transition: background-color 0.3s, color 0.3s;
        }
        .btn-regresar:hover {
            background-color: black;
            color: white;
        }
    </style>

    <script>
        // animación de carga idéntica a registrarConsulta.aspx
        window.addEventListener('load', function () {
            document.body.classList.add('page-loaded');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="p-4">
        
        <!-- Botón de regreso -->
        <asp:Button ID="btnRegresar" runat="server"
            Text="← Menú Principal"
            CssClass="btn-regresar"
            OnClick="btnRegresar_Click" />

        <!-- Título -->
        <h2 class="titulo">Reporte de Consultas</h2>

        <!-- Filtros y acciones -->
        <div class="row mb-3">
            <div class="col-md-3">
                <label for="txtDesde" class="form-label">Desde:</label>
                <asp:TextBox ID="txtDesde" runat="server"
                    CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <label for="txtHasta" class="form-label">Hasta:</label>
                <asp:TextBox ID="txtHasta" runat="server"
                    CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3 align-self-end">
                <asp:Button ID="btnBuscar" runat="server"
                    Text="Buscar" CssClass="btn btn-primary"
                    OnClick="btnBuscar_Click" />
            </div>
            <div class="col-md-3 align-self-end text-end">
                <asp:Button ID="btnExportExcel" runat="server"
                    Text="Exportar a Excel" CssClass="btn btn-success me-2"
                    OnClick="btnExportExcel_Click" />
                <asp:Button ID="btnExportPdf" runat="server"
                    Text="Exportar a PDF" CssClass="btn btn-danger"
                    OnClick="btnExportPdf_Click" />
            </div>
        </div>

        <!-- Resultado -->
        <asp:GridView ID="gvReporte" runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="IdConsulta"    HeaderText="ID" />
                <asp:BoundField DataField="Dueño"          HeaderText="Dueño" />
                <asp:BoundField DataField="Mascota"        HeaderText="Mascota" />
                <asp:BoundField DataField="FechaHora"
                    HeaderText="Fecha y hora"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Descripcion"    HeaderText="Descripción" />
                <asp:BoundField DataField="Diagnostico"    HeaderText="Diagnóstico" />
                <asp:BoundField DataField="Tratamiento"    HeaderText="Tratamiento" />
                <asp:BoundField DataField="Veterinario"    HeaderText="Veterinario" />
            </Columns>
        </asp:GridView>

    </form>
</body>
</html>
