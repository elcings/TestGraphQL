using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Models;

namespace TestGraphQL
{
    public class AuthorType:ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Name = "Author";
            Field(x => x.Id).Description("Author id");
            Field(x => x.Name).Description("Author name");
            Field(x => x.Books, type: typeof(ListGraphType<BookType>)).Description("Author's books");
        }
    }
}
