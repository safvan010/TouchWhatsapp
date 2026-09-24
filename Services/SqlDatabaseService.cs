using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WhatsappAutomation.Models;

namespace WhatsappAutomation.Services
{
    public class SalesRecord
    {
        public int SI { get; set; }
        public int VoucherNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public double GrandTotal { get; set; }
        public double NetTotal { get; set; }
        public double Discount { get; set; }
        public DateTime? SoldDate { get; set; }
        public string InvoiceType { get; set; }
        public string VoucherPrefix { get; set; }
    }

    public class SqlDatabaseService
    {
        private const string SqlUser = "Touchone";
        private const string SqlPass = "TouchOne$@454r54%a4";
        private readonly string _configFilePath;

        public string InstanceName { get; set; } = @"DELL\SQL2008R2";
        public string DatabaseName { get; set; } = "_CODE_";

        public SqlDatabaseService(string configFilePath = null)
        {
            _configFilePath = configFilePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dbconfig.txt");
            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    var lines = File.ReadAllLines(_configFilePath);
                    if (lines.Length > 0 && !string.IsNullOrWhiteSpace(lines[0]))
                    {
                        InstanceName = lines[0].Trim();
                    }
                    if (lines.Length > 1 && !string.IsNullOrWhiteSpace(lines[1]))
                    {
                        DatabaseName = lines[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SqlDatabaseService] Error loading dbconfig.txt: {ex.Message}");
            }
        }

        public void SaveConfig(string instance, string database)
        {
            InstanceName = instance?.Trim() ?? string.Empty;
            DatabaseName = database?.Trim() ?? string.Empty;

            try
            {
                File.WriteAllLines(_configFilePath, new[]
                {
                    InstanceName,
                    DatabaseName
                });
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save dbconfig.txt: {ex.Message}", ex);
            }
        }

        public string BuildConnectionString()
        {
            return $"Data Source={InstanceName};Initial Catalog={DatabaseName};User ID={SqlUser};Password={SqlPass};Persist Security Info=True;Connect Timeout=15;Application Name=WhatsAppAutomation;Pooling=true;Max Pool Size=20;Min Pool Size=1;";
        }

        public async Task<(bool Success, string Message)> TestConnectionAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InstanceName))
                    return (false, "SQL Instance Name is required.");

                if (string.IsNullOrWhiteSpace(DatabaseName))
                    return (false, "Database Name is required.");

                string connStr = BuildConnectionString();
                using (var conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand("SELECT @@VERSION", conn))
                    {
                        var version = await cmd.ExecuteScalarAsync();
                        return (true, $"Connected successfully! Server: {version?.ToString()?.Split('\n')[0]}");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Connection Failed: {ex.Message}");
            }
        }

        public async Task<List<SalesRecord>> GetPendingSalesAsync(DateTime? filterDate = null)
        {
            var records = new List<SalesRecord>();
            string connStr = BuildConnectionString();

            // SARGable date range: allows SQL Server to perform an index seek instead of a slow full-table scan.
            DateTime startDate = filterDate.HasValue ? filterDate.Value.Date : DateTime.Today;
            DateTime endDate = startDate.AddDays(1);

            // Use WITH (NOLOCK) so this background check NEVER blocks or locks tbSalesDetails for other users
            string sql = @"
                SELECT TOP 25 SI, VoucherNo, CustomerName, CustomerMobile, GrandTotal, NetTotal, Discount, SoldDate, InvoiceType, VoucherPrefix 
                FROM tbSalesDetails WITH (NOLOCK)
                WHERE SoldDate >= @StartDate AND SoldDate < @EndDate
                  AND InvoiceType = 'Normal' 
                  AND VoucherPrefix = (SELECT TOP 1 SettingsValue FROM tbSettings WITH (NOLOCK) WHERE SettingsName = 'VoucherPrefix')
                  AND (TokenNumber = '' OR TokenNumber IS NULL) 
                  AND CustomerMobile IS NOT NULL 
                  AND CustomerMobile <> ''
                ORDER BY SI ASC";

            using (var conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            records.Add(new SalesRecord
                            {
                                SI = reader["SI"] != DBNull.Value ? Convert.ToInt32(reader["SI"]) : 0,
                                VoucherNo = reader["VoucherNo"] != DBNull.Value ? Convert.ToInt32(reader["VoucherNo"]) : 0,
                                CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "Customer",
                                CustomerMobile = reader["CustomerMobile"] != DBNull.Value ? reader["CustomerMobile"].ToString() : string.Empty,
                                GrandTotal = reader["GrandTotal"] != DBNull.Value ? Convert.ToDouble(reader["GrandTotal"]) : 0.0,
                                NetTotal = reader["NetTotal"] != DBNull.Value ? Convert.ToDouble(reader["NetTotal"]) : 0.0,
                                Discount = reader["Discount"] != DBNull.Value ? Convert.ToDouble(reader["Discount"]) : 0.0,
                                SoldDate = reader["SoldDate"] != DBNull.Value ? Convert.ToDateTime(reader["SoldDate"]) : (DateTime?)null,
                                InvoiceType = reader["InvoiceType"] != DBNull.Value ? reader["InvoiceType"].ToString() : string.Empty,
                                VoucherPrefix = reader["VoucherPrefix"] != DBNull.Value ? reader["VoucherPrefix"].ToString() : string.Empty
                            });
                        }
                    }
                }
            }

            return records;
        }

        public async Task<List<SalesItem>> GetSalesItemsAsync(int si)
        {
            var items = new List<SalesItem>();
            string connStr = BuildConnectionString();

            // Query provided by user with WITH (NOLOCK) to ensure POS billing users are never locked
            string sql = @"
                SELECT tbSales.ItemName, tbSales.Qty, tbSales.Rate, tbSales.Disc, tbSales.NetAmount 
                FROM tbSalesDetails WITH (NOLOCK) 
                INNER JOIN tbSales WITH (NOLOCK) 
                   ON tbSales.VoucherNo = tbSalesDetails.VoucherNo 
                  AND tbSales.InvoiceType = tbSalesDetails.InvoiceType 
                  AND tbSales.VoucherPrefix = tbSalesDetails.VoucherPrefix 
                WHERE tbSalesDetails.SI = @SI";

            using (var conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.Add("@SI", SqlDbType.Int).Value = si;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new SalesItem
                            {
                                ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty,
                                Qty = reader["Qty"] != DBNull.Value ? Convert.ToDouble(reader["Qty"]) : 0.0,
                                Rate = reader["Rate"] != DBNull.Value ? Convert.ToDouble(reader["Rate"]) : 0.0,
                                Disc = reader["Disc"] != DBNull.Value ? Convert.ToDouble(reader["Disc"]) : 0.0,
                                NetAmount = reader["NetAmount"] != DBNull.Value ? Convert.ToDouble(reader["NetAmount"]) : 0.0
                            });
                        }
                    }
                }
            }

            return items;
        }

        public async Task<bool> UpdateTokenNumberAsync(int si, string token = "1")
        {
            string connStr = BuildConnectionString();
            // Use WITH (ROWLOCK) to ensure only the single specific row is locked during update
            string sql = "UPDATE tbSalesDetails WITH (ROWLOCK) SET TokenNumber = @Token WHERE SI = @SI";

            using (var conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 10;
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 10).Value = token;
                    cmd.Parameters.Add("@SI", SqlDbType.Int).Value = si;
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

        public static string NormalizeIndianMobile(string rawMobile)
        {
            if (string.IsNullOrWhiteSpace(rawMobile))
                return string.Empty;

            // Strip non-digits
            string digits = Regex.Replace(rawMobile, @"[^\d]", "");

            // If 10 digits (e.g. 9567366327), prefix with 91
            if (digits.Length == 10)
            {
                return "91" + digits;
            }

            // If 11 digits starting with 0 (e.g. 09567366327), replace leading 0 with 91
            if (digits.Length == 11 && digits.StartsWith("0"))
            {
                return "91" + digits.Substring(1);
            }

            // If already has country code (e.g. 919567366327, 12 digits)
            return digits;
        }

        public static string FormatSalesMessage(SalesRecord record, string template = null, string shopName = "")
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                string customer = string.IsNullOrWhiteSpace(record.CustomerName) ? "Customer" : record.CustomerName.Trim();
                string amount = record.GrandTotal.ToString("N2");
                return $"Hai {customer}\nThank you for purchase !\nYour bill amount is {amount}";
            }

            string result = template;
            result = result.Replace("{CustomerName}", string.IsNullOrWhiteSpace(record.CustomerName) ? "Customer" : record.CustomerName.Trim());
            result = result.Replace("{GrandTotal}", record.GrandTotal.ToString("N2"));
            result = result.Replace("{VoucherNo}", record.VoucherNo.ToString());
            result = result.Replace("{SoldDate}", record.SoldDate.HasValue ? record.SoldDate.Value.ToString("dd-MMM-yyyy") : DateTime.Today.ToString("dd-MMM-yyyy"));
            result = result.Replace("{NetTotal}", record.NetTotal.ToString("N2"));
            result = result.Replace("{Discount}", record.Discount.ToString("N2"));
            result = result.Replace("{ShopName}", shopName ?? string.Empty);

            return result;
        }
    }
}
