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
    public int dificultadID { get; set; }
    public int categoriaID { get; set; }

    private void InicializarJuego()
    {
        username = null;
        puntajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        preguntaActual = new Pregunta(0, "", 0, 0);
        listaPreguntas = null;
        listaRespuestas = null;
    }
    public List<Categoria> ObtenerCategorias() {
        return ObtenerCategorias();
    }
    public List<Dificultad> ObtenerDificultades() {
        return ObtenerDificultades();   
    }
    public void CargarPartida(string usuario, int dificultad, int categoria) {
        username = usuario;
        listaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
    }
    public void ObtenerProximaPregunta() {
        int preguntaID = preguntaActual.PreguntaID + 1;
        int dificultadID = preguntaActual.DificultadID;
        int categoriaID = preguntaActual.CategoriaID;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Pregunta WHERE Pregunta.DificultadID = @dificultadID and Pregunta.CategoriaID = @categoria and Pregunta.IdPregunta = @preguntaID";
            return db.Query<Pregunta>(sql).AsList();
        }
    }
    public void ObtenerProximasRespuestas(int idPregunta) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Respuesta WHERE Respuesta.IdPregunta = @idPregunta";
            return db.Query<Respuesta>(sql).AsList();
        }
    }
    public bool VerificarRespuesta(int idRespuesta) {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            bool sql = "SELECT EsCorrecta FROM Respuesta WHERE Respuesta.RespuestaId = @idRespuesta";
            return db.Query<Respuesta>(sql);
        }
        if(sql)
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
        return sql;
    }

}
