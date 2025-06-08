<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrarConsulta.aspx.cs" Inherits="presentacion.pages.registrarConsulta" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Consulta</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

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

        .form-container {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            gap: 40px;
        }

        .form-box {
            flex: 1;
        }

        .form-control, .form-select {
            border: 2px solid black;
            border-radius: 0;
        }

        .btn-guardar {
            background-color: black;
            color: white;
            font-weight: bold;
            padding: 10px 20px;
            border: none;
        }

        .btn-guardar:hover {
            background-color: #333;
        }

        .imagen-perro {
            max-width: 300px;
            border-radius: 10px;
        }

        @media (max-width: 768px) {
            .form-container {
                flex-direction: column;
                align-items: center;
            }
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
        window.addEventListener('load', function () {
            document.body.classList.add('page-loaded');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Button ID="btnRegresar" runat="server"
                    Text="← Menú Principal"
                    CssClass="btn-regresar"
                    OnClick="btnRegresar_Click" />

        <h2 class="titulo">Registrar Consulta</h2>
        <div class="form-container">
            <div class="form-box">
                <!-- 1) Selector de Dueño -->
                <div class="mb-3">
                    <label for="ddlDueno" class="form-label">Dueño</label>
                    <asp:DropDownList ID="ddlDueno" runat="server"
                                      CssClass="form-select" />
                </div>

                <!-- 2) Selector de Mascota -->
                <div class="mb-3">
                    <label for="ddlMascota" class="form-label">Mascota</label>
                    <asp:DropDownList ID="ddlMascota" runat="server"
                                      CssClass="form-select" />
                </div>

                <!-- 3) Fecha y hora -->
                <div class="mb-3">
                    <label for="txtFechaHora" class="form-label">Fecha y hora</label>
                    <asp:TextBox ID="txtFechaHora" runat="server"
                                 CssClass="form-control"
                                 TextMode="DateTimeLocal" />
                </div>

                <!-- 4) Descripción -->
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server"
                                 CssClass="form-control"
                                 TextMode="MultiLine" Rows="3" />
                </div>

                <!-- 5) Diagnóstico -->
                <div class="mb-3">
                    <label for="txtDiagnostico" class="form-label">Diagnóstico</label>
                    <asp:TextBox ID="txtDiagnostico" runat="server"
                                 CssClass="form-control"
                                 TextMode="MultiLine" Rows="3" />
                </div>

                <!-- 6) Tratamiento -->
                <div class="mb-3">
                    <label for="txtTratamiento" class="form-label">Tratamiento</label>
                    <asp:TextBox ID="txtTratamiento" runat="server"
                                 CssClass="form-control"
                                 TextMode="MultiLine" Rows="3" />
                </div>

                <!-- 7) Veterinario -->
                <div class="mb-3">
                    <label for="txtVeterinario" class="form-label">Veterinario</label>
                    <asp:TextBox ID="txtVeterinario" runat="server"
                                 CssClass="form-control" />
                </div>

                <asp:Label ID="lblMsg" runat="server"
                           CssClass="text-danger mb-3" />

                <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar Consulta"
                            CssClass="btn btn-guardar"
                            OnClick="btnGuardar_Click" />
            </div>

            <div>
                <img src="resources/cat.jpg" alt="Mascota" class="imagen-perro" />
            </div>
        </div>


        <!-- Hidden field para pasar el Id recién creado -->
    <asp:HiddenField ID="hfConsultaId" runat="server" />

    <!-- Modal de confirmación -->
    <div class="modal fade" id="confirmModal" tabindex="-1" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content p-4 rounded">
          <div class="modal-body text-center">
            <h5>¿Desea generar el recibo?</h5>
          </div>
          <div class="modal-footer justify-content-center border-0">
            <asp:Button ID="btnGeneratePdf" runat="server"
              CssClass="btn" Style="background-color:#28a745;color:white;"
              Text="Sí" OnClick="btnGeneratePdf_Click" />
            <asp:Button ID="btnBackInicio" runat="server"
              CssClass="btn btn-primary" Text="Volver a Inicio"
              OnClick="btnBackInicio_Click" />
          </div>
        </div>
      </div>
    </div>


    </form>
</body>
</html>
