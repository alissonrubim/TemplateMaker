using SuperNiceProject.Models;
using SuperNiceProject.Repositories;
using SuperNiceProject.UseCases.Interfaces;

namespace SuperNiceProject.UseCases
{
    public class UpdateOrder : IUpdateOrder
    {
        private readonly IOrderRepository _OrderRepository;
        public UpdateOrder(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public void Update(long OrderId, OrderModel model)
        {
            _OrderRepository.Put(OrderId, model);
        }
    }
}
