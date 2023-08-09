using System.Data;
using System.Data.SqlClient;

namespace B1_Task1
{
    public class DBService
    {
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=B1_Test;Integrated Security=True";

        public string FolderPath { get; set; }

        public DBService(string path)
        {
            FolderPath = path;
        }

        public void ImportData()
        {
            string tableName = "ImportedData";

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("Latin", typeof(string));
            dataTable.Columns.Add("Russian", typeof(string));
            dataTable.Columns.Add("Integer", typeof(int));
            dataTable.Columns.Add("Decimal", typeof(decimal));

            using (StreamReader reader = new StreamReader(Path.Combine(FolderPath, "result")))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        string line;
                        string[] values = new string[5];

                        var count = 0;

                        while ((line = reader.ReadLine()) != null)
                        {
                            values = line.Split(new string[] { "||" }, StringSplitOptions.None);

                            dataTable.Rows.Add(
                                null,
                                DateTime.Parse(values[0]),
                                values[1],
                                values[2],
                                int.Parse(values[3]),
                                decimal.Parse(values[4])
                            );

                            if (dataTable.Rows.Count == 100000)
                            {

                                bulkCopy.DestinationTableName = tableName;
                                bulkCopy.WriteToServer(dataTable);

                                Console.WriteLine($"Загружено {100000 * count} строк");

                                dataTable.Rows.Clear();

                                count++;
                            }                          
                        }
                    }
                }
            }
        }

        public void ExecuteProcedure()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var calculateCommand = new SqlCommand("CalculateSumAndMedian", connection);
                calculateCommand.CommandType = CommandType.StoredProcedure;

                var reader = calculateCommand.ExecuteReader();

                if (reader.Read())
                {
                    try
                    {
                        var sumIntegers = long.Parse(reader["SumIntegers"].ToString());
                        var medianDecimal = decimal.Parse(reader["MedianDecimal"].ToString());

                        Console.WriteLine($"Сумма целых чисел: {sumIntegers}");
                        Console.WriteLine($"Медиана дробных: {medianDecimal}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка. Данные отсутсвуют или в неправильном формате");
                    }                  
                }

            }
        }
    }
}
