using System.ComponentModel.DataAnnotations;

namespace Email_Template.Models
{
    public class UsuarioLoginModel
    {
        [Required(ErrorMessage = "{0} ? requerido")
            , EmailAddress(ErrorMessage = "email com formato invalido, ex: email@email.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} ? requerido")]
        public string Senha { get; set; }
    }
}
