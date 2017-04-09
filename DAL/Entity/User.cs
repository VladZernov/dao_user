using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAOUserProject.DAL.Entity
{
    public partial class User
    {
        private string _password;

        public int Id { get; set; }

        [Display(Name = "Registration date")]
        public DateTime CreatedAt { get; set; }
        public bool Delivery { get; set; }
        [Display(Name = "E-Mail")]
        [Required]
        public string EMail { get; set; }
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]        
        public string LastName { get; set; } 
        [StringLength(60, MinimumLength = 6)]       
        [Display(Name = "ICQ")]
        public string Icq { get; set; }
        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Password")]
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

        public void setHash(string hash)
        {
             _password = hash;
        }

        public string Salt { get; set; }
    }
}
