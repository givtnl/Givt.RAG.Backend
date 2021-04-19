using System.Collections.Generic;
using Amazon.DynamoDBv2;
using backend.business.Backers.Queries.GetList;
using backend.business.Events.Mappers;
using backend.business.Infrastructure;
using backend.business.Participants.Commands.Register;
using backend.Filters;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace backend
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

            services.AddControllers();
            services.AddMvc(x => x.Filters.Add<ExceptionFilter>());


            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddAutoMapper(x => x.AddMaps(typeof(EventMapper).Assembly));
            
            // I dont think you can change the order in this one
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(HackatonPipelineBehavior<,>));
            services.AddMediatR(typeof(GetBackersListQuery).Assembly);

            services.AddValidatorsFromAssembly(typeof(RegisterParticipantCommand).Assembly);

            services.AddOpenApiDocument(options =>
            {
                options.Title = "Gehaktaton API";
                options.AllowNullableBodyParameters = false;
                options.DocumentName = "v1";
                options.PostProcess = document =>
                {
                    document.Produces = new List<string>
                    {
                        "application/json"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
