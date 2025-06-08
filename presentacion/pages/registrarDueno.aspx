<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrarDueno.aspx.cs" Inherits="presentacion.pages.registrarDueno" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Dueño</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

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

        .form-control {
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

        <asp:Button ID="btnRegresar" runat="server" Text="← Menú Principal" CssClass="btn-regresar" OnClick="btnRegresar_Click" />

        <h2 class="titulo">Registrar Dueño</h2>
        <div class="form-container">
            <div class="form-box">
                <!-- Campo Nombre -->
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                        ControlToValidate="txtNombre"
                        ErrorMessage="El nombre es obligatorio"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="grupoValidacion" />
                </div>

                <!-- Campo Teléfono -->
                <div class="mb-3">
                    <label for="txtTelefono" class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    <asp:RegularExpressionValidator ID="revTelefono" runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="El teléfono debe contener exactamente 8 dígitos"
                        ValidationExpression="^\d{8}$"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="grupoValidacion" />
                </div>

                <!-- Campo Email -->
                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Ingrese un correo electrónico válido"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="grupoValidacion" />
                </div>

                <!-- Campo Dirección -->
                <div class="mb-3">
                    <label for="txtDireccion" class="form-label">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server"
                                 CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3" />

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Dueño"
                    CssClass="btn btn-guardar" OnClick="btnGuardar_Click"
                    ValidationGroup="grupoValidacion" />
            </div>

            <div>
                <img src="resources/pet.jpg" alt="Perrito" class="imagen-perro" />
            </div>
        </div>
    </form>
</body>
</html>