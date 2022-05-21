using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cargo { get; set; }
        public int Idade { get; set; }

        public Funcionario()
        {

        }
        public void Copiar(Models.Funcionario funcionarioASerCopiado)
        {
            /*this.Id = livroASerCopiado.Id;*/
            this.Nome = funcionarioASerCopiado.Nome;
            this.Sobrenome = funcionarioASerCopiado.Sobrenome;
            this.Cargo = funcionarioASerCopiado.Cargo;
            this.Idade = funcionarioASerCopiado.Idade;
        }

    }
}