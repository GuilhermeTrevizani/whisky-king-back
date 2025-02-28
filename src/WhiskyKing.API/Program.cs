using Microsoft.AspNetCore.Mvc;
using WhiskyKing.API.Extensions;
using WhiskyKing.API.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSettings(builder);
builder.Services.AddAuthenticationJwt(builder);
builder.Host.AddAutofac();
builder.Services.AddDatabase(builder);
builder.Services.AddPolicy();
builder.Services.AddSwagger();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCors(x =>
    x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        x.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Services.CreateDatabase();

app.Run();