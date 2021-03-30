using {{SolutionName}}.Models;

namespace {{SolutionName}}.UseCases.Interfaces
{
    public interface IUpdate{{Model.ModelName}}
    {
        void Update(long {{Model.KeyField.Name}}, {{Model.ModelName}}Model model);
    }
}
