using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Auth
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

}
