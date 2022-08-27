using System.ComponentModel.DataAnnotations;

namespace Email_Template.Models
{
    public class UsuarioCadastroModel
    {
        [Required(ErrorMessage = "{0} é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é requerido")
        , EmailAddress(ErrorMessage = "email com formato invalido, ex: email@email.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public string Senha { get; set; }
    }
}
