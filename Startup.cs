using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using EmployeeAuthenticationWebAPI.Model;
using Microsoft.EntityFrameworkCore;
using EmployeeAuthenticationWebAPI;
using EmployeeAuthenticationWebAPI.Controllers;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace EmployeeAuthenticationSystem
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public void AddService(IServiceCollection services)
		{
			services.AddSingleton<IDbUtil, DbUtil>();
			services.AddScoped<DepartmentController>();
			services.AddScoped<EmployeeController>();
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//Enable the CORS
			services.AddCors(c =>
			{
				c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			});
			services.AddControllersWithViews()
				.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
				.AddNewtonsoftJson(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.SerializerSettings.ContractResolver = new DefaultContractResolver());
			services.AddControllers();
			AddService(services);
			// Setup DB 
			//services.AddDbContext<Department>(options => 
			//	options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider
				(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
				RequestPath = "/Photos"
			});
		}
	}
}
