namespace TP07.Models;
public class Pregunta{
    public int IdPregunta { get; set; }
    public int IdCategoria { get; set; }
    public int IdDificultad { get; set; }
    public string? Enunciado { get; set; }
    public string? Foto { get; set; }
}