<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginForm.aspx.cs" Inherits="presentacion.pages.loginForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Iniciar Sesión</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .header-bar {
            height: 60px;
            background-color: #cce5ff;
            border-top: 6px solid purple;
        }
        .login-container {
            max-width: 400px;
            margin: 0 auto;
            margin-top: 60px;
            background-color: white;
            padding: 40px;
            border-radius: 0;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }
        .form-label {
            font-size: 0.8rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-bottom: 5px;
        }
        .form-control {
            border-radius: 0;
        }
        .btn-black {
            background-color: black;
            color: white;
            border-radius: 0;
        }
        .btn-black:hover {
            background-color: #333;
        }
        .small-link {
            font-size: 0.9rem;
        }
    </style>
</head>
<body>
    <div class="header-bar"></div>

    <form id="form1" runat="server">
        <div class="login-container text-center">
            <h2 class="fw-bold">Bienvenido!!</h2>
            <p class="small-link">
                Inicia Sesión o <a href="#" class="text-primary text-decoration-none">Crea una nueva Cuenta</a>
            </p>

            <div class="text-start mt-4 mb-3">
                <label for="txtUsuario" class="form-label">Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario" />
            </div>

            <div class="text-start mb-4">
                <label for="txtContrasenia" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña" />
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="btn btn-black w-100 mb-2" OnClick="btnLogin_Click" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mt-3" />
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>