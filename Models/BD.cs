using Dapper;
using Microsoft.Data.SqlClient;

public static class BD
{
    private static string _connectionString = @"Server=localhost;Database=PreguntadORT;Integrated Security=True;TrustServerCertificate=True;";

    public static List<Categoria> ObtenerCategorias()
    {
        List<Categoria> categorias = new List<Categoria>();
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
    public static List<Pregunta> ObtenerPreguntas(int dificultad, int categoria) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Pregunta WHERE Pregunta.IdDificultad = @dificultad and Pregunta.IdCategoria = @categoria";
            return db.Query<Pregunta>(sql).AsList();
        }
    }
}

