using SuperNiceProject.Models;

namespace SuperNiceProject.UseCases.Interfaces
{
    public interface ICreateOrder
    {
        int Create(OrderModel model);
    }
}
