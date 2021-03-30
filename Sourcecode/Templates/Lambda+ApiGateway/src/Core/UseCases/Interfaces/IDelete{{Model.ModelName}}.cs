namespace {{SolutionName}}.UseCases.Interfaces
{
    public interface IDelete{{Model.ModelName}}
    {
        void Delete(long {{Model.KeyField.Name}});
    }
}
