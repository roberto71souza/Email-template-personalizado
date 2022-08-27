using Email_Template.Models;
using Email_Template.UTIL;
using System.Collections.Generic;
using System.Linq;

namespace Email_Template.Services
{
    public class UsuarioService
    {
        private List<UsuarioCadastroModel> _Usuario { get; set; }

        public UsuarioService()
        {
            _Usuario = new List<UsuarioCadastroModel>();
        }

        public void AdicionarUsuario(UsuarioCadastroModel usuario)
        {
            var senhaEncriptografada = CriptografarSenha.EncodePassword(usuario.Senha);

            _Usuario.Add(new UsuarioCadastroModel() { Nome = usuario.Nome, Email = usuario.Email, Senha = senhaEncriptografada });
        }

        public void AtualizarSenhaUsuario(UsuarioCadastroModel usuario, string novaSenha)
        {
            var senhaEncriptografada = CriptografarSenha.EncodePassword(novaSenha);
            usuario.Senha = senhaEncriptografada;
        }

        public bool ValidarSenhaUsuario(string passwordModel, string passwordCompare)
        {
            var senhaDescriptografada = CriptografarSenha.DecodePassword(passwordCompare);

            return senhaDescriptografada == passwordModel;
        }

        public UsuarioCadastroModel BuscarUsuario(string email)
        {
            return _Usuario.FirstOrDefault(x => x.Email == email);
        }


    }
}
