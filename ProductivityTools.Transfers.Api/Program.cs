using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Logging;
using ProductivityTools.Transfers.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens; 

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





builder.Services.AddControllers();
builder.Services.ConfigureServicesDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
