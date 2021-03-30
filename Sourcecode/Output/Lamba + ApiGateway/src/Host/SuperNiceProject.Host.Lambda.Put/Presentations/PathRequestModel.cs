using System;
using SuperNiceProject.Models;
using Lambda.Base.Exceptions;
using Lambda.Base.Presentations;

namespace SuperNiceProject.Host.Lambda.Put.Presentations
{
    public class PathRequestModel: IRequestModel
    {
        public long OrderId { get; set; }
       
        public void Validate()
        {
            if (OrderId <= 0)
                throw new InvalidRequestException("Field OrderId invalid or not defined");
        }
    }
}
