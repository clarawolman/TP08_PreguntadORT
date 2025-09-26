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
    public IActionResult ConfigurarJuego() {
        return View();
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria) {
        juegoNuevo.CargarPartida(username, dificultad, categoria);
        return RedirectToAction(Jugar);
    }
    public IActionResult Jugar() {
        ViewBag.preguntaActual = juegoNuevo.Algo();
        ViewBag.listaRespuestas = juegoNuevo.Algo();
        if(/*si hay mas preguntas va a lo de respuesta, si no dice "fin"*/)
        {
            
        }
        ViewBag.respuestaCorrecta = juegoNuevo.Algo(); //???        
    }
}
