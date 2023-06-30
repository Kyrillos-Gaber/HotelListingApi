namespace HotelListingApi.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<Country> CountriesRepo { get; }
        
        IGenericRepo<Hotel> HotelsRepo { get; }

        Task Save();
    }
}
