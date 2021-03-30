using {{SolutionName}}.Models;
using {{SolutionName}}.Repositories;
using {{SolutionName}}.UseCases.Interfaces;
using System.Collections.Generic;

namespace {{SolutionName}}.UseCases
{
    public class GetAll{{Model.ModelName}} : IGetAll{{Model.ModelName}}
    {
        private readonly I{{Model.ModelName}}Repository _{{Model.ModelName}}Repository;

        public GetAll{{Model.ModelName}}(I{{Model.ModelName}}Repository {{Model.ModelName}}Repository)
        {
            _{{Model.ModelName}}Repository = {{Model.ModelName}}Repository;
        }

        public IEnumerable<{{Model.ModelName}}Model> GetAll()
        {
            return _{{Model.ModelName}}Repository.GetAll();
        }
    }
}
