using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_bib.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int NumeroDePaginas { get; set; }

        public Livro()
        {

        }

        public void Copiar(Models.Livro livroASerCopiado)
        {
            /*this.Id = livroASerCopiado.Id;*/
            this.Titulo = livroASerCopiado.Titulo;
            this.Autor = livroASerCopiado.Autor;
            this.NumeroDePaginas = livroASerCopiado.NumeroDePaginas;
        }
    }
}