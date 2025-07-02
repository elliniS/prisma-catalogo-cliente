using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;
using System;
using PrismaCatalogo.Validations;
using NuGet.Packaging.Licenses;
using FluentValidation.AspNetCore;

namespace AuthFacil.Mvc.Controllers;

public class AuthController : Controller
{
    private IUsuarioService _usuarioService;

    public AuthController(IUsuarioService usuario)
    {
        _usuarioService = usuario;
    }

    // GET: Funcionario/FuncionarioUsuarios/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Funcionario/FuncionarioUsuarios/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,NomeUsuario,Senha")] UsuarioViewModel usuario)
    {
        usuario.UsuarioTipo = PrismaCatalogo.Enuns.EnumUsuarioTipo.Cliente;
        UsuarioValidator validations = new UsuarioValidator();
        var resul = validations.Validate(usuario);

        if (resul.IsValid)
        {
            try
            {
                var result = await _usuarioService.Create(usuario);

                return RedirectToAction(nameof(Login));

            }
            catch(Exception e )
            {
                ViewData["mensagemError"] = e.Message;
            }
        }

        return View();
    }

    // GET: Funcionario/Usuarios/Edit/5
    public async Task<IActionResult> MinhaConta()
    {
        try
        {
            var usuario = await _usuarioService.FindById(Convert.ToInt32(User.FindFirst("Id")?.Value.Trim()));

            if (usuario == null)
            {
                throw new Exception();
            }
            return View(usuario);
        }
        catch
        {
            ViewData["mensagemError"] = "Erro ao acessar tela de edição!";
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MinhaConta([Bind("Nome,NomeUsuario,Email,Senha")] UsuarioViewModel usuarioViewModel)
    {
        UsuarioUpdateValidator validations = new UsuarioUpdateValidator();
        var resul = validations.Validate(usuarioViewModel);

        if (resul.IsValid)
        {
            try
            {
                var re = await _usuarioService.Update(Convert.ToInt32(User.FindFirst("Id")?.Value.Trim()), usuarioViewModel);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewData["mensagemError"] = e.Message;
            }
        }

        ModelState.Clear();
        resul.AddToModelState(ModelState);

        return View(usuarioViewModel);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([Bind("NomeUsuario,Senha,FgLembra,ReturnUrl")] UsuarioLoginViewModel logInModel)
    {

        try
        {
            var usuario = await _usuarioService.Login(logInModel);

            //if (usuario == null)
            //{
            //    ViewData["mensagemError"] = "Erro ao realizar Login!";

            //    return View();
            //}

            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, usuario.NomeUsuario),
            new Claim(ClaimTypes.Role, usuario.UsuarioTipo.ToString()),
            new Claim("Id", usuario.Id.ToString()),
            new Claim("Token", usuario.Token),
            new Claim("RefreshToken", usuario.RefreshToken)
            ];
            var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var identity = new ClaimsIdentity(claims, authScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(authScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true //logInModel.FgLembra
                });

            if (!String.IsNullOrWhiteSpace(logInModel.ReturnUrl))
            {
                return Redirect(logInModel.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        catch(Exception e)
        {
            ViewData["mensagemError"] = e.Message;

            return View();
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EnviaCodigoSenha([Bind("NomeUsuario")] UsuarioLoginViewModel usuarioLoginView)
    {
        try
        {
           var u = await _usuarioService.CodigoReenviaSenha(usuarioLoginView);

            return RedirectToAction("ConfirmaCodigo", new { usuarioId = u.UsuarioId });


        }
        catch (Exception e) {
            ViewData["mensagemError"] = e.Message;
            return RedirectToRoute("Auth.Login");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmaCodigo(int usuarioId)
    {
        var c = new ReenviaSenhaViewModel() { 
            UsuarioId = usuarioId 
        };

        return View(c);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmaCodigo([Bind("UsuarioId,Codigo")] ReenviaSenhaViewModel reenviaSenhaViewModel)
    {
        try
        {
            var u = await _usuarioService.VerificaCodigo(reenviaSenhaViewModel);

            u.Codigo = reenviaSenhaViewModel.Codigo;

            return RedirectToAction("AlteraSenha", u);
        }
        catch (Exception e)
        {
            ViewData["mensagemError"] = e.Message;
            return View(reenviaSenhaViewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> AlteraSenha( ReenviaSenhaViewModel reenviaSenhaViewModel)
    {
        return View(reenviaSenhaViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AlteraSenhaPost([Bind("UsuarioId,Codigo,Senha,ConfirmaSenha")] ReenviaSenhaViewModel reenviaSenhaViewModel)
    {
        try
        {
            var u = await _usuarioService.AlteraSenha(reenviaSenhaViewModel);

            return RedirectToRoute("Auth.Login");
        }
        catch (Exception e)
        {
            ViewData["mensagemError"] = e.Message;
            return RedirectToRoute("Auth.Login");
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToRoute("Auth.Login");
    }

    public IActionResult Unauthorized()
    {
        return View();
    }
}
