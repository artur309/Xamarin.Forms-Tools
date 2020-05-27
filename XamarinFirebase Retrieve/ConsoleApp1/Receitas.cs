using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Receitas
    {
        public string NomeReceita { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacao { get; set; }
        public string Comentarios { get; set; }
        public string Imagem { get; set; }
        public bool Privacidade { get; set; }
    }
}