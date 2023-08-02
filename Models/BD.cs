using System.Data.SqlClient;
using Dapper;

namespace TP07.Models;

public static class BD{
    private static string cadenaConexion = @"Server=localhost;Database=PreguntadORT;Trusted_Connection=True;";

    public static List<Categoria> ObtenerCategorias(){
        using(SqlConnection connection = new SqlConnection(cadenaConexion)){
            string sql = "SELECT * FROM Categorias";
            return connection.Query<Categoria>(sql).ToList();
        }
    }

    public static List<Dificultad> ObtenerDificultades(){
        using(SqlConnection connection = new SqlConnection(cadenaConexion)){
            string sql = "SELECT * FROM Dificultades";
            return connection.Query<Dificultad>(sql).ToList();
        }
    }

    public static List<Pregunta> ObtenerPreguntas(int idCategoria, int idDificultad){
        using(SqlConnection connection = new SqlConnection(cadenaConexion)){
            string sql = "SELECT * FROM Preguntas WHERE IdCategoria = @IdCategoria AND IdDificultad = @IdDificultad";
            return connection.Query<Pregunta>(sql, new {IdCategoria = idCategoria, IdDificultad = idDificultad}).ToList();
        }
    }

    public static List<Respuesta> ObtenerRespuestas(List<Pregunta> preguntas){
        using(SqlConnection connection = new SqlConnection(cadenaConexion)){
            string sql = "SELECT * FROM Respuestas WHERE IdPregunta = @IdPregunta";
            List<Respuesta> respuestas = new();
            foreach(Pregunta pregunta in preguntas){
                respuestas.AddRange(connection.Query<Respuesta>(sql, new {IdPregunta = pregunta.IdPregunta}).ToList());
            }
            return respuestas;
        }
    }
}