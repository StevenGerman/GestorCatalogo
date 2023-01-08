using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using negocio;
namespace Vistas
{
    public partial class FrmAgregarArticulo : Form
    {
        private Articulo articulo;
        private ArticuloNegocio articuloNegocio = new ArticuloNegocio();
        public FrmAgregarArticulo()
        {
            InitializeComponent();
        }
        public FrmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            lblTitulo.Text = "Modificar Artículo";
        }
        //Eventos de interaccion con la ventana.
        private void panelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            MoverFormulario();
        }

        private void FrmAgregarArticulo_MouseDown(object sender, MouseEventArgs e)
        {
            MoverFormulario();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void MoverFormulario()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }


        
        
        private void ptbCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ptbMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void CargarImagen(string imagen)
        {
            try
            {
                ptbImagen.Load(imagen);
            }
            catch (Exception)
            {

                ptbImagen.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
        }
        
        private void txtbUrlImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtbUrlImagen.Text);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Articulo nuevo = new Articulo();
            if(ValidarRequeridos())
            {
                try
                {
                    if (articulo == null)
                        articulo = new Articulo();
                    articulo.Codigo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescrip.Text;
                    articulo.Precio = decimal.Parse(txtPrecio.Text, CultureInfo.GetCultureInfo("en-US"));
                    articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                    articulo.Marca = (Marca)cbxMarcas.SelectedItem;
                    articulo.UrlImagen = txtbUrlImagen.Text;

                    if (articulo.Id != 0)
                    {
                        articuloNegocio.ModificarArticulo(articulo);
                        MessageBox.Show("Se ha modificado el artículo correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        articuloNegocio.AgregarArticulo(articulo);
                        MessageBox.Show("Se ha añadido el artículo correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.Close();
                }
                catch (Exception h)
                {
                    MessageBox.Show(h.ToString());
                }
            }
            
            
        }

        private void FrmAgregarArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cbxMarcas.DataSource = marcaNegocio.ListarMarcas();
                cbxMarcas.DisplayMember = "Descripcion";
                cbxMarcas.ValueMember = "Id";
                cbxCategoria.DataSource = categoriaNegocio.Listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                if(articulo != null)
                {
                    txtNombre.Text = articulo.Nombre;
                    txtCodigo.Text = articulo.Codigo;
                    txtDescrip.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString("F2",CultureInfo.GetCultureInfo("en-US"));
                    txtbUrlImagen.Text = articulo.UrlImagen;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                    cbxMarcas.SelectedValue = articulo.Marca.Id;
                    CargarImagen(articulo.UrlImagen);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        

        

        private bool ValidarRequeridos()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDescrip.Text) || string.IsNullOrEmpty(txtbUrlImagen.Text))
            {
                MessageBox.Show("Por favor complete todos los campos de texto..");
                return false;
            }
            if (cbxCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar la Categoria del Articulo para guardarlo..");
                return false;
            }
            if (cbxMarcas.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar la Merca del Articulo para guardarlo..");
                return false;
            }
            if (txtPrecio.Text.ToString() == "")
            {
                MessageBox.Show("El Articulo debe tener un precio para poder guardarlo...");
                return false;

            }
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El Precio debe ser un número. Por Ejemplo:100.00");
                return false;
            }
            return true;
        }

        private bool EsNumero(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }


    }
}
