namespace Validators.Types
{
    public interface ICheckRule
    {
        CheckRuleState State { get; set; }
        Task Run() ;  
    }
}
