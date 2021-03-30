using System.Data;
using Coolblue.Utilities.Data.Timing;
using Coolblue.Utilities.Resilience.Oracle.Core;
using Dapper;
using SuperNiceProject.Models;
using SuperNiceProject.Oracle.Adapter.Dto;
using SuperNiceProject.Oracle.Adapter.Helpers;
using SuperNiceProject.Repositories;

namespace SuperNiceProject.Oracle.Adapter.Repositories
{
    public class OrderRepository : AbstractOracleRepository, IOrderRepository
    {
        private const string MonitoringGet = "MonitoringGet";
        private const string MonitoringPost = "MonitoringPost";
        private const string MonitoringPut = "MonitoringPut";
        private const string MonitoringDelete = "MonitoringDelete";

        private const string GetQuery =
           @"select
                INVITETOPAYID,
                P_NAME,
                P_TEST
            from table(VAN_PKG_INVITETOPAY.GET(:P_ORDERID))";

        private const string PostQuery =
             @"begin
                VAN_PKG_INVITETOPAY.PUT(
                    :P_NAME,
                    :P_TEST
                    :P_ORDERID)
              end;";
        private const string PutQuery =
            @"begin
                VAN_PKG_INVITETOPAY.PUT(
                    :P_NAME,
                                    :P_TEST
                ); 
              end;";

        private const string DeleteQuery =
           @"begin
                VAN_PKG_INVITETOPAY.DELETE(:P_ORDERID); 
              end;";

        public OrderRepository(ITimingDbConnectionFactory connectionFactory, OracleResiliencePolicy resiliencePolicy) : base(
           connectionFactory, resiliencePolicy)
        {

        }

        public void Delete(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_ORDERID", Id);

            ExecuteWithPolicy(() => {
                return Connection.Execute("DeleteQuery", DeleteQuery, parameters);
            }, MonitoringDelete);
        }

        public OrderModel Get(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_ORDERID", Id);

            return ExecuteWithPolicy(() => {
                Connection.Open();
                var dto = Connection.QueryFirst<OrderDto>("GetQuery", GetQuery, parameters);
                return dto.ToModel();
            }, MonitoringGet);
        }

        public int Post(OrderModel model)
        {
            var parameters = new DynamicParameters();
                parameters.Add("P_NAME", model.Name);
                parameters.Add("P_TEST", model.Test);
            parameters.Add("P_ORDERID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return ExecuteWithPolicy(() => {
                Connection.Execute("PostQuery", PostQuery, parameters);
                return parameters.Get<int>("P_ORDERID");
            }, MonitoringPost);
        }

        public void Put(long Id, OrderModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("P_ORDERID", Id);
                parameters.Add("P_NAME", model.Name);
                parameters.Add("P_TEST", model.Test);

            ExecuteWithPolicy(() => {
                return Connection.Execute("PutQuery", PutQuery, parameters);
            }, MonitoringPut);
        }
    }
}
