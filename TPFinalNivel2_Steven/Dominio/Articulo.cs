﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        [DisplayName("Código de Artículo")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }
        public Marca Marca { get; set; }
        [DisplayName("Caetgoría")]
        public Categoria Categoria { get; set; }





    }
}