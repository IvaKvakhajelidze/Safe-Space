using Microsoft.EntityFrameworkCore;
using SafeSpace.Application.Services;
using SafeSpace.Domain.Entities;
using SafeSpace.Infrastructure.Data;
using SafeSpace.Infrastructure.Services;

namespace SafeSpace.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddMemoryCache();  

            builder.Services.AddHttpClient<IProfanityFilterService, ProfanityFilterService>();
            builder.Services.AddHttpClient<IQuoteService, QuoteService>();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStoryService, StoryService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICommentLikeService, CommentLikeService>();

            builder.Services.AddSession();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
