using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace negocio
{
    public class CategoriaNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        

        public List<Categoria> Listar()
        {
            List<Categoria> listaCategoria = new List<Categoria>();

            try
            {
                datos.setearConsulta("select Id,Descripcion from CATEGORIAS;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    listaCategoria.Add(aux);
                }

                return listaCategoria;
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


    }
}
