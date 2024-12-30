using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SuperZTP.TemplateMethod
{
    public class GenerateTXT : Generate
    {
        public GenerateTXT(List<Model.Task> tasks) : base(tasks) { }

        protected override void Save(string filepath, string content)
        {
            File.WriteAllText(filepath, content);
        }
    }
}
