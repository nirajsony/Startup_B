using ExcelDataReader;
using Startup_B.Models.Stock;
using System.Data;
using System.IO;

namespace Startup_B.Services.stock
{
    public class Stock : IStock
    {
        public async Task<List<User>> ReadExcelAsync(string fileName)
        {
            var users = new List<User>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var excelConfiguraration = new ExcelDataSetConfiguration() {

                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = false
                }
            };
            

            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var excelFileDataSet = reader.AsDataSet(excelConfiguraration);

                    DataTable dt = new DataTable();
                    dt = excelFileDataSet.Tables[0];

                    foreach (var row in dt.Rows)
                    {
                        var s = ((System.Data.DataRow)row).ItemArray;

                        users.Add(new User()
                        {
                            Name = s[0]?.ToString(),
                            Email = s[1]?.ToString(),
                            Phone = s[2]?.ToString()
                        });

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
            return users;
        }
    }
}
