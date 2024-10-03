namespace Miljoboven.Models
{
    public interface IErrandRepository
    {
        IEnumerable<Errand> GetErrands();
        Errand GetErrandById(string id);
    }

}
