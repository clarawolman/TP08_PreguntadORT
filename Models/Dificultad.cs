
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;


public static List<Dificultad> ObtenerDificultades(int dificultad, int categoria)
{
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = @"
                SELECT * FROM Dificultad
                WHERE (@Dificultad = -1 OR DificultadID = @Dificultad)
                  AND (@Categoria = -1 OR CategoriaID = @Categoria)";
        return db.Query<Dificultad>(sql, new { Dificultad = dificultad, Categoria = categoria }).AsList();
    }
}