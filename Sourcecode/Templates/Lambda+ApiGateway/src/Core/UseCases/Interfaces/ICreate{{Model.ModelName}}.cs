using {{SolutionName}}.Models;

namespace {{SolutionName}}.UseCases.Interfaces
{
    public interface ICreate{{Model.ModelName}}
    {
        int Create({{Model.ModelName}}Model model);
    }
}
