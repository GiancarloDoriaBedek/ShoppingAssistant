namespace ShoppingAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dependecyMapping = new DependencyMapping(args);
            var app = dependecyMapping.BuildMappedWebApplication();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}