using SistemaDeTarefas.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Tarefa
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataConclusao { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int CategoriaId { get; set; }
    
    public Usuario Usuario { get; set; }
    public Categoria Categoria { get; set; }
}
