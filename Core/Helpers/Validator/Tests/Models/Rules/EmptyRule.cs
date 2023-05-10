using Validators.Types;

namespace Validators.Test.Models.Rules
{
    public class EmptyRule:BaseTestRule,ICheckRule
    {
        public CheckRuleState State { get; set; }
        public Task Run()
        {
            return Task.Run(() =>
            {
                State = CheckRuleState.Valid;
                ValidationTime = DateTime.Now;
            });

        }
    }
}
