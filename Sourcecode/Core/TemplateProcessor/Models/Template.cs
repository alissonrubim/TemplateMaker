using System.Collections.Generic;

namespace TemplateProcessor.Models
{
    public class Template
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<TemplateParameter> Parameters { get; set; }
        public string SearchDirectory { get; set; }
        public IEnumerable<string> SearchFileExtensions { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
