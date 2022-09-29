using System;
using System.Data;
using System.Data.SqlClient;
using WebEncode.App_Code;

namespace WebEncode.Suscripcion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static SqlConnection connection()
        {

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["encode"].ConnectionString);

            return conn;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            btnBuscar.Click += BtnBuscar_Click;
            btnRegistrar.Click += BtnRegistrar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnModificar.Click += BtnModificar_Click;
            btnNuevo.Click += BtnNuevo_Click;
            if (!IsPostBack)
            {
                btnModificar.Visible = false;
                btnNuevo.Visible = false;
                btnGuardar.Visible = false;
                btnRegistrar.Visible = false;


            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDir.Text = "";
            txtTel.Text = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            ddlTipoDni.SelectedValue = "-1";
            txtNroDni.Text = "";
            txtUsuario.ReadOnly = false;
            txtNroDni.Enabled = true;
            ddlTipoDni.Enabled = true;
            btnModificar.Visible = false;
            txtPassword.Text = "";
            txtPassword.Attributes.Add("value", txtPassword.Text);
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope();
                using (trans)
                {
                    string nombre = txtNombre.Text;
                    string apellido = txtApellido.Text;
                    string dni = txtNroDni.Text;
                    string tipodoc = ddlTipoDni.SelectedValue;
                    string direccion = txtDir.Text;
                    string telefono = txtTel.Text;
                    string email = txtEmail.Text;
                    string nombreusuario = txtUsuario.Text;
                    string password = MetodosComunes.Encriptar(txtPassword.Text);

                    int Id = 0;
                    SqlConnection cn = connection();
                    cn.Open();
                    //OBTENGO EL IDSUSCRIPTOR PARA ACTUALIZAR EL REGISTRO
                    string sqlTraerIdSuscriptor = "select sr.IdSuscriptor from encode.dbo.Suscriptor sr join encode.dbo.Suscripcion sn  on sr.IdSuscriptor = sn.IdSuscriptor where NumeroDocumento = @dni and FechaFin is null";

                    SqlCommand cmdTraerIdSuscriptor = new SqlCommand(sqlTraerIdSuscriptor, cn);

                    cmdTraerIdSuscriptor.Parameters.AddWithValue("@dni", dni);

                    SqlDataAdapter adapterTraerIdSuscriptor = new SqlDataAdapter(cmdTraerIdSuscriptor);

                    DataSet dsIdSuscriptor = new DataSet();
                    adapterTraerIdSuscriptor.Fill(dsIdSuscriptor, "IdSuscriptor");

                    if (dsIdSuscriptor.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = dsIdSuscriptor.Tables[0];

                        Id = Convert.ToInt32(dt.Rows[0]["IDSUSCRIPTOR"].ToString());


                    }


                    //UPDATE DE EL SUSCRIPTOR
                    string sqlupdate = "UPDATE encode.dbo.Suscriptor SET NOMBRE = @nombre, APELLIDO = @apellido, NUMERODOCUMENTO = @dni, TIPODOCUMENTO = @tipodoc, DIRECCION = @direccion, TELEFONO = @telefono, EMAIL = @email, NOMBREUSUARIO = @nombreusuario, PASSWORD = @password  where IDSUSCRIPTOR = @Id ";

                    SqlCommand cmd = new SqlCommand(sqlupdate, cn);

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@tipodoc", tipodoc);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@nombreusuario", nombreusuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();

                    lblMensajeExito.Text = "Registro modificado éxitosamente";
                    lblMensajeExito.Visible = true;
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDir.Text = "";
                    txtTel.Text = "";
                    txtEmail.Text = "";
                    txtUsuario.Text = "";
                    txtNroDni.Enabled = true;
                    ddlTipoDni.Enabled = true;
                    txtPassword.Text = "";
                    txtPassword.Attributes.Add("value", txtPassword.Text);
                    btnModificar.Visible = false;
                    btnNuevo.Visible = false;


                    connection().Close();
                    trans.Complete();

                }

            }
            catch (Exception _Error)
            {

                lblMensaje.Text = _Error.Message;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDir.Text = "";
            txtTel.Text = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            ddlTipoDni.SelectedValue = "-1";
            txtNroDni.Text = "";
            txtNroDni.Enabled = true;
            ddlTipoDni.Enabled = true;
            btnModificar.Visible = false;
            lblMensaje.Text = "";
            lblMensajeExito.Text = "";
            txtPassword.Text = "";
            txtPassword.Attributes.Add("value", txtPassword.Text);
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope();
                using (trans)
                {
                    string nombre = txtNombre.Text;
                    string apellido = txtApellido.Text;
                    string dni = txtNroDni.Text;
                    string tipodoc = ddlTipoDni.SelectedValue;
                    string direccion = txtDir.Text;
                    string telefono = txtTel.Text;
                    string email = txtEmail.Text;
                    string nombreusuario = txtUsuario.Text;
                    string password = MetodosComunes.Encriptar(txtPassword.Text);

                    SqlConnection cn = connection();
                    cn.Open();
                    //CONTROLO QUE NO EXISTA UNA SUSCRIPCION VIGENTE
                    string sqlVerificoSuscripcion = "select count(*) cantidad from encode.dbo.Suscriptor sr join encode.dbo.Suscripcion sn  on sr.IdSuscriptor = sn.IdSuscriptor where   NumeroDocumento = @dni and FechaFin is null";

                    SqlCommand cmdVerificoSuscripcion = new SqlCommand(sqlVerificoSuscripcion, cn);

                    cmdVerificoSuscripcion.Parameters.AddWithValue("@dni", dni);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdVerificoSuscripcion);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "VerificoSuscripcion");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        if (Convert.ToInt32(dt.Rows[0]["CANTIDAD"]) > 0)
                        {
                            lblMensaje.Visible = true;
                            lblMensaje.Text = "Ya existe una suscripción vigente para el dni consultado";
                            lblMensajeExito.Text = "";
                            lblMensajeExito.Visible = false;
                            connection().Close();
                            trans.Complete();
                            return;
                        }

                    }

                    //CONTROLO EL USUARIO NO SE REPITA
                    string sqlExisteUsuario = "SELECT count(*) cantidad FROM encode.dbo.Suscriptor  where  NombreUsuario = @usuario";

                    SqlCommand cmdExisteUsuario = new SqlCommand(sqlExisteUsuario, cn);

                    cmdExisteUsuario.Parameters.AddWithValue("@usuario", nombreusuario);

                    SqlDataAdapter adapterExisteUsuario = new SqlDataAdapter(cmdExisteUsuario);

                    DataSet dsExisteUsuario = new DataSet();
                    adapterExisteUsuario.Fill(dsExisteUsuario, "VerificoSiExiteUsuario");

                    if (dsExisteUsuario.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = dsExisteUsuario.Tables[0];

                        if (Convert.ToInt32(dt.Rows[0]["CANTIDAD"]) > 0)
                        {
                            lblMensaje.Visible = true;
                            lblMensaje.Text = "El usuario ya existe";
                            lblMensajeExito.Text = "";
                            lblMensajeExito.Visible = false;
                            connection().Close();
                            trans.Complete();
                            return;
                        }

                    }
                    //INSERTO EL SUSCRIPTOR
                    string sql = "INSERT INTO  encode.dbo.Suscriptor (NOMBRE, APELLIDO, NUMERODOCUMENTO, TIPODOCUMENTO, DIRECCION, TELEFONO, EMAIL, NOMBREUSUARIO, PASSWORD )VALUES (@nombre, @apellido, @dni, @tipodoc, @direccion, @telefono, @email, @nombreusuario, @password) ";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@tipodoc", tipodoc);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@nombreusuario", nombreusuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT @@IDENTITY";
                    int clave = int.Parse(cmd.ExecuteScalar().ToString());

                    //INSERTO LA SUSCRIPCION 
                    string sqlSuscripcion = "INSERT INTO  encode.dbo.Suscripcion (IDSUSCRIPTOR,  FECHAALTA) VALUES (@IdSuscriptor, @fecha) ";

                    SqlCommand cmdSuscripcion = new SqlCommand(sqlSuscripcion, cn);

                    cmdSuscripcion.Parameters.AddWithValue("@IdSuscriptor", clave);
                    cmdSuscripcion.Parameters.AddWithValue("@fecha", System.DateTime.Now);

                    cmdSuscripcion.ExecuteNonQuery();

                    //TRAIGO EL NOMBRE DE USUARIO Y PASSWORD PARA INFORMAR POR PANTALLA
                    string sqlTraigoDatosUsuarioyPassword = "select NombreUsuario, Password from encode.dbo.Suscriptor  where IdSuscriptor = @Id";

                    SqlCommand cmdTraigoDatosUsuarioyPassword = new SqlCommand(sqlTraigoDatosUsuarioyPassword, cn);

                    cmdTraigoDatosUsuarioyPassword.Parameters.AddWithValue("@Id", clave);

                    SqlDataAdapter adapterTraigoDatosUsuarioyPassword = new SqlDataAdapter(cmdTraigoDatosUsuarioyPassword);

                    DataSet dsDatos = new DataSet();
                    adapterTraigoDatosUsuarioyPassword.Fill(dsDatos, "DatosUsuario");

                    if (dsDatos.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt = dsDatos.Tables[0];

                        lblMensaje.Text = "";
                        lblMensaje.Visible = false;
                        lblMensajeExito.Visible = true;
                        lblMensajeExito.Text = "Regsitro generado éxitosamente. Se generó el usuario: " + dt.Rows[0]["NombreUsuario"].ToString() + " Password: " + MetodosComunes.Desencriptar(dt.Rows[0]["Password"].ToString());
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                        txtDir.Text = "";
                        txtTel.Text = "";
                        txtEmail.Text = "";
                        txtUsuario.Text = "";


                    }

                    connection().Close();
                    trans.Complete();
                }

            }
            catch (Exception _Error)
            {

                lblMensaje.Text = _Error.Message;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtNroDni.Text;

            try
            {
                if (dni != "" && ddlTipoDni.SelectedValue != "-1")
                {
                    string sql = "select * from encode.dbo.Suscriptor sr join encode.dbo.Suscripcion sn  on sr.IdSuscriptor = sn.IdSuscriptor where NumeroDocumento = @dni and FechaFin is null ";
                    SqlConnection cn = connection();
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@dni", dni);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Datos");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        txtNombre.Text = dt.Rows[0]["NOMBRE"].ToString();
                        txtApellido.Text = dt.Rows[0]["APELLIDO"].ToString();
                        txtDir.Text = dt.Rows[0]["DIRECCION"].ToString();
                        txtEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                        txtTel.Text = dt.Rows[0]["TELEFONO"].ToString();
                        txtUsuario.ReadOnly = true;
                        txtUsuario.Text = dt.Rows[0]["NOMBREUSUARIO"].ToString();
                        txtPassword.Text = MetodosComunes.Desencriptar(dt.Rows[0]["PASSWORD"].ToString());
                        txtPassword.Attributes.Add("value", txtPassword.Text);
                        txtEstado.Text = "Suscripto";
                        txtNroDni.Enabled = false;
                        ddlTipoDni.Enabled = false;
                        btnModificar.Visible = true;
                        lblMensajeExito.Visible = false;
                        btnRegistrar.Visible = false;
                        lblMensajeExito.Text = "";
                        btnNuevo.Visible = true;

                    }
                    else
                    {
                        txtEstado.Text = "No suscripto";
                        lblMensaje.Text = "No existen resultados para el dni ingresado.";
                        lblMensajeExito.Visible = false;
                        lblMensajeExito.Text = "";
                        lblMensaje.Visible = true;
                        btnModificar.Visible = false;
                        btnNuevo.Visible = false;
                        btnGuardar.Visible = false;
                        btnRegistrar.Visible = true;
                        txtUsuario.ReadOnly = false;
                        txtNroDni.Enabled = true;
                        ddlTipoDni.Enabled = true;
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                        txtDir.Text = "";
                        txtTel.Text = "";
                        txtEmail.Text = "";
                        txtUsuario.Text = "";
                        txtPassword.Text = "";
                        btnNuevo.Visible = true;
                        
                    }


                    cn.Close();

                }
                else
                {
                    MetodosComunes.MostrarMensaje(Page, "seleccione un tipo de documento y/o Ingrese un dni");

                }

            }
            catch (Exception _Error)
            {
                lblMensaje.Text = _Error.Message;
            }

        }


    }
}