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

    public IActionResult ConfigurarJuego(){
        return View();
    }
    public IActionResult Configurar(string nombre, int dificultad, int categoria)
    {
        if (string.IsNullOrEmpty(nombre))
        {
        ViewBag.Error = "Debés ingresar nombre, dificultad y categoría.";
        return View("ConfigurarJuego");
        }
        juegoNuevo.InicializarJuego();
        return RedirectToAction("Comenzar", new { nombre = nombre, dificultad = dificultad, categoria = categoria });

    }
    public IActionResult Comenzar(string nombre, int dificultad, int categoria)
    {
        juegoNuevo.CargarPartida(nombre, dificultad, categoria);
        return RedirectToAction("Jugar", new { dificultad = dificultad, categoria = categoria });

    }
    public IActionResult Jugar(int dificultad, int categoria)
    {
        if (BD.ObtenerPreguntas(dificultad, categoria).Count() > 0) {
            ViewBag.Pregunta = juegoNuevo.ObtenerProximaPregunta(dificultad, categoria);
            ViewBag.ListaRespuestas = juegoNuevo.ObtenerProximasRespuestas(juegoNuevo.preguntaActual.PreguntaID + 1); //??? 
            return View("Juego");
        }
        else{
            return View("Fin");
        }
    }
    [HttpPost]
    public IActionResult Juego(int respuestaElegidaID)
    { 
        juegoNuevo.VerificarRespuesta(respuestaElegidaID);
        return View(); //js
    }
}
