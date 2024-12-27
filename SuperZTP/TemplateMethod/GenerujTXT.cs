using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SuperZTP.TemplateMethod
{
    public class GenerujTXT : Generuj
    {
        public GenerujTXT(List<Model.Task> tasks) : base(tasks) { }

        protected override void Zapisz(string filepath, string content)
        {
            File.WriteAllText(filepath, content);
        }
    }
}
