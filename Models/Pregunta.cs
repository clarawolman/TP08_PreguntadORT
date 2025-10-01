
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace TP08_PreguntadORT.Models
{
    public class Pregunta
    {
        public int PreguntaID { get; set; }
        public string Texto { get; set; }
        public int CategoriaID { get; set; }
        public int DificultadID { get; set; }
        public string ImagenURL { get; set; }
    }
}