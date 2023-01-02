using Startup_B.Models.Stock;

namespace Startup_B.Services.stock
{
    public interface IStock
    {
        public Task<List<StockModel>> ReadExcelAsync(string fileName);
    }
}
