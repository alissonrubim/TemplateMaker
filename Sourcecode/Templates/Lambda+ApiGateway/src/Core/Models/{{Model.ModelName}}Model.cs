using System;

namespace {{SolutionName}}.Models
{
    public class {{Model.ModelName}}Model
    {
        public long {{Model.KeyField.Name}} { get; set; }
    {{#each Model.Fields}}
        public {{OracleToCSharp Type}} {{Name}} { get; set; } 
    {{/each}}
    }

}
