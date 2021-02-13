using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DockerComposeExample.Repository.Context;

namespace DockerComposeExample.IoC
{
    public static class MontadorAmbiente
    {
        public static void MontarAmbienteSqlServer(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
            //    var context = serviceScope.ServiceProvider.GetService<DockerComposeExampleDbContext>();
            //    System.Console.WriteLine("Aplicando Migrations...");
            //    context.Database.Migrate();   
            }
        }
    }
}
