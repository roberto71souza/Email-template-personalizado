using System.IO;

namespace Email_Template.UTIL
{
    public static class Template
    {
        public static readonly string TEMPLATE_CADASTRO_USUARIO;
        public static readonly string TEMPLATE_LOGIN_USUARIO;
        public static readonly string TEMPLATE_ALTERACAO_SENHA;

        static Template()
        {
            TEMPLATE_CADASTRO_USUARIO = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\template\template-email-cadastro.html");
            TEMPLATE_LOGIN_USUARIO = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\template\template-email-login.html");
            TEMPLATE_ALTERACAO_SENHA = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\template\template-alteracao-senha.html");
        }
    }
}
