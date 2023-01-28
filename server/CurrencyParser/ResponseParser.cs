using CurrencyParser.Interfaces;
using DatabaseAbstractions.Models;
using System.Globalization;

namespace CurrencyParser
{
    public class ResponseParser : IResponseParser
    {
        const char lineTerminator = '\n';
        const char valueSeparator = '|';

        public IEnumerable<CurrencyRow> ParseResponse(string data)
        {
            var rows = data
                .Split(lineTerminator);
            var infoRowValues = rows[0]
                .Split(valueSeparator);
            var dataRows = rows
                .Where(it => !it.Contains("Date") && !string.IsNullOrWhiteSpace(it));
            var currencyRows = dataRows
                    .Select(it => ParseRow(infoRowValues, it));
            return currencyRows;
        }

        private CurrencyRow ParseRow(string[] dataRow, string row)
        {
            var rowValues = row.Split(valueSeparator);
            var date = ParseDate(rowValues[0]);

            var currencyRow = new CurrencyRow();
            for (int i = 1; i < rowValues.Length; i++)
            {
                var currencyInfo = dataRow[i].Split(" ");
                var currencyName = currencyInfo[1];
                var currencyMultiplier = int.Parse(currencyInfo[0]);
                var value = decimal.Parse(rowValues[i].Trim());
                if (currencyMultiplier == 100)
                {
                    value /= 100;
                }
                currencyRow.Date = date;
                if (currencyRow.GetType().GetProperty(currencyName) == null)
                {
                    throw new InvalidDataException("Missed currency " + currencyName);
                }
                currencyRow
                    .GetType()
                    .GetProperty(currencyName)?
                    .SetValue(currencyRow, value);
            }
            return currencyRow;
        }

        private static DateTimeOffset ParseDate(string date)
        {
            var provider = CultureInfo.InvariantCulture.DateTimeFormat;
            var result = DateTimeOffset.ParseExact(
                date,
                new string[] { "dd.MM.yyyy" },
                provider,
                DateTimeStyles.None);
            return result;
        }
    }
}
