using SuperNiceProject.Models;
using SuperNiceProject.Repositories;
using SuperNiceProject.UseCases.Interfaces;

namespace SuperNiceProject.UseCases
{
    public class CreateOrder : ICreateOrder
    {
        private readonly IOrderRepository _OrderRepository;
        public CreateOrder(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public int Create(OrderModel model)
        {
            return _OrderRepository.Post(model);
        }
    }
}
