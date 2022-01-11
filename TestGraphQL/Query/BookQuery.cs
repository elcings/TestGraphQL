using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Data;
using TestGraphQL.Models;
using TestGraphQL.Types;

namespace TestGraphQL.Query
{
    public class BookQuery:ObjectGraphType<object>, IGraphQueryMarker
    {
        public BookQuery(ApplicationDbContext db)
        {
            Field<BookType>("book",
              arguments: new QueryArguments(new QueryArgument<IdGraphType>() { Name = "id" }),
              resolve: context => {
                  var id = context.GetArgument<int>("id");
                  var book = db.Books.Find(id); ;
                  return book;
              });
            Field<CustomBookModelType>("books",
                resolve: context => {
                    var books = db.Books;
                    return new { ListBook = books, Count = books.Count(), IsSucced = true };
                });

        }
    }
}
