using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Models;

namespace TestGraphQL.Types
{
    public class CustomBookModelType:ObjectGraphType<object>
    {
        public CustomBookModelType()
        {
            Name = "customBook";
            Field(typeof(ListGraphType<BookType>),"ListBook");
            Field(typeof(IntGraphType),"Count");
            Field(typeof(NonNullGraphType<StringGraphType>), "Error");
            Field(typeof(BooleanGraphType), "IsSucced");
        }
    }
}
