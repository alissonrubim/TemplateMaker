using {{SolutionName}}.Repositories;
using {{SolutionName}}.UseCases.Interfaces;

namespace {{SolutionName}}.UseCases
{
    public class Delete{{Model.ModelName}} : IDelete{{Model.ModelName}}
    {
        private readonly I{{Model.ModelName}}Repository _{{Model.ModelName}}Repository;
        public Delete{{Model.ModelName}}(I{{Model.ModelName}}Repository {{Model.ModelName}}Repository)
        {
            _{{Model.ModelName}}Repository = {{Model.ModelName}}Repository;
        }
        public void Delete(long {{Model.KeyField.Name}})
        {
            _{{Model.ModelName}}Repository.Delete({{Model.KeyField.Name}});
        }
    }
}
