
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
namespace TP08_PreguntadORT.Models {

public class Categoria{
    public int CategoriaID {get; private set;}
    public string Nombre {get; private set;}
}    
}