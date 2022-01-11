using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Data;

namespace TestGraphQL.Mutations
{
    public class BookMutation:ObjectGraphType, IGraphMutationMaker
    {
        public BookMutation(ApplicationDbContext db)
        {
            Field<StringGraphType>("deleteBook",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "bookId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("bookId");
                    var book = db.Books.Find(id);
                    if (book == null)
                    {
                        context.Errors.Add(new ExecutionError("book not found"));
                        return "Error";
                    }
                    db.Remove(book);
                    db.SaveChanges();
                    return $"deleted book id={id}";
                });
        }
    }
}
