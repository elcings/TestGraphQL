using GraphQL.Types;
using GraphQL.Utilities;
using System;
using TestGraphQL.Mutations;
using TestGraphQL.Query;

namespace TestGraphQL
{
    public class AuthorSchema : Schema
    {
        public AuthorSchema(IServiceProvider resolver):base(resolver)
        {
            Query = resolver.GetRequiredService<CompositeQuery>();
            Mutation = resolver.GetRequiredService<CompositeMutation>();
        }
    }
}
