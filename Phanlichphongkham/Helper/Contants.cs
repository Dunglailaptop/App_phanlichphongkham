using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Helper
{
    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
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
        public static string RemoveVietnameseAndSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Bảng ánh xạ ký tự có dấu sang không dấu
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            // Loại bỏ dấu và chuyển về chuỗi không dấu
            string noDiacritics = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            // Loại bỏ khoảng trắng và ký tự không mong muốn
            return new string(noDiacritics
                .Where(c => char.IsLetterOrDigit(c))
                .ToArray());
        }
    }
}
