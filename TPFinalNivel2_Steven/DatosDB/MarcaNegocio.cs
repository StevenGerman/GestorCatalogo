using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace negocio
{
    public class MarcaNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Marca> ListarMarcas()
        {
            List<Marca> listaMarcas = new List<Marca>();
            try
            {
                datos.setearConsulta("select Id,Descripcion from MARCAS;");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Marca aux = new Marca();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];


                    listaMarcas.Add(aux);
                }
                return listaMarcas;
                
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
