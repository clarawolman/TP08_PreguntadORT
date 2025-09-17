using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP08_PreguntadORT.Models;

namespace TP08_PreguntadORT.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

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
        Juego.CargarPartida(username, dificultad, categoria);
        return RedirectToAction(Jugar);
    }
    public IActionResult Jugar() {
        
    }
}
