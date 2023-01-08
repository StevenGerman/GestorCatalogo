using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> ListArticulos = new List<Articulo>();
        public void AgregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio,ImagenUrl) values('"+nuevo.Codigo+"', '"+nuevo.Nombre+"','"+nuevo.Descripcion+"', @Marca, @Categoria,@Precio,@Imagen);");
                datos.setearParametro("@Marca",nuevo.Marca.Id);
                datos.setearParametro("@Precio",nuevo.Precio);
                datos.setearParametro("@Categoria",nuevo.Categoria.Id);
                datos.setearParametro("@Imagen",nuevo.UrlImagen);
                datos.ejecutarLectura();
            }
            catch (Exception x)
            {

                throw x;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> ListarArticulos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select A.Id,Codigo,Nombre,A.Descripcion,M.Id as IdM,M.Descripcion Marca,ImagenUrl,Precio,C.Id IdC,C.Descripcion as Categoria from ARTICULOS A inner join MARCAS M on A.IdMarca=M.Id join CATEGORIAS C on A.IdCategoria=C.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if(!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdM"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdC"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    ListArticulos.Add(aux);
                }
                return ListArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


            

        }

        public void ModificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion,IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @Imagen, Precio = @Precio where Id = @Id;");
                datos.setearParametro("@Codigo",articulo.Codigo);
                datos.setearParametro("@Nombre",articulo.Nombre);
                datos.setearParametro("@Descripcion",articulo.Descripcion);
                datos.setearParametro("@IdMarca",articulo.Marca.Id);
                datos.setearParametro("@IdCategoria",articulo.Categoria.Id);
                datos.setearParametro("@Imagen",articulo.UrlImagen);
                datos.setearParametro("@Precio",articulo.Precio);
                datos.setearParametro("@Id",articulo.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
                    
            }
        }

        

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id = @Id;");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> ListArticulosFiltrada = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "Select A.Id,Codigo,Nombre,A.Descripcion,M.Id as IdM,M.Descripcion Marca, ImagenUrl, Precio, C.Id IdC, C.Descripcion as Categoria from ARTICULOS A inner join MARCAS M on A.IdMarca = M.Id join CATEGORIAS C on A.IdCategoria = C.Id and ";
            if(campo == "Nombre")
            {
                switch (criterio)
                {
                    case "Comienza con":
                        consulta += "Nombre like '" + filtro + "%';";
                        break;
                    case "Termina con":
                        consulta += "Nombre like '%" + filtro + "';";
                        break;
                    case "Contiene":
                        consulta += "Nombre like '%" + filtro + "%';";
                        break;
                    default:
                        break;
                }
            }
            else if (campo == "Marca")
            {
                switch (criterio)
                {
                    case "Comienza con":
                        consulta += "M.Descripcion like '" + filtro + "%';";
                        break;
                    case "Termina con":
                        consulta += "M.Descripcion like '%" + filtro + "';";
                        break;
                    case "Contiene":
                        consulta += "M.Descripcion like '%" + filtro + "%';";
                        break;
                    default:
                        break;
                }
            }
            else if (campo == "Precio")
            {
                switch (criterio)
                {
                    case "Mayor a":
                        consulta += "Precio >" + filtro + ";";
                        break;
                    case "Menor a":
                        consulta += "Precio <" + filtro + ";";
                        break;
                    default:
                        break;
                }
            }

            datos.setearConsulta(consulta);
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Id = (int)datos.Lector["Id"];
                aux.Codigo = (string)datos.Lector["Codigo"];
                aux.Nombre = (string)datos.Lector["Nombre"];
                aux.Descripcion = (string)datos.Lector["Descripcion"];
                if (!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                aux.Precio = (decimal)datos.Lector["Precio"];
                aux.Marca = new Marca();
                aux.Marca.Id = (int)datos.Lector["IdM"];
                aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                aux.Categoria = new Categoria();
                aux.Categoria.Id = (int)datos.Lector["IdC"];
                aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                ListArticulosFiltrada.Add(aux);
            }
            return ListArticulosFiltrada;

        }
    }
}
