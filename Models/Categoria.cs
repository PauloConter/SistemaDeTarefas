using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaDeTarefas.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [JsonIgnore]
        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}

