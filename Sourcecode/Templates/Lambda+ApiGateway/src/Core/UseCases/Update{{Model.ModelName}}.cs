using {{SolutionName}}.Models;
using {{SolutionName}}.Repositories;
using {{SolutionName}}.UseCases.Interfaces;

namespace {{SolutionName}}.UseCases
{
    public class Update{{Model.ModelName}} : IUpdate{{Model.ModelName}}
    {
        private readonly I{{Model.ModelName}}Repository _{{Model.ModelName}}Repository;
        public Update{{Model.ModelName}}(I{{Model.ModelName}}Repository {{Model.ModelName}}Repository)
        {
            _{{Model.ModelName}}Repository = {{Model.ModelName}}Repository;
        }

        public void Update(long {{Model.KeyField.Name}}, {{Model.ModelName}}Model model)
        {
            _{{Model.ModelName}}Repository.Put({{Model.KeyField.Name}}, model);
        }
    }
}
