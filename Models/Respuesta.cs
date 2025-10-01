namespace TP08_PreguntadORT.Models
{
    public class Respuesta
    {
        public int RespuestaID { get; set; }
        public int PreguntaID { get; set; }
        public string Texto { get; set; }
        public bool EsCorrecta { get; set; }
        public string ImagenURL { get; set; }
    }
}