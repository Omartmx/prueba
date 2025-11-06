using Bunit;
using BlazorLoginApp1.Components.Pages;
using Xunit;
using System; // Necesario para Console

namespace BlazorLoginApp.Tests // Asegúrate que coincida con tu namespace
{
    public class LoginTests : TestContext
    {
        [Fact]
        public void LoginCredenciales()
        {
            Console.WriteLine("Iniciando prueba de login con credenciales correctas.");
           
            var component = RenderComponent<Login>();
    
            component.Find("input#email").Change("admin@test.com");
            component.Find("input#password").Change("12345");
            component.Find("button").Click();

            Assert.Contains("¡Login exitoso!", component.Markup);
            Console.WriteLine("Prueba completada exitosamente");
        }
    }
}