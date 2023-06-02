using Bogus.DataSets;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<ICrudService<User>, CrudService<User>>();
//builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

builder.Services.AddSingleton<ICrudService<User>, CrudService<User>>(provider => new CrudService<User>(provider.GetService<EntityFaker<User>>()!,
                                                                                                       provider.GetService<IConfiguration>()!.GetSection("Fakers").GetValue<int>("Min"),
                                                                                                       provider.GetService<IConfiguration>()!.GetSection("Fakers").GetValue<int>("Max")));
builder.Services.AddTransient<EntityFaker<User>>(provider => new UserFaker(provider.GetService<IConfiguration>()!.GetSection("Fakers")["Language"]));


/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/bye";
        options.LogoutPath = "/";
    });
*/


var signingKey = Encoding.Default.GetBytes(Guid.NewGuid().ToString());

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        RequireExpirationTime = true,
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.Use(async (context, next) =>
    {

    await next();


        Console.WriteLine(context.User.Identity?.Name);
    Console.WriteLine($"{context.Request.Path} => {context.Response.StatusCode}");

});


//app.MapGet("/Users", async context => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<ICrudService<User>>()!.ReadAsync()) );
app.MapGet("/Users", async (ICrudService<User> service) => await service.ReadAsync());

                                //wymagane jedno z wymienionych uprawnieñ
app.MapGet("/Users/{id:int}", [Authorize(Roles = "ReadSingle, User")] async (int id, ICrudService<User> service) => await service.ReadAsync(id));
app.MapDelete("/Users/{id:int}", async (int id, ICrudService<User> service) => await service.DeleteAsync(id)).RequireAuthorization();

                        //wymagane wszystkie uprawnienia
app.MapPost("/Users", [Authorize(Roles = "Add")][Authorize(Roles = "User")] async (User user, ICrudService<User> service) => await service.CreateAsync(user));
app.MapPut("/Users/{id:int}", async (int id, User user, ICrudService<User> service) => await service.UpdateAsync(id, user));




app.MapGet("/", () => "Are you lost?");



app.MapPost("/login", async (HttpContext context, ICrudService<User> service) =>
{
    var user = await context.Request.ReadFromJsonAsync<User>();
    var users = await service.ReadAsync();

    user = users
            .Where(x => x.Username == user.Username)
            .SingleOrDefault(x => x.Password == user.Password);

    if(user != null)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            //new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Role, "Add"),
            new Claim(ClaimTypes.Role, "ReadSingle")
        };


        var tokenDescriptior = new SecurityTokenDescriptor();
        tokenDescriptior.Subject = new System.Security.Claims.ClaimsIdentity(claims);
        tokenDescriptior.Expires = DateTime.Now.AddMinutes(5);
        tokenDescriptior.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptior);

        await context.Response.WriteAsync(tokenHandler.WriteToken(token));


        //        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var claimPrincipal = new ClaimsPrincipal(claimIdentity);
        //        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

    }
    else
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

});

app.Run();
