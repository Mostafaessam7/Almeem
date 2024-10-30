using System.ComponentModel.DataAnnotations;

namespace AlmeemDashboard.Models
{
    public class LoginModel 
    { 
        public string Email { get; set; }
        public string Password { get; set; }
        public string TwoFactorCode { get; set; }
        public string TwoFactorRecoveryCode { get; set; }
    }

}
