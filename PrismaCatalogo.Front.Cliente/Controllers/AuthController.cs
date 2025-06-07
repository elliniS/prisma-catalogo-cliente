﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;
using System;
using PrismaCatalogo.Validations;
using NuGet.Packaging.Licenses;

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
