using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;

namespace DAOUserProject.DAL.Entity
{
    public partial class User
    {
        private string _password;

        public int Id { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Рассылка")]
        [Required]
        public bool Delivery { get; set; }
        [Display(Name = "E-Mail")]
        [Required]
        public string EMail { get; set; }
        [Display(Name = "Имя")]
        [Required]
        [RegularExpression(@"^[а-яА-Я]+$")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [Required]
        [RegularExpression(@"^[а-яА-Я]+$")]        
        public string LastName { get; set; } 
        [StringLength(60, MinimumLength = 6)]       
        [Display(Name = "ICQ")]
        public string Icq { get; set; }
        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [Required]
        public string Password 
        { 
            get
            {
                return _password;
            } 
            set
            {
                // generate a 128-bit salt using a secure PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                this.Salt = Convert.ToBase64String(salt);
                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                _password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: value,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            } 
        }

        public string Salt { get; set; }
    }
}
