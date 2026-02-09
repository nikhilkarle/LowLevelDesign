using System;
using System.Collections.Generic;
using TemplatePattern.Models;
using TemplatePattern.Template;

namespace TemplatePattern.Importers
{
    public class CsvImporter : DataImporter
    {
        protected override string ReadRawData()
        {
            string csv =
                "1,Nk1\n" +
                "2,Nk2\n" +
                "3,Nk3\n";

            return csv;
        }

        protected override List<ImportFile> Parse(string raw)
        {
            List<ImportFile> records = new List<ImportFile>();

            string[] lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');

                int id = int.Parse(parts[0]);
                string name = parts[1];

                ImportFile record = new ImportFile(id, name);
                records.Add(record);
            }

            return records;
        }
    }
}
