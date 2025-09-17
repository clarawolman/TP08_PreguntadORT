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
    public void CargarPartida(string username, int dificultad, int categoria) {
        InicializarJuego();
        
    }

}
