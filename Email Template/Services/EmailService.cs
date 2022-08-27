using Email_Template.Models;
using Email_Template.UTIL;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Email_Template.Services
{
    public class EmailService
    {
        public IHostEnvironment _hostEnvironment { get; set; }

        public IFluentEmail _fluentEmail { get; set; }
        public EmailService(IHostEnvironment hostEnvironment, IFluentEmail fluentEmail)
        {
            _hostEnvironment = hostEnvironment;
            _fluentEmail = fluentEmail;
        }

        public void SendEmailCadastroUsuario(UsuarioCadastroModel usuario)
        {
            var bodyPath = Template.TEMPLATE_CADASTRO_USUARIO;
            var body = "";
            var subject = "Confirmação de cadastro";

            using (StreamReader stream = new StreamReader(bodyPath))
            {
                body = stream.ReadToEnd();
            }

            body = body.Replace("#NOME-USUARIO", usuario.Nome);
            body = body.Replace("#USUARIO-LOGIN", usuario.Email);
            body = body.Replace("#SENHA", usuario.Senha);

            var email = new List<Address>() { new Address() { EmailAddress = usuario.Email } };

            SendEmail(email, subject, body);
        }

        public void SendEmailLoginUsuario(string email, string nome)
        {
            var bodyPath = Template.TEMPLATE_LOGIN_USUARIO;
            var body = "";
            var subject = "Verificação de Login";

            using (StreamReader stream = new StreamReader(bodyPath))
            {
                body = stream.ReadToEnd();
            }

            body = body.Replace("#NOME-USUARIO", nome);
            body = body.Replace("#DATA", DateTime.Now.ToString("dd/MM/yy"));
            body = body.Replace("#HORA", DateTime.Now.ToString("HH:mm"));

            var emails = new List<Address>() { new Address() { EmailAddress = email } };

            SendEmail(emails, subject, body);
        }

        public void SendEmailAlteracaoSenhaUsuario(UsuarioCadastroModel usuario, string novaSenha)
        {
            var bodyPath = Template.TEMPLATE_ALTERACAO_SENHA;
            var body = "";
            var subject = "Confirmação de alteração de senha";

            using (StreamReader stream = new StreamReader(bodyPath))
            {
                body = stream.ReadToEnd();
            }

            body = body.Replace("#NOME-USUARIO", usuario.Nome);
            body = body.Replace("#DATA", DateTime.Now.ToString("dd/MM/yy"));
            body = body.Replace("#HORA", DateTime.Now.ToString("HH:mm"));
            body = body.Replace("#NOVA-SENHA", novaSenha);

            var emails = new List<Address>() { new Address() { EmailAddress = usuario.Email } };

            SendEmail(emails, subject, body);
        }

        private void SendEmail(IEnumerable<Address> email, string subject, string body)
        {
            _fluentEmail.To(email).Subject(subject).Body(body, true);

            _fluentEmail.Send();
        }
    }
}
