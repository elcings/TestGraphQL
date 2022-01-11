using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Models;

namespace TestGraphQL
{
    public class BookType:ObjectGraphType<Book>
    {
        public BookType()
        {
            Name = "Book";
            Field(x => x.Id).Description("Book id");
            Field(x => x.Name).Description("Book name");
            Field(x => x.Genre).Description("Book genre");
            Field(x => x.Published).Description("If the book is published or not");
            Field(x => x.AuthorId).Description("AuthorId");
        }
    }
}
