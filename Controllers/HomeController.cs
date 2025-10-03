using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP08_PreguntadORT.Models;

namespace TP08_PreguntadORT.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    Juego juegoNuevo = new Juego();
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ConfigurarJuego(string username, int dificultad, int categoria)
    {
        if (string.IsNullOrEmpty(nombre) || int.IsNullOrEmpty(dificultad) || int.IsNullOrEmpty(categoria))
    {
        ViewBag.Error = "Debés ingresar nombre, dificultad y categoría.";
        return View("ConfigurarJuego");
    }
        juegoNuevo.InicializarJuego();
        return RedirectToAction(Comenzar(username, dificultad, categoria));
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        juegoNuevo.CargarPartida(username, dificultad, categoria);
        return RedirectToAction(Jugar);
    }
    public IActionResult Jugar()
    {
        if (BD.ObtenerPreguntas().Count() > 0) {
            ViewBag.Pregunta = juegoNuevo.ObtenerProximaPregunta();
            ViewBag.ListaRespuestas = ObtenerProximasRespuestas(juegoNuevo.ObtenerProximaPregunta().preguntaID); //??? 
            return View("Juego");
        }
        else{
            return View("Fin");
        }
    }
    [HttpPost]
    public IActionResult Juego(int respuestaElegida)
    { 
        juegoNuevo.VerificarRespuesta();
        return View(); //deberia returnear view respuesta pero vamos a hacer que aparezca en la misma view con javascript onclick porq binker me dijo
    }
}
