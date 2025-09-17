public static class BD
{
    private static string _connectionString = @"Server=localhost;Database=PreguntadORT;Trusted_Connection=True;";

    public static List<Categoria> ObtenerCategorias()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Categoria";
            return db.Query<Categoria>(sql).AsList();
        }
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Dificultad";
            return db.Query<Dificultad>(sql).AsList();
        }
    }

    public static List<Respuesta> ObtenerRespuestas(int idPregunta)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Respuesta WHERE PreguntaID = @IdPregunta";
            return db.Query<Respuesta>(sql, new { IdPregunta = idPregunta }).AsList();
        }
    }
}

