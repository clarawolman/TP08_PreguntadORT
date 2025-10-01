using Microsoft.Data.SqlClient;
using Dapper;

namespace TP08_PreguntadORT.Models
{
    public class Juego
{
    private string _connectionString = "Server=.;Database=preguntadORT;Trusted_Connection=True;TrustServerCertificate=True;";
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
        return BD.ObtenerCategorias();
    }
    public List<Dificultad> ObtenerDificultades() {
        return BD.ObtenerDificultades();   
    }
    public void CargarPartida(string usuario, int dificultad, int categoria) {
        username = usuario;
        listaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
        if (listaPreguntas != null && listaPreguntas.Count > 0)
        {
            preguntaActual = listaPreguntas[0];
            listaRespuestas = BD.ObtenerRespuestas(preguntaActual.PreguntaID);
        }
    }
    public void SeleccionarPreguntaPorIndice(int indice)
    {
        if (listaPreguntas == null || listaPreguntas.Count == 0) return;
        if (indice < 0 || indice >= listaPreguntas.Count) return;
        contadorNroPreguntaActual = indice;
        preguntaActual = listaPreguntas[indice];
        listaRespuestas = ObtenerProximasRespuestas(preguntaActual.PreguntaID);
    }
    public Pregunta ObtenerProximaPregunta() {
        if (listaPreguntas == null || listaPreguntas.Count == 0) return null;
        int currentIndex = listaPreguntas.FindIndex(p => p.PreguntaID == (preguntaActual?.PreguntaID ?? -1));
        int nextIndex = currentIndex + 1;
        if (nextIndex >= 0 && nextIndex < listaPreguntas.Count)
        {
            return listaPreguntas[nextIndex];
        }
        return null;
    }
    public List<Respuesta> ObtenerProximasRespuestas(int idPregunta) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Respuesta WHERE PreguntaID = @idPregunta ORDER BY RespuestaID";
            return db.Query<Respuesta>(sql, new { idPregunta }).ToList();
        }
    }
    public bool VerificarRespuesta(int idRespuesta) {
        bool esCorrecta = false;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT EsCorrecta FROM Respuesta WHERE RespuestaID = @idRespuesta";
            esCorrecta = db.QuerySingleOrDefault<bool>(sql, new { idRespuesta });
        }
        
        if(esCorrecta)
        {
            switch(preguntaActual.DificultadID)
            {
                case 1:
                puntajeActual++;
                break;
                case 2:
                puntajeActual+=2;
                break;
                case 3:
                puntajeActual+=3;
                break;
            }
            cantidadPreguntasCorrectas++;
        }
        contadorNroPreguntaActual++;
        preguntaActual = ObtenerProximaPregunta();
        if (preguntaActual != null)
        {
            listaRespuestas = ObtenerProximasRespuestas(preguntaActual.PreguntaID);
        }
        return esCorrecta;
    }
    }
}
