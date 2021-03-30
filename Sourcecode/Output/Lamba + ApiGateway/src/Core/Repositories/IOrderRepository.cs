using SuperNiceProject.Models;

namespace SuperNiceProject.Repositories
{
    public interface IOrderRepository
    {
        OrderModel Get(long OrderId);
        int Post(OrderModel model);
        void Put(long OrderId, OrderModel model);
        void Delete(long OrderId);
    }
}
