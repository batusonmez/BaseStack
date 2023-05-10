using Validators.Types;

namespace Validators.Test.Models.Rules
{
    public class MustBeCheap : BaseTestRule, ICheckRule
    {
        private readonly Car car;
        private readonly int averageValue;

        public MustBeCheap(Car car, int averageValue)
        {
            this.car = car;
            this.averageValue = averageValue;
        }
        public CheckRuleState State { get; set; }

        public Task Run()
        {
            return Task.Run(() =>
            {
                if (car.Price < averageValue)
                {
                    State = CheckRuleState.Valid;
                }
                else
                {
                    State = CheckRuleState.Valid;
                };
                ValidationTime = DateTime.Now;
            });
        }
    }
}
