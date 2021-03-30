using SuperNiceProject.Models;

namespace SuperNiceProject.UseCases.Interfaces
{
    public interface IGetOrder
    {
        OrderModel Get(long OrderId);
    }
}
