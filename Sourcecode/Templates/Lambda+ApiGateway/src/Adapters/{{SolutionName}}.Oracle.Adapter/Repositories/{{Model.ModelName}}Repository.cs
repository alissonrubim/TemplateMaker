using System.Data;
using System.Collections.Generic;
using Coolblue.Utilities.Data.Timing;
using Coolblue.Utilities.Resilience.Oracle.Core;
using Dapper;
using {{SolutionName}}.Models;
using {{SolutionName}}.Oracle.Adapter.Dto;
using {{SolutionName}}.Repositories;
using System.Linq;

namespace {{SolutionName}}.Oracle.Adapter.Repositories
{
    public class {{Model.ModelName}}Repository : AbstractOracleRepository, I{{Model.ModelName}}Repository
    {
        private const string MonitoringGetAll = "MonitoringGetAll";
        private const string MonitoringGet = "MonitoringGet";
        private const string MonitoringPost = "MonitoringPost";
        private const string MonitoringPut = "MonitoringPut";
        private const string MonitoringDelete = "MonitoringDelete";

        private const string GetAllQuery =
           @"select
                {{UpperCase Model.KeyField.Name}},
            {{#each Model.Fields}}
                P_{{UpperCase Name}}{{#unless @last}},{{/unless}}
            {{/each}}
            from table({{PackageName}}.GETALL())";

        private const string GetQuery =
           @"select
                {{UpperCase Model.KeyField.Name}},
            {{#each Model.Fields}}
                P_{{UpperCase Name}}{{#unless @last}},{{/unless}}
            {{/each}}
            from table({{PackageName}}.GET(:P_{{UpperCase Model.KeyField.Name}}))";

        private const string PostQuery =
             @"begin
                {{PackageName}}.POST(
                {{#each Model.Fields}}
                    :P_{{UpperCase Name}}{{#unless @last}},{{/unless}}
                {{/each}}
                    :P_{{UpperCase Model.KeyField.Name}})
              end;";
        private const string PutQuery =
            @"begin
                {{PackageName}}.PUT(
                {{#each Model.Fields}}
                    :P_{{UpperCase Name}}{{#unless @last}},{{/unless}}
                {{/each}}); 
              end;";

        private const string DeleteQuery =
           @"begin
                {{PackageName}}.DELETE(:P_{{UpperCase Model.KeyField.Name}}); 
              end;";

        public {{Model.ModelName}}Repository(ITimingDbConnectionFactory connectionFactory, OracleResiliencePolicy resiliencePolicy) : base(
           connectionFactory, resiliencePolicy)
        {

        }

        public void Delete(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_{{UpperCase Model.KeyField.Name}}", Id);

            ExecuteWithPolicy(() => {
                return Connection.Execute("DeleteQuery", DeleteQuery, parameters);
            }, MonitoringDelete);
        }

        public {{Model.ModelName}}Model Get(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_{{UpperCase Model.KeyField.Name}}", Id);

            return ExecuteWithPolicy(() => {
                Connection.Open();
                var dto = Connection.QueryFirst<{{Model.ModelName}}Dto>("GetQuery", GetQuery, parameters);
                return dto.ToModel();
            }, MonitoringGet);
        }

        public IEnumerable<{{Model.ModelName}}Model> GetAll()
        {
            return ExecuteWithPolicy(() => {
                Connection.Open();
                var dto = Connection.Query<{{Model.ModelName}}Dto>("GetAllQuery", GetAllQuery);
                return from item in dto
                       select item.ToModel();
            }, MonitoringGetAll);
        }

        public int Post({{Model.ModelName}}Model model)
        {
            var parameters = new DynamicParameters();
            {{#each Model.Fields}}
                parameters.Add("P_{{UpperCase Name}}", model.{{Name}});
            {{/each}}
            parameters.Add("P_{{ UpperCase Model.KeyField.Name}}", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return ExecuteWithPolicy(() => {
                Connection.Execute("PostQuery", PostQuery, parameters);
                return parameters.Get<int>("P_{{UpperCase Model.KeyField.Name}}");
            }, MonitoringPost);
        }

        public void Put(long Id, {{Model.ModelName}}Model model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_{{UpperCase Model.KeyField.Name}}", Id);
            {{#each Model.Fields}}
                parameters.Add("P_{{UpperCase Name}}", model.{{Name}});
            {{/each}}

            ExecuteWithPolicy(() => {
                return Connection.Execute("PutQuery", PutQuery, parameters);
            }, MonitoringPut);
        }
    }
}
