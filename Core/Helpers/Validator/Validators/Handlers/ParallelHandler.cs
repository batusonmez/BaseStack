using Validators.Types;

namespace Validators.Handlers
{
    public class ParallelHandler: ICheckListHandler
    {

        public Task Run(IEnumerable<ICheckRule> rules)
        {
            
           return Task.WhenAll(rules.Select(d => d.Run()));
        }
    }
}
