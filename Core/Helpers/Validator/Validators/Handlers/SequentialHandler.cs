using Validators.Types;

namespace Validators.Handlers
{
    public class SequentialHandler : ICheckListHandler
    {

        public   async Task Run(IEnumerable<ICheckRule> rules)
        {
            foreach (var rule in rules)
            {
                await  rule.Run();
            }
             
        }
    }
}
