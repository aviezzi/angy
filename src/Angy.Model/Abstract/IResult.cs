namespace Angy.Model.Abstract
{
    public interface IResult<out TValue, out TError>
    {
        TValue Success { get; }
        TError Error { get; }
    }
}