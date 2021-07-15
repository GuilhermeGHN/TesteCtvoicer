using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TesteCtvoicer.Data;
using TesteCtvoicer.Entities;
using TesteCtvoicer.Models;
using TesteCtvoicer.Repositories;
using TesteCtvoicer.Services;

namespace TesteCtvoicer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Veiculo, VeiculoViewModel>();
				cfg.CreateMap<VeiculoViewModel, Veiculo>();
			});

			var mapper = config.CreateMapper();
			services.AddSingleton(mapper);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
			builder.RegisterAssemblyModules(typeof(FrotaContextModule).Assembly);
			builder.RegisterAssemblyModules(typeof(VeiculoRepositoryModule).Assembly);
			builder.RegisterAssemblyModules(typeof(VeiculoServiceModule).Assembly);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
