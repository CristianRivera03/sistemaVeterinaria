<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="presentacion.pages.inicio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .header {
            background-color: #cce5ff;
            height: 60px;
            display: flex;
            justify-content: flex-end;
            align-items: center;
            padding: 0 20px;
            border-top: 4px solid #000;
        }

        .header i {
            font-size: 24px;
        }

        .main-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 40px 80px;
        }

        .btn-menu {
            width: 220px;
            margin-bottom: 20px;
            border: 2px solid black;
            border-radius: 0;
            font-weight: 600;
            background-color: white;
        }

        .btn-menu:hover {
            background-color: #8cc04e;
        }

        .img-perro {
            max-width: 350px;
            border-radius: 10px;
        }


        @media (max-width: 768px) {
            .main-container {
                flex-direction: column;
                padding: 20px;
            }

            .img-perro {
                margin-top: 30px;
            }
        }
    </style>
    <script src="https://kit.fontawesome.com/a2c5c4e6e6.js" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <i class="fas fa-user-circle"></i>
        </div>

        <div class="main-container">
            <div class="d-flex flex-column">
                <asp:Button ID="btnDueno" runat="server" Text="Registrar Dueños" CssClass="btn btn-menu" />
                <asp:Button ID="btnAnimal" runat="server" Text="Registrar Animal" CssClass="btn btn-menu" />
                <asp:Button ID="btnConsulta" runat="server" Text="Registrar Consulta" CssClass="btn btn-menu" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar Consultas" CssClass="btn btn-menu" />
                <asp:Button ID="btnReportes" runat="server" Text="Ver Reportes" CssClass="btn btn-menu" />
            </div>

            <div>
                <img src="resources/rataGod.jpg" alt="perrito feliz" class="img-perro" />
            </div>
        </div>
    </form>
</body>
</html>
