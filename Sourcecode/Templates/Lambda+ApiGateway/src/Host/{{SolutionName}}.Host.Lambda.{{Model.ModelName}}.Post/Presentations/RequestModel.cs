using System;
using {{SolutionName}}.Models;
using Lambda.Base.Exceptions;
using Lambda.Base.Presentations;

namespace {{SolutionName}}.Host.Lambda.{{Model.ModelName}}.Post.Presentations
{
    public class RequestModel: IRequestModel
    {
    {{#each Model.Fields}}
        public {{OracleToCSharp Type}} {{Name}} { get; set; } 
    {{/each}}

        public {{Model.ModelName}}Model ToModel()
        {
            return new {{Model.ModelName}}Model {
            {{#each Model.Fields}}
                {{Name}} = {{Name}}{{#unless @last}},{{/unless}}
            {{/each}}
            };
        }

        public void Validate()
        {
            /*if (OrderId <= 0 && InvoiceId <= 0)
                throw new InvalidRequestException("At least one field (OrderId or InvoiceId) should be setted.");*/
        }
    }
}
