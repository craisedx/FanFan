﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace FanFan.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан адрес")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Remote("CheckEmail", "Account",HttpMethod ="Post", ErrorMessage = "Аккаунт уже существует")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}