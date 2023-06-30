namespace HotelListingApi.Data.IRepository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext context;

        private IGenericRepo<Country>? countries;

        private IGenericRepo<Hotel>? hotels;


        public UnitOfWork(DatabaseContext context) => this.context = context;

        public IGenericRepo<Country> CountriesRepo => countries ??= new GenericRepo<Country>(context);

        public IGenericRepo<Hotel> HotelsRepo => hotels ??= new GenericRepo<Hotel>(context);


        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
