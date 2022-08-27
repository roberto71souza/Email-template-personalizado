using System.ComponentModel.DataAnnotations;

namespace Email_Template.Models
{
    public class MudarSenhaModel
    {
        [Required(ErrorMessage = "{0} é requerido"), Display(Name = "Senha atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "{0} é requerido"), Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }
    }
}
