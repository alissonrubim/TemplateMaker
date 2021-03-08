using System.Data;
using Coolblue.Utilities.Data.Timing;
using Coolblue.Utilities.Resilience.Oracle.Core;
using Dapper;
using {{SolutionName}}.Models;
using {{SolutionName}}.Oracle.Adapter.Dto;
using {{SolutionName}}.Oracle.Adapter.Helpers;
using {{SolutionName}}.Repositories;

namespace {{SolutionName}}.Oracle.Adapter.Repositories
{
    public class {{ModelName}}Repository : AbstractOracleRepository, I{{ModelName}}Repository
    {
        private const string MonitoringGet = "MonitoringGet";
        private const string MonitoringPost = "MonitoringPost";
        private const string MonitoringPut = "MonitoringPut";
        private const string MonitoringDelete = "MonitoringDelete";

        private const string GetQuery =
           @"select
                INVITETOPAYID,
                UNIQUEREFERENCE
            from table(VAN_PKG_INVITETOPAY.GET(:P_ID))";

        private const string PostQuery =
             @"begin
                VAN_PKG_INVITETOPAY.PUT(
                    :P_UNIQUEREFERENCE,
                    :P_ID)
              end;";
        private const string PutQuery =
            @"begin
                VAN_PKG_INVITETOPAY.PUT(
                    :P_UNIQUEREFERENCE); 
              end;";

        private const string DeleteQuery =
           @"begin
                VAN_PKG_INVITETOPAY.DELETE(:P_ID); 
              end;";

        public {{ModelName}}Repository(ITimingDbConnectionFactory connectionFactory, OracleResiliencePolicy resiliencePolicy) : base(
           connectionFactory, resiliencePolicy)
        {

        }

        public void Delete(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_ID", Id);

            ExecuteWithPolicy(() => {
                return Connection.Execute("DeleteQuery", DeleteQuery, parameters);
            }, MonitoringDelete);
        }

        public {{ModelName}}Model Get(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_ID", Id);

            return ExecuteWithPolicy(() => {
                Connection.Open();
                var dto = Connection.QueryFirst<{{ModelName}}Dto>("GetQuery", GetQuery, parameters);
                return dto.ToModel();
            }, MonitoringGet);
        }

        public int Post({{ModelName}}Model model)
        {
            var parameters = new DynamicParameters();
            //parameters.Add("P_UNIQUEREFERENCE", model.UniqueReference);
            parameters.Add("P_ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return ExecuteWithPolicy(() => {
                Connection.Execute("PostQuery", PostQuery, parameters);
                return parameters.Get<int>("P_ID");
            }, MonitoringPost);
        }

        public void Put(long Id, {{ModelName}}Model model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_ID", Id);
            //parameters.Add("P_UNIQUEREFERENCE", model.UniqueReference);

            ExecuteWithPolicy(() => {
                return Connection.Execute("PutQuery", PutQuery, parameters);
            }, MonitoringPut);
        }
    }
}
