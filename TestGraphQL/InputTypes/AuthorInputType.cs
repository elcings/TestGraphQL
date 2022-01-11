using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL
{
    public class AuthorInputType: InputObjectGraphType
    {
        public AuthorInputType()
        {
            Name = "authorInput";
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
        }
    }
}
