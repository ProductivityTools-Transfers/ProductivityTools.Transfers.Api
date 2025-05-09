using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Logging;
using ProductivityTools.Transfers.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProductivityTools.Transfers.WebApi.Services;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string masterconfpath = Environment.GetEnvironmentVariable("MasterConfigurationPath");
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile($"{masterconfpath}\\ProductivityTools.Transfers.ServiceAccount.json"),
});
IdentityModelEventSource.ShowPII = true;
builder.Services
 .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.Authority = "https://securetoken.google.com/pttransfersprod";
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidIssuer = "https://securetoken.google.com/pttransfersprod",
         ValidateAudience = true,
         ValidAudience = "pttransfersprod",
         ValidateLifetime = true
     };
 });

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://localhost:3000", "https://transfersweb.z16.web.core.windows.net", "https://ptservicestatus-309299231472.us-central1.run.app").AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddScoped<TransferService>();
builder.Services.AddControllers();
builder.Services.ConfigureServicesDatabase();

var app = builder.Build();
app.UseRouting();//not sure if required
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
