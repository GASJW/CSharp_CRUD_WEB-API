using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Disco
    {
        public int Id { get; set; }
        [Required]
        public string NomeDisco {get; set; }
        public string NomeAutor { get; set; }
        [Required]
        public int NumeroDeMusicas { get; set; }
        [Required]
        public DateTime DataLancamento { get; set; }

        public Disco()
        {

        }

        public void Copiar(Models.Disco discoASerCopiado)
        {
            this.NomeDisco = discoASerCopiado.NomeDisco;
            this.NomeAutor = discoASerCopiado.NomeAutor;
            this.NumeroDeMusicas = discoASerCopiado.NumeroDeMusicas;
            this.DataLancamento = discoASerCopiado.DataLancamento;
        }
    }
}
