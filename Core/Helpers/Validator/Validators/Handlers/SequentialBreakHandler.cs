using Validators.Types;

namespace Validators.Handlers
{
    public class SequentialBreakHandler : ICheckListHandler
    {

        public async Task Run(IEnumerable<ICheckRule> rules)
        {
            foreach (var rule in rules)
            {
                await  rule.Run();
                if (rule.State == CheckRuleState.InValid)
                {
                    break;
                }
            }
             
        }
    }
}
