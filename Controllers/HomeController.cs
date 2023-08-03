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
        Juego.InicializarJuego();
        ViewBag.Categorias = Juego.ObtenerCategorias();
        ViewBag.Dificultades = Juego.ObtenerDificultades();
        return View();
    }

    [HttpGet]
    public IActionResult Comenzar(string username, int dificultad, int categoria){
        
        Juego.CargarPartida(username, dificultad, categoria);
        if(Juego.Preguntas.Count > 0){
            return RedirectToAction("Jugar");
        }
        else{
            return RedirectToAction("ConfigurarJuego");
        }
    }

    public IActionResult Jugar(){
        ViewBag.Username = Juego.Username;
        ViewBag.Puntaje = Juego.PuntajeActual;
        ViewBag.CantidadPreguntasCorrectas = Juego.CantidadPreguntasCorrectas;
        Pregunta pregunta = Juego.ObtenerPregunta();
        if(pregunta == null){
            return View("Fin");
        } else{
            List<Respuesta> respuestas = Juego.ObtenerProximasRespuestas(pregunta.IdPregunta);
            ViewBag.Pregunta = pregunta;
            ViewBag.Respuestas = respuestas;
            return View("Jugar");
        }
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
        bool respuesta = Juego.VerificarRespuesta(idPregunta, idRespuesta);
        ViewBag.EsCorrecta = respuesta;
        return View("Respuesta");
    }
}
