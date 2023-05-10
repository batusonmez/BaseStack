namespace Validators.Types
{
    public interface ICheckList
    {
        bool Valid { get; }
        IEnumerable<ICheckRule> Rules { get; set; }
    }
}
