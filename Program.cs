
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = "Cookies";
            opt.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("Cookies")
        .AddOpenIdConnect("oidc", opt =>
        {
            opt.SignInScheme = "Cookies";
            opt.Authority = "https://localhost:5003";
            opt.RequireHttpsMetadata = false; // nunca use em produção
            opt.ClientId = "mvc.implicit";
            opt.ResponseType = "token id_token";
            opt.UsePkce = true;
            opt.SaveTokens = true;
            opt.Scope.Add("openid");
            opt.Scope.Add("profile");
            //opt.Scope.Add("offline_access");
            opt.Scope.Add("NotaFiscal");
            opt.Scope.Add("scope1");
            opt.Scope.Add("scope2");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();

        app.UseRouting();

//        app.UseAuthorization();

        app.UseAuthentication();

        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}