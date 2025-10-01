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
        ViewBag.Categorias = BD.ObtenerCategorias();
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        ViewBag.Dificultades = BD.ObtenerDificultades();
        ViewBag.Categorias = BD.ObtenerCategorias();
        return View();
    }
    [HttpPost]
    public IActionResult Comenzar(string username, int dificultad, int categoriaSeleccionada)
    {
        HttpContext.Session.SetString("username", username);
        HttpContext.Session.SetInt32("dificultad", dificultad);
        HttpContext.Session.SetInt32("categoria", categoriaSeleccionada);
        HttpContext.Session.SetInt32("indiceActual", 0);
        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {
        var username = HttpContext.Session.GetString("username");
        var dificultad = HttpContext.Session.GetInt32("dificultad");
        var categoria = HttpContext.Session.GetInt32("categoria");
        var indiceActual = HttpContext.Session.GetInt32("indiceActual") ?? 0;

        if (string.IsNullOrEmpty(username) || dificultad == null || categoria == null)
        {
            ViewBag.Error = "Configurá el juego antes de jugar.";
            return RedirectToAction("Index");
        }

        juegoNuevo = new Juego();
        juegoNuevo.CargarPartida(username, dificultad.Value, categoria.Value);
        juegoNuevo.SeleccionarPreguntaPorIndice(indiceActual);

        ViewBag.username = username;
        ViewBag.puntajeActual = juegoNuevo.puntajeActual;
        ViewBag.contadorNroPreguntaActual = indiceActual;
        ViewBag.preguntaActual = juegoNuevo.preguntaActual;
        ViewBag.listaRespuestas = juegoNuevo.listaRespuestas;
        ViewBag.Categorias = BD.ObtenerCategorias();

        if (juegoNuevo.preguntaActual == null)
        {
            ViewBag.Fin = "¡Fin de la partida!";
        }
        return View();
    }
    [HttpPost]
    public IActionResult VerificarRespuesta(int idRespuesta)
    {
        var username = HttpContext.Session.GetString("username");
        var dificultad = HttpContext.Session.GetInt32("dificultad");
        var categoria = HttpContext.Session.GetInt32("categoria");
        var indiceActual = HttpContext.Session.GetInt32("indiceActual") ?? 0;

        if (string.IsNullOrEmpty(username) || dificultad == null || categoria == null)
        {
            return RedirectToAction("Index");
        }

        juegoNuevo = new Juego();
        juegoNuevo.CargarPartida(username, dificultad.Value, categoria.Value);
        juegoNuevo.SeleccionarPreguntaPorIndice(indiceActual);

        bool esCorrecta = juegoNuevo.VerificarRespuesta(idRespuesta);
        int nuevoIndice = indiceActual + 1;
        HttpContext.Session.SetInt32("indiceActual", nuevoIndice);

        if (juegoNuevo.preguntaActual == null)
        {
            TempData["Mensaje"] = esCorrecta ? "¡Respuesta correcta! Fin de la partida." : "Respuesta incorrecta. Fin de la partida.";
            return RedirectToAction("Index");
        }
        return RedirectToAction("Jugar");
    }
    public IActionResult Juego()
    { // eu increible lo poco que entiendo
        
        return View();
    }
}
