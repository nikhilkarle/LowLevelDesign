namespace TemplatePattern.Models
{
    public class ImportFile
    {
        public int Id {get; set;}
        public string Name {get; set;}
        
        public ImportFile(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}