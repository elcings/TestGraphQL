using System;
using GraphiQl;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestGraphQL.Data;
using TestGraphQL.Mutations;
using TestGraphQL.Query;
using TestGraphQL.Types;

namespace TestGraphQL
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
            services.AddDbContext<ApplicationDbContext>(context =>
            {
                context.UseInMemoryDatabase("DbGraphQL");
            });
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<BookType>();
            services.AddSingleton<AuthorType>();
            services.AddSingleton<CustomBookModelType>();
            services.AddSingleton<IGraphQueryMarker, AuthorQuery>();
            services.AddSingleton<IGraphQueryMarker, BookQuery>();
            services.AddSingleton<CompositeQuery>();
            services.AddSingleton<AuthorInputType>();
            services.AddSingleton<IGraphMutationMaker,AuthorMutation>();
            services.AddSingleton<IGraphMutationMaker,BookMutation>();
            services.AddSingleton<CompositeMutation>();
            services.AddSingleton<ISchema, AuthorSchema>();
            services.AddGraphQL();
           
            services.AddControllers();
        }

        private static void AddTestData(ApplicationDbContext context)
        {
            var authorDbEntry = context.Authors.Add(
                  new Models.Author
                  {
                      Id=1,
                      Name = "First Author",
                  }
                );

            context.SaveChanges();

            context.Books.AddRange(
              new Models.Book
              {
                  Id=1,
                  Name = "First Book",
                  Published = true,
                  AuthorId = authorDbEntry.Entity.Id,
                  Genre = "Mystery"
              },
              new Models.Book
              {
                  Id = 2,
                  Name = "Second Book",
                  Published = true,
                  AuthorId = authorDbEntry.Entity.Id,
                  Genre = "Crime"
              }
            );

            context.SaveChanges();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGraphiQl("/graphql");
            var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            AddTestData(context);
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
