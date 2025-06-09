<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrarAnimal.aspx.cs" Inherits="presentacion.pages.registrarAnimal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Animal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Cargar jQuery antes de los validadores unobtrusive -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.validate/1.19.3/jquery.validate.min.js"></script>

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

        <asp:Button ID="btnRegresar" runat="server" Text="← Menú Principal" CssClass="btn-regresar" OnClick="btnRegresar_Click" />

        <h2 class="titulo">Registrar Animal</h2>
        <div class="form-container">
            <div class="form-box">
                <!-- Campo Dueño (obligatorio) -->
                <div class="mb-3">
                    <label for="ddlDueno" class="form-label">Dueño</label>
                    <asp:DropDownList ID="ddlDueno" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione un dueño" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDueno" runat="server"
                        ControlToValidate="ddlDueno"
                        InitialValue=""
                        ErrorMessage="Debe seleccionar un dueño"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="grupoValidacion" />
                </div>

                <!-- Nombre de la mascota (obligatorio) -->
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre de la mascota</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                        ControlToValidate="txtNombre"
                        ErrorMessage="El nombre de la mascota es obligatorio"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="grupoValidacion" />
                </div>

                <!-- Especie (obligatoria) -->
                <div class="mb-3">
                    <label for="txtEspecie" class="form-label">Especie</label>
                    <asp:TextBox ID="txtEspecie" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvEspecie" runat="server"
                        ControlToValidate="txtEspecie"
                        ErrorMessage="La especie es obligatoria"
                        CssClass="text-danger"
                        Display="Dynamic"
                        ValidationGroup="grupoValidacion" />
                </div>

                <!-- Raza (opcional) -->
                <div class="mb-3">
                    <label for="txtRaza" class="form-label">Raza</label>
                    <asp:TextBox ID="txtRaza" runat="server" CssClass="form-control" />
                </div>

                <!-- Fecha de nacimiento -->
                <div class="mb-3">
                    <label for="txtFechaNacimiento" class="form-label">Fecha de nacimiento</label>
                    <asp:TextBox ID="txtFechaNacimiento" runat="server"
                        CssClass="form-control" TextMode="Date" />
                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3" />

                <asp:Button ID="btnGuardar" runat="server"
                    Text="Guardar Animal"
                    CssClass="btn btn-guardar"
                    OnClick="btnGuardar_Click"
                    ValidationGroup="grupoValidacion" />
            </div>

            <div>
                <img src="resources/cat.jpg" alt="Cat" class="imagen-perro" />
            </div>
        </div>
    </form>
</body>
</html>