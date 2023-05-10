using Validators.Types;

namespace Validators
{
    public class CheckList : ICheckList
    {
        private readonly ICheckListHandler handler;

        public IEnumerable<ICheckRule> Rules { get; set; }
        public CheckList(ICheckListHandler handler)
        {
            this.handler = handler;
            Rules = new List<ICheckRule>();
        }

        public Task Run()
        {
            return handler.Run(Rules);
        }

        public bool Valid
        {
            get
            {
                return Rules.All(d => d.State == CheckRuleState.Valid);
            }
        }


    }
}
