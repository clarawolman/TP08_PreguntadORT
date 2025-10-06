using Dapper;
using Microsoft.Data.SqlClient;
namespace TP08_PreguntadORT.Models{
    
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
            string sql = "SELECT * FROM Pregunta WHERE Pregunta.DificultadID = @dificultad and Pregunta.CategoriaID = @categoria";
            return db.Query<Pregunta>(sql, new { dificultad = dificultad, categoria = categoria }).AsList();
        }
    }
    public static Pregunta ObtenerProximaPregunta(int preguntaID, int dificultadID, int categoria) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Pregunta WHERE Pregunta.DificultadID = @dificultadID and Pregunta.CategoriaID = @categoria and Pregunta.PreguntaID = @preguntaID";
            return db.QueryFirstOrDefault<Pregunta>(sql, new { preguntaID = preguntaID, dificultadID = dificultadID, categoria = categoria });
        }
    }
    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Respuesta WHERE Respuesta.PreguntaID = @idPregunta";
            return db.Query<Respuesta>(sql, new { idPregunta = idPregunta }).AsList();
        }
    }
    public static bool VerificarRespuesta(int idRespuesta) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT EsCorrecta FROM Respuesta WHERE Respuesta.RespuestaId = @idRespuesta";
            return db.QueryFirstOrDefault<bool>(sql, new { idRespuesta = idRespuesta });
        }
    }
}
}
