namespace HotelListingApi.Data
{
    public class QueryParams
    {
        const byte MaxPageSize = 50;

        public byte PageNumber { get; set; } = 1;

        private byte _pageSize = 10;

        public byte PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize || value <= 0 ? MaxPageSize : value);
        }

    }
}
