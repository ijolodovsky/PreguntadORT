namespace TP07.Models;

public static class Juego{
    private static string? userName;
    private static int puntajeActual;
    private static int preguntasCorrectas;
    private static List<Pregunta>? preguntas;
    private static List<Respuesta>? respuestas;

    public static string? Username { get => userName; }
    public static int PuntajeActual { get => puntajeActual; }
    public static int CantidadPreguntasCorrectas { get => preguntasCorrectas; }
    public static List<Pregunta>? Preguntas { get => preguntas; }
    public static List<Respuesta>? Respuestas { get => respuestas; }

    public static void InicializarJuego(){
        userName = "";
        puntajeActual = 0;
        preguntasCorrectas = 0;
        preguntas = new List<Pregunta>();
        respuestas = new List<Respuesta>();
    }

    public static List<Categoria> ObtenerCategorias(){
        return BD.ObtenerCategorias();
    }

    public static List<Dificultad> ObtenerDificultades(){
        return BD.ObtenerDificultades();
    }

    public static void CargarPartida(string username, int dificultad, int categoria){
        userName = username;
        preguntas = BD.ObtenerPreguntas(categoria, dificultad);
        respuestas = BD.ObtenerRespuestas(preguntas);
    }

    public static Pregunta ObtenerPregunta(){
        if(preguntas.Count > 0){
        Random random = new();
        int indice = random.Next(0, preguntas.Count);
        return preguntas[indice];
        }
        else{
            return null;
        }
    }

    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta){
        List<Respuesta> respuestas = new List<Respuesta>();
        foreach (Respuesta respuesta in Respuestas)
        {
            if (respuesta.IdPregunta == idPregunta)
            {
                respuestas.Add(respuesta);
            }
        }
        return respuestas;
    }

    public static bool VerificarRespuesta(int idPregunta, int idRespuesta){
        bool esCorrrecta = false;
        Respuesta respuesta = Respuestas.Find(respuesta => respuesta.IdRespuesta == idRespuesta);
        
        if(respuesta!=null){
            esCorrrecta = respuesta.Correcta;
            if(esCorrrecta){
                puntajeActual += 100;
                preguntasCorrectas++;
            }
            
        }
        Pregunta pregunta = preguntas.Find(pregunta => pregunta.IdPregunta == idPregunta);
        if(pregunta!=null){
            preguntas.Remove(pregunta);
        }
        return esCorrrecta;
    }
}