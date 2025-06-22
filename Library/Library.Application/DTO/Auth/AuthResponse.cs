using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
    }


}
