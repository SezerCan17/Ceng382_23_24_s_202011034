using Lab10.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Hizmetlerin koleksiyonunu al
var services = builder.Services;

// Hizmetleri koleksiyona ekle
services.AddRazorPages();

// Veritabanı bağlantısını eklerken bu hizmet koleksiyonunu kullan
services.AddDbContext<Lab10Context>(options =>
    options.UseSqlServer("DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
