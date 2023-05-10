namespace Validators.Types
{
    public interface ICheckListHandler
    {
        Task Run(IEnumerable<ICheckRule> rules);
    }
}
