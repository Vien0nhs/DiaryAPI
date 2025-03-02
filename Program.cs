using DiaryAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DiaryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DiaryConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello from Railway!");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(errorApp =>
{
	errorApp.Run(async context =>
	{
		context.Response.StatusCode = 500;
		context.Response.ContentType = "application/json";

		var errorFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
		if (errorFeature != null)
		{
			var exception = errorFeature.Error;
			var errorResponse = new
			{
				StatusCode = 500,
				Message = "Internal Server Error",
				Detail = exception.Message, // Trả về thông điệp lỗi chi tiết
				StackTrace = exception.StackTrace // Trả về stack trace để debug
			};
			await context.Response.WriteAsJsonAsync(errorResponse);
		}
	});
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
