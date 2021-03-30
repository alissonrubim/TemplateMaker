using SuperNiceProject.Models;
using SuperNiceProject.Repositories;
using SuperNiceProject.UseCases.Interfaces;

namespace SuperNiceProject.UseCases
{
    public class GetOrder : IGetOrder
    {
        private readonly IOrderRepository _OrderRepository;
        public GetOrder(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public OrderModel Get(long OrderId)
        {
            return _OrderRepository.Get(OrderId);
        }
    }
}
