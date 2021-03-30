using {{SolutionName}}.Models;
using {{SolutionName}}.Repositories;
using {{SolutionName}}.UseCases.Interfaces;

namespace {{SolutionName}}.UseCases
{
    public class Create{{Model.ModelName}} : ICreate{{Model.ModelName}}
    {
        private readonly I{{Model.ModelName}}Repository _{{Model.ModelName}}Repository;
        public Create{{Model.ModelName}}(I{{Model.ModelName}}Repository {{Model.ModelName}}Repository)
        {
            _{{Model.ModelName}}Repository = {{Model.ModelName}}Repository;
        }

        public int Create({{Model.ModelName}}Model model)
        {
            return _{{Model.ModelName}}Repository.Post(model);
        }
    }
}
