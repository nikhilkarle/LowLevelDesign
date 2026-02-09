using System;
using TemplatePattern.Models;

namespace TemplatePattern.Template
{
    public abstract class DataImporter
    {
        public void Import()
        {
            string raw = ReadRawData();
            List<ImportFile> records = Parse(raw);

            bool valid = Validate(records);
            if (!valid)
            {
                Console.WriteLine("Validation failed!");
                return;
            }

            Save(records);
            Report(records);
        }

        protected abstract string ReadRawData();
        protected abstract List<ImportFile> Parse(string raw);

        protected virtual bool Validate(List<ImportFile> records)
        {
            if (records.Count == 0)
            {
                return false;
            }
            // do other validation checks
            return true;
        }

        protected virtual void Save(List<ImportFile> records)
        {
            Console.WriteLine("Saved records to Database");
        }

        protected virtual void Report(List<ImportFile> records)
        {
            Console.WriteLine("Import complete");
        }
    }
}