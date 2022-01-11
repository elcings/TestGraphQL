using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using TestGraphQL.Query;

namespace TestGraphQL.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public WeatherForecastController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        public async Task<IActionResult> Post([FromBody] GraphlQlQuery query)
        {
            var inputs = query.Variables?.ToInputs();

            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                OperationName=query.OperationName,
                Inputs = inputs
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions);


            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

    }
        
    
}
