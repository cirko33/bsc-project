﻿using System.ComponentModel.DataAnnotations;

namespace Project.DTOs
{
    public class LoginDTO
    {
        [Required, MaxLength(100)]
        public string? Username { get; set; }
        [Required, MaxLength(100)]
        public string? Password { get; set; }
    }
}
