using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Query
{
    public class CompositeQuery : ObjectGraphType<object>
    {
        public CompositeQuery(IEnumerable<IGraphQueryMarker> graphQueryMarkers)
        {
            Name = "CompositeQuery";
            foreach (var marker in graphQueryMarkers)
            {
               if( marker is ObjectGraphType<object> a)
                foreach (var f in a.Fields)
                {
                    AddField(f);
                }
            }
        }
    }
}
