using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Livro
    {
        public int Id { get; set; }
        
        [Required]
        public string Titulo { get; set; }
        
        public string Autor { get; set; }
        
        [Required] 
        [Range(0, 5000)]
        [Column("numeroPaginas")]
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