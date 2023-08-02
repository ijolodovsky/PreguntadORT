namespace TP07.Models;

public static class Juego{
    private static string? userName;
    private static int puntajeActual;
    private static int preguntasCorrectas;
    private static List<Pregunta>? preguntas;
    private static List<Respuesta>? respuestas;

    public static void InicializarJuego(){
        userName = "";
        puntajeActual = 0;
        preguntasCorrectas = 0;
        preguntas = new();
        respuestas = new();
    }

    public static List<Categoria> ObtenerCategorias(){
        return BD.ObtenerCategorias();
    }

    public static List<Dificultad> ObtenerDificultades(){
        return BD.ObtenerDificultades();
    }

    public static void CargarPartida(string username, int dificultad, int categoria){
        preguntas = BD.ObtenerPreguntas(categoria, dificultad);
        respuestas = BD.ObtenerRespuestas(preguntas);
    }

    public static Pregunta ObtenerPregunta(){
        if(preguntas!.Count > 0){
        Random random = new();
        int indice = random.Next(preguntas!.Count);
        return preguntas[indice];
        }
        else{
            return null;
        }
    }

    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta){
        return respuestas!.FindAll(respuesta => respuesta.IdPregunta == idPregunta);
    }

    public static bool VerificarRespuesta(int idPregunta, int idRespuesta){
        Pregunta pregunta = preguntas!.Find(pregunta => pregunta.IdPregunta == idPregunta);
        Respuesta respuesta = respuestas!.Find(respuesta => respuesta.IdRespuesta == idRespuesta);
        
        if(pregunta!=null && respuesta!=null && respuesta.Correcta){
            puntajeActual += 100;
            preguntasCorrectas++;
            preguntas.Remove(pregunta);
            respuestas.RemoveAll(respuesta => respuesta.IdPregunta == idPregunta);
            return true;
        }
        else{
            return false;
        }
    }
}