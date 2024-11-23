using CandidateApp.DataAccess;

namespace CandidateApp.API.Infrastructures
{
    public static class ConfigAppBuild
    {
        public static IApplicationBuilder UseCustomAppBuild(this IApplicationBuilder app)
        {
            app.ApplicationServices.ApplyMigrationsAndCreateDatabase();


            // Example: Custom middleware logic
            //app.Use(async (context, next) =>
            //{
            //    // Custom logic before controller execution
            //    Console.WriteLine("Before controller execution");

            //    await next.Invoke();

            //    // Custom logic after controller execution
            //    Console.WriteLine("After controller execution");
            //});


            app.UseHttpsRedirection();  // Optional: Enable HTTPS redirection.

            //app.UseStaticFiles();

            app.UseRouting();

            // Enable Swagger middleware
            app.UseSwagger(); // Enable Swagger UI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate API");
                options.RoutePrefix = string.Empty; // Makes Swagger UI available at the root (e.g. http://localhost:5000/)
            });

            return app;

        }

    }
}
