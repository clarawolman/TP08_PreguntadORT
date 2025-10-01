
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace TP08_PreguntadORT.Models
{
    public class Dificultad
    {
        public int DificultadID { get; set; }
        public string Nombre { get; set; }
    }
}