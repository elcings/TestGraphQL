using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Data;
using TestGraphQL.Models;
using TestGraphQL.Types;

namespace TestGraphQL.Query
{
    public class AuthorQuery : ObjectGraphType<object>, IGraphQueryMarker
    {
        public AuthorQuery(ApplicationDbContext db)
        {
            Field<AuthorType>(
              "author",
              arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the Author." }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  var author = db
              .Authors
              .Include(x => x.Books)
              .FirstOrDefault(i => i.Id == id);
                  return author;
              });

            Field<ListGraphType<AuthorType>>(
              "authors",
              resolve: context =>
              {
                  var authors = db.Authors.Include(x => x.Books);
                  return authors;
              });
        }
    }
}
