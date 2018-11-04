using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunch.Data;
using Lunch.Menu;
using Lunch.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lunch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Settings.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IMenuProvider menuProvider;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                menuProvider = new KafetariaMenuProvider();
            }
            else
            {
                app.UseHsts();
                menuProvider = new KafetariaMenuProvider();
            }

            new MenuService()
                .SetMenuProvider(menuProvider)
                .SetSearchEngine(new MenuSearchEngine());

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
