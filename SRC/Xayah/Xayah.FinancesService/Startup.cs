using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xayah.Data;
using Xayah.Impl;
using Xayah.Model;
using Xayah.Model.Interfaces;

namespace Xayah.FinancesService
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<TransferContext>(opt =>
								opt.UseSqlServer(Configuration.GetConnectionString("TransferContext")));

			services.AddTransient<IRepository<BankTransfer>, TransferRepository>();
			services.AddTransient<ICheckTransfer, TransferRepository>();
			services.AddTransient<ITransferContext, TransferContext>();
			services.AddTransient<IOfxReader, OfxReader>();
			services.AddTransient<IConciliateService, ConciliateService>();


			services.AddMvc(opt => {
				opt.OutputFormatters.RemoveType<StringOutputFormatter>();
				opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
			});

			services.AddMvc()
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.Formatting = Formatting.Indented;
				});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
