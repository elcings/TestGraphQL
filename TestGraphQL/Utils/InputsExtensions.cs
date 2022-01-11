using GraphQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL
{
    public static class InputsExtensions
    {
        public static Inputs ToInputs(this JObject obj)
        {
            return new Inputs(obj.ToObject<Dictionary<string,object>>());
        }
    }
}
