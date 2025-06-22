using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Helper
{
    public static class Contants
    {
        public static async Task<DataTable> ConvertToDataTableAsync<T>(Task<List<T>> taskList)
        {
            var list = await taskList;
            var table = new DataTable();

            if (list == null || list.Count == 0)
                return table;

            var type = typeof(T);
            var properties = type.GetProperties();

            // Tạo cột
            foreach (var prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Tạo dòng
            foreach (var item in list)
            {
                var row = table.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
