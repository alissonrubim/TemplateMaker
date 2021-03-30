using SuperNiceProject.Models;

namespace SuperNiceProject.UseCases.Interfaces
{
    public interface IUpdateOrder
    {
        void Update(long OrderId, OrderModel model);
    }
}
