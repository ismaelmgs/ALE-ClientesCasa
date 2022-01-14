<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" EnableEventValidation="false" Inherits="ClientesCasa.login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> Aerolíneas Ejecutivas</title>

    <asp:PlaceHolder runat="server">
        <%-- <%: Scripts.Render("~/bundles/modernizr") %>--%>
    </asp:PlaceHolder>
    <%--<webopt:bundlereference runat="server" path="~/Content/css" />--%>
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/bootstrap.min.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/bootstrap-responsive.min.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/fullcalendar.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/matrix-style.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/matrix-media.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/matrix-login.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/jquery.gritter.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/colorpicker.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/datepicker.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/uniform.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/select2.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/Content/bootstrap-wysihtml5.css") %>" />
    <link rel="stylesheet" href="<%=ResolveClientUrl("~/font-awesome/css/font-awesome.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("http://fonts.googleapis.com/css?family=Open+Sans:400,700,800") %>" />
    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("http://fonts.googleapis.com/css?family=Roboto:400,700,300|Material+Icons") %>" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <style type="text/css">
        .fijo {
            position: fixed !important;
            right: 0px;
            top: 0px;
            z-index: 10 !important
        }
    </style>
    <script>
        $('#myModal').on('shown.bs.modal', function () {
              $('#myInput').trigger('focus')
          })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="upaPrincipal" runat="server">
            <ContentTemplate>
                <div>
                    <div id="loginbox">
                        <form id="loginform" class="form-vertical" action="index.html">
                            <div class="control-group normal_text">
                                <h3>
                                    <img src="Images/LOGO_N.png" /></h3>
                            </div>
                            <div class="control-group" style="margin-left: -30px;">
                                <div class="controls">
                                    <div class="section group">
                                        <div class="main_input_box">
                                            <div class="col span_1_of_5" style="background-color: #28b779;border-radius: 10px 0px 0px 10px;">
                                                <span class="add-on bg_lg" style="text-align: center;"><i class="icon-user" style="margin-right: 20%;"></i></span>
                                            </div>
                                            <div class="col span_4_of_5">
                                                <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" MaxLength="40" Width="100%" CssClass="input_2" style="border-radius:0px 10px 10px 0px;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="section group">
                                        <div class="main_input_box">
                                            <div class="col span_1_of_5" style="background-color: #ffb848;border-radius: 10px 0px 0px 10px;">
                                                <span class="add-on bg_ly" style="text-align: center;"><i class="icon-lock" style="margin-right: 20%;"></i></span>
                                            </div>
                                            <div class="col span_4_of_5">
                                                <asp:TextBox ID="txtPassword" runat="server" placerholder="Password" TextMode="Password" MaxLength="40" CssClass="input_2" Width="100%" style="border-radius:0px 10px 10px 0px;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions"  style="text-align: center !important;">
                                <span class="pull-center">
                                    <!-- Button trigger modal -->
                                    &nbsp;&nbsp;&nbsp;
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" style="width: 240px !important;">
                                        ¿Olvidó su contraseña?
                                    </button>
                                </span>
                                <span class="pull-center">
                                    <asp:Button ID="btnIngregar" runat="server" Text="Acceder" OnClick="btnIngregar_Click" CssClass="btn btn-success" />
                                </span>
                            </div>
                        </form>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #223140; color:#ffffff;">
                        <h4 class="modal-title" id="exampleModalLabel">¿Olvidó su contraseña?</h4>
                    </div>
                    <div class="modal-body" style="background-color: #0b1a29; color:#ffffff;">
                        <form id="recoverform" action="#" class="form-vertical">
                            <p class="normal_text">
                                <h5>Ingrese su dirección de correo electrónico a continuación y le enviaremos instrucciones sobre cómo recuperar una contraseña.</h5>
                            </p>
                            <div class="controls">
                                <div class="main_input_box" style="text-align: center;">
                                    <input type="text" placeholder="Correo electrónico" style="border-radius: 10px;" />
                                    <button type="button" class="btn btn-success" style="margin-top: 0px;">
                                        Recuperar
                                    </button>
                                </div>

                            </div>
                        </form>
                    </div>
                    <div class="modal-footer" style="background-color: #223140;">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="<%=ResolveClientUrl("~/Scripts/excanvas.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.ui.custom.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/bootstrap.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.flot.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.flot.resize.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.peity.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/fullcalendar.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.dashboard.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.gritter.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.interface.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.chat.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.login.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.validate.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.form_validation.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.wizard.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.uniform.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/select2.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.popover.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.dataTables.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.tables.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/bootstrap-colorpicker.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/bootstrap-datepicker.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.toggle.buttons.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/masked.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/matrix.form_common.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/wysihtml5-0.3.0.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/jquery.peity.min.js") %>"></script>
    <script src="<%=ResolveClientUrl("~/Scripts/bootstrap-wysihtml5.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("https://maps.googleapis.com/maps/api/js?key=YOUR_KEY_HERE") %>"></script>
</body>
</html>
