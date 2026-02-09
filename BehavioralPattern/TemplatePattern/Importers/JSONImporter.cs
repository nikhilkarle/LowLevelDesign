using System;
using System.Collections.Generic;
using System.Text.Json;
using TemplatePattern.Models;
using TemplatePattern.Template;

namespace TemplatePattern.Importers
{
    public class JsonImporter : DataImporter
    {
        private class JsonRow
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        protected override string ReadRawData()
        {
            string json = "[{\"Id\":10,\"Name\":\"NK1\"},{\"Id\":11,\"Name\":\"Nk2\"}]";
            return json;
        }

        protected override List<ImportFile> Parse(string raw)
        {
            List<ImportFile> records = new List<ImportFile>();

            JsonRow[] rows = JsonSerializer.Deserialize<JsonRow[]>(raw);

            if (rows == null)
            {
                return records;
            }

            for (int i = 0; i < rows.Length; i++)
            {
                ImportFile record = new ImportFile(rows[i].Id, rows[i].Name);
                records.Add(record);
            }

            return records;
        }
    }
}
