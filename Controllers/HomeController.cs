using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego(){

        ViewBag.Categorias = Juego.ObtenerCategorias();
        ViewBag.Dificultades = Juego.ObtenerDificultades();
        Juego.InicializarJuego();
        return View();
    }

    public IActionResult Comenzar(string username, int dificultad, int categoria){
        
        Juego.CargarPartida(username, dificultad, categoria);
        if(Juego.ObtenerPregunta() != null){
            return View("Jugar");
        }
        else{
            return View("ConfigurarJuego");
        }
    }

    public IActionResult Jugar(){
        if(Juego.ObtenerPregunta() == null){
            return RedirectToAction("Fin");
        } else{
            Pregunta pregunta = Juego.ObtenerPregunta();
            ViewBag.Pregunta = pregunta;
            ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(pregunta.IdPregunta);
            return View("Juego");
        }
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
        bool respuesta = Juego.VerificarRespuesta(idPregunta, idRespuesta);
        ViewBag.EsCorrecta = respuesta;
        return View("Respuesta");
    }
}
