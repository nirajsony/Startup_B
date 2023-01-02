using ExcelDataReader;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Startup_B.Models.Stock;
using Startup_B.Services.Exception;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;

namespace Startup_B.Services.stock
{
    public class Stock : IStock
    {
        private IExceptionLog exceptionLog;
        public Stock(IExceptionLog _exceptionLog)
        {
            exceptionLog = _exceptionLog;
        }

        public async Task<List<StockModel>> ReadExcelAsync(string fileName)
        {
            var stocks = new List<StockModel>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var excelConfiguraration = new ExcelDataSetConfiguration()
            {

                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = false
                }
            };

            try
            {
                using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var excelFileDataSet = reader.AsDataSet(excelConfiguraration);

                        DataTable dt = new DataTable();
                        dt = excelFileDataSet.Tables[0];

                        if(dt.Rows.Count > 0)
                        {
                            foreach (var row in dt.Rows)
                            {
                                var s = ((System.Data.DataRow)row).ItemArray;

                                // StockModel StockModel.Sr StockModel.Brand StockModel.AssetType StockModel.MAKEMODEL StockModel.SNIDNUMBER StockModel.STATUS19072022
                                // Startup_B.Models.Stock.StockModel	Sr	SN/ID NUMBER	MAKE/MODEL	STATUS 19.07.2022	Brand	Asset Type

                                stocks.Add(new StockModel()
                                {
                                    Sr = s[0]?.ToString(),
                                    Brand = s[1]?.ToString(),
                                    AssetType = s[2]?.ToString(),
                                    MAKEMODEL = s[3]?.ToString(),
                                    SNIDNUMBER = s[4]?.ToString(),
                                    STATUS19072022 = s[5]?.ToString(),
                                });
                            }
                        }
                        else
                        {
                            await exceptionLog.LogErrors("", "No records found in the uploaded file.");
                        }

                        // add
                        /*while (reader.Read())
                        {
                            if (reader.GetValue(0) != null && reader.GetValue(1) != null && reader.GetValue(2) != null)
                            {
                                users.Add(new User()
                                {
                                    Name = reader.GetValue(0).ToString(),
                                    Email = reader.GetValue(1).ToString(),
                                    Phone = reader.GetValue(2).ToString()
                                });
                            }
                        }*/

                    }
                }
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (System.Exception ex)
            {
                await exceptionLog.LogException("ReadExcelAsync", ex.Message, DateTime.Now);
            }

            return stocks;
        }
    }
}
