
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;


public static List<Categoria> ObtenerCategorias()
{
    using (SqlConnection db = new SqlConnection(_connectionString))
    { //esto no lo capto
        string sql = @" 
                SELECT * FROM Categoria
                WHERE (@Dificultad = -1 OR DificultadID = @Dificultad)
                  AND (@Categoria = -1 OR CategoriaID = @Categoria)";
        return db.Query<Categoria>(sql, new { Dificultad = dificultad, Categoria = categoria }).AsList();
    }
}