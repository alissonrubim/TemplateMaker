using {{SolutionName}}.Models;
using System.Collections.Generic;

namespace {{SolutionName}}.UseCases.Interfaces
{
    public interface IGetAll{{Model.ModelName}}
    {
        IEnumerable<{{Model.ModelName}}Model> GetAll();
    }
}
