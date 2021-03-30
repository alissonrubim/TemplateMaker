using SuperNiceProject.Repositories;
using SuperNiceProject.UseCases.Interfaces;

namespace SuperNiceProject.UseCases
{
    public class DeleteOrder : IDeleteOrder
    {
        private readonly IOrderRepository _OrderRepository;
        public DeleteOrder(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }
        public void Delete(long OrderId)
        {
            _OrderRepository.Delete(OrderId);
        }
    }
}
