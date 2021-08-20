
using AutoMapper;
using BookManagementModels.Entities;
using Business.Book;
using Business.Book.DTO.Maps;
using Elasticsearch.Net;
using Indexer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork;
using UOW;

namespace Api.BookManagement
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
            services.Configure<Configuration>(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book API",
                    Description = "A service example 'Base Stack' template",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Engin Özdemir",
                        Email = "xenamorphx@gmail.com",
                        Url = new Uri("https://github.com/diwsi"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT license",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
            });

            //Map Depenecies 
            services.AddTransient<IBookBusiness, BookBusiness>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>(sp =>
            {
                var context = new BookManagementContext(Configuration["ConnectionString"]);
                return new EFUnitOfWork(context);
            });
            AddIndexer(services);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Document Indexer Client
        /// </summary>
        /// <param name="services"></param>
        void AddIndexer(IServiceCollection services)
        {
            var url = Configuration["elasticsearch:url"]; 
              
            var settings = new ConnectionConfiguration(new Uri(url));

            var client = new ElasticLowLevelClient(settings);
             
            var indexer = new ElasticSearchIndexer(client);

            services.AddSingleton<IIndexer>(indexer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comment API V1");
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
