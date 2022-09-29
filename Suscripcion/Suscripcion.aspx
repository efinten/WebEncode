<%@ Page Title="Suscripcion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suscripcion.aspx.cs" Inherits="WebEncode.Suscripcion.WebForm1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Suscripcion</h1>
    <p class="lead">Para completar la suscripcion complete los siguientes datos .</p>


    <h2>Buscar Suscriptor</h2>

    <div>
        <div class="form-group col-sm-4">
            <asp:Label runat="server">Tipo de Documento</asp:Label>

            <asp:DropDownList runat="server" ID="ddlTipoDni" CssClass="ddl">
                <asp:ListItem Text="--Seleccione una opción" Value="-1"></asp:ListItem>
                <asp:ListItem Text="DNI" Value="1"></asp:ListItem>
                <asp:ListItem Text="PASAPORTE" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="form-group col-sm-4">
            <asp:Label runat="server">Tipo de Documento</asp:Label>
            <asp:TextBox ID="txtNroDni" runat="server" Width="150px" MaxLength="9" CssClass="txtBuscar"></asp:TextBox>

        </div>
        <div class="form-group col-sm-4">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100%" CssClass="btn btn-primary" />
        </div>
    </div>
    <h2>Datos del Subscriptor</h2>
    <div class="text-center">

        <strong style="color: red"><span>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label></span></strong>
        <strong style="color: green"><span>
            <asp:Label ID="lblMensajeExito" runat="server"></asp:Label></span></strong>
    </div>
    <div class="col-lg-12">
        <div class="form-group col-sm-3">
            <asp:Label runat="server">Nombre </asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="txtBuscar"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldNombre" ForeColor="Red" runat="server" ControlToValidate="txtNombre" Display="Static" ErrorMessage="El nombre es requerido" ValidationGroup="validar"></asp:RequiredFieldValidator>

        </div>

        <div class="form-group col-sm-3">
            <asp:Label runat="server">Apellido</asp:Label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="txtBuscar"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorApellido" ForeColor="Red" runat="server" ControlToValidate="txtApellido" Display="Static" ErrorMessage="El apellido es requerido" ValidationGroup="validar"></asp:RequiredFieldValidator>

        </div>
        <div class="form-group col-sm-3">
            <asp:Button ID="btnNuevo"  runat="server" Width="100%" Text="Nuevo" class="btn btn-info" />
        </div>

    </div>
    <div class="col-lg-12">
        <div class="form-group col-sm-3">
            <asp:Label runat="server">Dirección </asp:Label>
            <asp:TextBox ID="txtDir" runat="server" CssClass="txtBuscar"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequRequiredFieldValidatorDir" ForeColor="Red" runat="server" ControlToValidate="txtDir" Display="Static" ErrorMessage="La dirección es requerida" ValidationGroup="validar"></asp:RequiredFieldValidator>

        </div>

        <div class="form-group col-sm-3">
            <asp:Label runat="server">Email</asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="txtBuscar"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" ForeColor="Red" runat="server" ControlToValidate="txtEmail" Display="Static" ErrorMessage="El Email es requerido" ValidationGroup="validar"></asp:RequiredFieldValidator>

        </div>
        <div class="form-group col-sm-3">
            <asp:Button ID="btnModificar" ValidationGroup="validar" runat="server" Width="100%" Text="Modificar" class="btn btn-info" />
        </div>
    </div>
    <div class="col-lg-12">

        <div class="form-group col-sm-3">
            <asp:Label runat="server">Teléfono </asp:Label>
            <asp:TextBox ID="txtTel" runat="server" CssClass="txtBuscar" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTel" ForeColor="Red" runat="server" ControlToValidate="txtTel" Display="Static" ErrorMessage="El teléfono es requerido" ValidationGroup="validar"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group col-sm-3">
            <asp:Label runat="server">Estado</asp:Label>
            <asp:TextBox ID="txtEstado" runat="server" CssClass="txtBuscar" Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group col-sm-3">
            <asp:Button ID="btnGuardar" runat="server" ValidationGroup="validar" Width="100%" Text="Guardar" class="btn btn-success" />
        </div>
    </div>
    <div>
        <div class="form-group col-sm-3">
            <asp:Label runat="server">Nombre de Usuario </asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="txtBuscar"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsuario" ForeColor="Red" runat="server" ControlToValidate="txtUsuario" Display="Static" ErrorMessage="El Usuario es requerido" ValidationGroup="validar"></asp:RequiredFieldValidator>

        </div>

        <div class="form-group col-sm-3">
            <asp:Label runat="server">Contraseña</asp:Label>
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="txtBuscar"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPasswd" ForeColor="Red" runat="server" ControlToValidate="txtPassword" Display="Static" ErrorMessage="La password es requerido" ValidationGroup="validar"></asp:RequiredFieldValidator>


        </div>
        <div class="form-group col-sm-3">
            <asp:Button ID="btnCancelar" runat="server" Width="100%" Text="Cancelar" class="btn btn-warning" />
        </div>
    </div>
    <div class="form-group col-sm-12">
        <asp:Button ID="btnRegistrar" runat="server" Width="100%" ValidationGroup="validar" Text="Registrar suscripcion" class="btn btn-success" />
    </div>
</asp:Content>
