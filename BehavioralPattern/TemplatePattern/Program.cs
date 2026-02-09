using System;
using TemplatePattern.Importers;
using TemplatePattern.Template;

namespace TemplatePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataImporter csv = new CsvImporter();
            Console.WriteLine("=== CSV Import ===");
            csv.Import();

            Console.WriteLine();

            DataImporter json = new JsonImporter();
            Console.WriteLine("=== JSON Import ===");
            json.Import();
        }
    }
}
