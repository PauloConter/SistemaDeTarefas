﻿using System.ComponentModel.DataAnnotations;

namespace SistemaDeTarefas.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
