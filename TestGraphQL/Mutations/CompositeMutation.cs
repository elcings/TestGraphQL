using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Mutations
{
    public class CompositeMutation:ObjectGraphType
    {
        public CompositeMutation(IEnumerable<IGraphMutationMaker> graphMutationMakers)
        {
            Name = "CompositeMutation";
            foreach (var marker in graphMutationMakers)
            {
                if (marker is ObjectGraphType a)
                    foreach (var f in a.Fields)
                    {
                        AddField(f);
                    }
            }
        }
    }
}
