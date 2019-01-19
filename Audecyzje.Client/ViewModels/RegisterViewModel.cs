using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Audecyzje.Client.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class ManageViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool Blocked { get; set; }

        public string UserId { get; set; }
    }
}
