using Microsoft.Data.SqlClient;
using Dapper;
public class Juego
{
    public string username { get; set; }
    public int puntajeActual { get; set; }
    public int cantidadPreguntasCorrectas { get; set; }
    public int contadorNroPreguntaActual { get; set; }
    public Pregunta preguntaActual { get; set; }
    public List<Pregunta> listaPreguntas { get; set; }
    public List<Respuesta> listaRespuestas { get; set; }

    private void InicializarJuego()
    {
        username = null;
        puntajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        preguntaActual = null;
        listaPreguntas = null;
        listaRespuestas = null;
    }
    public List<Categoria> ObtenerCategorias() {
        
    }
    public List<Dificultad> ObtenerDificultades() {
        
    }
    public void CargarPartida(string usuario, int dificultad, int categoria) {
        username = usuario;
        BD.ObtenerPreguntas(dificultad, categoria);
    }
    public void ObtenerProximaPregunta() {
        //Retorna la siguiente pregunta de la lista de preguntas del juego.
    }
    public void ObtenerProximasRespuestas(int idPregunta) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Respuesta WHERE Respuesta.IdPregunta = @idPregunta";
            return db.Query<Respuesta>(sql).AsList();
        }
    }
    public bool VerificarRespuesta(int idRespuesta) {
        //Si la respuesta del usuario fue correcta, suma una cantidad específica de puntos a PuntajeActual (la definen ustedes) y suma 1 respuesta correcta en CantidadPreguntasCorrectas.
        //Independientemente de que este bien o mal la respuesta, Incrementa el contador de ContadorNroPreguntaActual y asigna la siguiente pregunta de la ListaPreguntas a PreguntaActual.
        listaRespuestas = ObtenerProximasRespuestas();
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT EsCorrecta FROM Respuesta WHERE Respuesta.RespuestaId = @idRespuesta";
            return db.Query<Respuesta>(sql);
        }
    }

}
