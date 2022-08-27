using Email_Template.Models;
using Email_Template.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Email_Template.Controllers
{
    public class HomeController : Controller
    {
        private EmailService _emailService { get; set; }
        private UsuarioService _usuarioService { get; set; }
        public HomeController(EmailService emailService, UsuarioService usuarioService)
        {
            _emailService = emailService;
            _usuarioService = usuarioService;
        }

        [HttpGet()]
        public IActionResult Index() => View();

        [HttpGet()]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UseEmail")))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost()]
        public IActionResult Login(UsuarioLoginModel usuarioLogin)
        {
            if (ModelState.IsValid)
            {
                var usuarioRetorno = _usuarioService.BuscarUsuario(usuarioLogin.Email);

                if (usuarioRetorno is not null)
                {
                    if (_usuarioService.ValidarSenhaUsuario(usuarioLogin.Senha, usuarioRetorno.Senha))
                    {
                        HttpContext.Session.SetString("UseEmail", usuarioRetorno.Email);

                        _emailService.SendEmailLoginUsuario(usuarioLogin.Email, usuarioRetorno.Nome);

                        return RedirectToAction("Perfil");
                    }
                    ModelState.AddModelError("Senha", "Senha Incorreta");

                    return View();
                }

                ModelState.AddModelError("Email", "Usuário não existe no sistema");
            }
            return View();
        }

        [HttpGet()]
        public IActionResult Cadastrar()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UseEmail")) is false)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost()]
        public IActionResult Cadastrar(UsuarioCadastroModel usuarioCadastro)
        {
            if (ModelState.IsValid)
            {
                if (_usuarioService.BuscarUsuario(usuarioCadastro.Email) is null)
                {
                    _usuarioService.AdicionarUsuario(usuarioCadastro);

                    _emailService.SendEmailCadastroUsuario(usuarioCadastro);

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Email", "Usuário já existe no sistema");
            }
            return View();
        }

        [HttpGet()]
        public IActionResult Perfil() => View();

        [HttpGet()]
        public IActionResult MudarSenha() => View();

        [HttpPost()]
        public IActionResult MudarSenha(string novaSenha)
        {
            var usuarioEmail = HttpContext.Session.GetString("UseEmail");

            var usuarioRetorno = _usuarioService.BuscarUsuario(usuarioEmail);

            if (usuarioRetorno is not null)
            {

                if (_usuarioService.ValidarSenhaUsuario(novaSenha, usuarioRetorno.Senha) is false)
                {
                    _usuarioService.AtualizarSenhaUsuario(usuarioRetorno, novaSenha);

                    _emailService.SendEmailAlteracaoSenhaUsuario(usuarioRetorno, novaSenha);

                    return RedirectToAction("Perfil");
                }

                ModelState.AddModelError("", "Senha atual não pode ser a mesma que a anterior.");

                return View();
            }
            ModelState.AddModelError("", "Erro ao buscar usuario.");
            return View();
        }

        [HttpGet()]
        public IActionResult Deslogar()
        {
            HttpContext.Session.Remove("UseEmail");

            return RedirectToAction("Index");
        }
    }
}
