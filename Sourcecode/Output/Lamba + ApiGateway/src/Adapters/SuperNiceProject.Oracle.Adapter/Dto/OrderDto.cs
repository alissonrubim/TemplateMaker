using SuperNiceProject.Oracle.Adapter.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using SuperNiceProject.Models;

namespace SuperNiceProject.Oracle.Adapter.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public char Name { get; set; } 
        public char Test { get; set; } 

        public static OrderDto FromModel(OrderModel model)
        {
            return new OrderDto {
                OrderId = model.OrderId,
                Name = model.Name,
                Test = model.Test
            };
        }

        public OrderModel ToModel()
        {
            return new OrderModel {
                OrderId = OrderId,
                Name = Name,
                Test = Test
            };
        }
    }
}
