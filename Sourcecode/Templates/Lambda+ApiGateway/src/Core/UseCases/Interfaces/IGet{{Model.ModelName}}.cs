using {{SolutionName}}.Models;

namespace {{SolutionName}}.UseCases.Interfaces
{
    public interface IGet{{Model.ModelName}}
    {
        {{Model.ModelName}}Model Get(long {{Model.KeyField.Name}});
    }
}
