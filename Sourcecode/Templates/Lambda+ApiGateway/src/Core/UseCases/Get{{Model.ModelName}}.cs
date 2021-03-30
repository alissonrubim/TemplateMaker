using {{SolutionName}}.Models;
using {{SolutionName}}.Repositories;
using {{SolutionName}}.UseCases.Interfaces;

namespace {{SolutionName}}.UseCases
{
    public class Get{{Model.ModelName}} : IGet{{Model.ModelName}}
    {
        private readonly I{{Model.ModelName}}Repository _{{Model.ModelName}}Repository;
        public Get{{Model.ModelName}}(I{{Model.ModelName}}Repository {{Model.ModelName}}Repository)
        {
            _{{Model.ModelName}}Repository = {{Model.ModelName}}Repository;
        }

        public {{Model.ModelName}}Model Get(long {{Model.KeyField.Name}})
        {
            return _{{Model.ModelName}}Repository.Get({{Model.KeyField.Name}});
        }
    }
}
