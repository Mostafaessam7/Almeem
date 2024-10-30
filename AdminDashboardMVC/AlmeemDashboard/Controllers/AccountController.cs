using System.Net.Http;
using System.Net.Http.Json;
using AlmeemDashboard.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;

    public AccountController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7051/");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
          //  var response = await _httpClient.PostAsJsonAsync(
          //    "api/login?useCookies=true&useSessionCookies=true",
          //    model
          //);

            var response = await _httpClient.PostAsJsonAsync(
                "api/login?useCookies=true",
                model
            );

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Login failed");
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred");
            return View(model);
        }
    }


    public IActionResult Register()
    {
        return View();
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }



}