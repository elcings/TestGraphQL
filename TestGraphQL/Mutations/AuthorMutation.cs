using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Data;
using TestGraphQL.Models;
using TestGraphQL.Mutations;

namespace TestGraphQL
{
    public class AuthorMutation : ObjectGraphType, IGraphMutationMaker
    {
        public AuthorMutation(ApplicationDbContext db)
        {
            Field<AuthorType>("createAuthor",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "author" }),
                resolve: context =>
                 {
                     var human = context.GetArgument<Author>("author");
                     db.Authors.Add(human);
                     db.SaveChanges();
                     return human;
                 });


            Field<StringGraphType>("deleteAuthor",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "authorId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("authorId");
                    var author = db.Authors.Find(id);
                    db.Remove(author);
                    db.SaveChanges();
                    return $"Test{id}";
                });

            Field<AuthorType>("updateAuthor",
               arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AuthorInputType>> { Name = "author" },
                   new QueryArgument<IdGraphType> { Name = "authorId" }),
               resolve: context =>
               {
                   var author = context.GetArgument<Author>("author");
                   var id = context.GetArgument<int>("authorId");
                   var authordb = db.Authors.Find(id);
                   if (authordb == null)
                   {
                       context.Errors.Add(new ExecutionError("data not found"));
                       return null;

                   }
                   authordb.Name = author.Name;
                   db.SaveChanges();
                   return authordb;
               });

        }
    }
}
