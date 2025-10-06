using Microsoft.Data.SqlClient;
using Dapper;
namespace TP08_PreguntadORT.Models
{

    public class Juego
    {
        public string username { get; private set; }
        public int puntajeActual { get; private set; }
        public int cantidadPreguntasCorrectas { get; private set; }
        public int contadorNroPreguntaActual { get; private set; }
        public Pregunta preguntaActual { get; private set; }
        public List<Pregunta> listaPreguntas { get; private set; }
        public List<Respuesta> listaRespuestas { get; private set; }
        public int dificultadID { get; private set; }
        public int categoriaID { get; private set; }

        public void InicializarJuego()
        {
            username = null;
            puntajeActual = 0;
            cantidadPreguntasCorrectas = 0;
            contadorNroPreguntaActual = 0;
            preguntaActual = new Pregunta(0, "", 0, 0);
            listaPreguntas = null;
            listaRespuestas = null;
        }
        public List<Categoria> ObtenerCategorias()
        {
            return BD.ObtenerCategorias();
        }
        public List<Dificultad> ObtenerDificultades()
        {
            return BD.ObtenerDificultades();
        }
        public void CargarPartida(string usuario, int dificultad, int categoria)
        {
            username = usuario;
            dificultadID = dificultad;
            categoriaID = categoria;
            listaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);

            if (listaPreguntas != null && listaPreguntas.Count > 0)
            {
                preguntaActual = listaPreguntas[0];
            }
        }
        /*public Pregunta ObtenerProximaPregunta() {
            int preguntaID = preguntaActual.PreguntaID + 1;
            int dificultadID = preguntaActual.DificultadID;
            int categoriaID = preguntaActual.CategoriaID;
            Pregunta pregunta = BD.ObtenerProximaPregunta(preguntaID, dificultadID, categoriaID);
            return pregunta;
        }*/
        public Pregunta ObtenerProximaPregunta(int dificultad, int categoria)
        {
            if (listaPreguntas == null || listaPreguntas.Count == 0)
            {
                listaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
                if (listaPreguntas == null || listaPreguntas.Count == 0)
                    throw new Exception("No hay preguntas cargadas.");
            }

            Pregunta proximaPregunta = null;

            if (preguntaActual == null)
            {
                proximaPregunta = listaPreguntas[0];
            }
            else
            {
                int indiceActual = listaPreguntas.FindIndex(p => p.PreguntaID == preguntaActual.PreguntaID);

                if (indiceActual >= 0 && indiceActual + 1 < listaPreguntas.Count)
                {
                    proximaPregunta = listaPreguntas[indiceActual + 1];
                }
            }

            preguntaActual = proximaPregunta;
            return proximaPregunta;
        }
        public List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
        {
            List<Respuesta> respuestasProximas = BD.ObtenerProximasRespuestas(idPregunta);
            return respuestasProximas;
        }
        public bool VerificarRespuesta(int idRespuesta)
        {
            bool esCorrecta;
            esCorrecta = BD.VerificarRespuesta(idRespuesta);
            if (esCorrecta)
            {
                switch (preguntaActual.DificultadID)
                {
                    case 1:
                        puntajeActual++;
                        break;
                    case 2:
                        puntajeActual += 2;
                        break;
                    case 3:
                        puntajeActual += 3;
                        break;
                }
                cantidadPreguntasCorrectas++;
            }
            contadorNroPreguntaActual++;
            preguntaActual = ObtenerProximaPregunta(listaPreguntas[contadorNroPreguntaActual].DificultadID, listaPreguntas[contadorNroPreguntaActual].CategoriaID);
            return esCorrecta;
        }

    }

}