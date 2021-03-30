using {{SolutionName}}.Oracle.Adapter.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using {{SolutionName}}.Models;

namespace {{SolutionName}}.Oracle.Adapter.Dto
{
    public class {{Model.ModelName}}Dto
    {
        public long {{Model.KeyField.Name}} { get; set; }
    {{#each Model.Fields}}
        public {{OracleToCSharp Type}} {{Name}} { get; set; } 
    {{/each}}

        public static {{Model.ModelName}}Dto FromModel({{Model.ModelName}}Model model)
        {
            return new {{Model.ModelName}}Dto {
                {{Model.KeyField.Name}} = model.{{Model.KeyField.Name}},
            {{#each Model.Fields}}
                {{Name}} = model.{{Name}}{{#unless @last}},{{/unless}}
            {{/each}}
            };
        }

        public {{Model.ModelName}}Model ToModel()
        {
            return new {{Model.ModelName}}Model {
                {{Model.KeyField.Name}} = {{Model.KeyField.Name}},
            {{#each Model.Fields}}
                {{Name}} = {{Name}}{{#unless @last}},{{/unless}}
            {{/each}}
            };
        }
    }
}
