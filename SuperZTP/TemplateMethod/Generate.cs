using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Resources;

namespace SuperZTP.TemplateMethod
{
    public abstract class Generate
    {
        protected List<Model.Task> tasks;
        protected abstract void Save(string filepath, string content);

        public Generate(List<Model.Task> tasks)
        {
            this.tasks = tasks;
        }

        // Generowanie raportu
        public void GenerateRaport(string filepath)
        {
            var upcomingDeadlines = tasks.Where(task => task.Deadline >= DateTime.Now && task.Deadline <= DateTime.Now.AddDays(3));
            var content = GenerateUpcomingTaskReport(upcomingDeadlines);
            Save(filepath, content);
        }

        public string GenerateUpcomingTaskReport(IEnumerable<Model.Task> tasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Strings.UpcomingTasksTitle);
            foreach (var task in tasks)
            {
                sb.AppendLine($"{task.Title} - {Strings.Deadline} {task.Deadline:yyyy-MM-dd}");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        // Generowanie podsumowania
        public void GenerateSummary(string filepath)
        {
            var completedTasks = tasks.Where(task => task.IsDone == true);
            var overdueTasks = tasks.Where(task => task.IsDone == false && task.Deadline < DateTime.Now);
            var content = GenerateCompletedTasksReport(completedTasks) + GenerateOverdueTasksReport(overdueTasks);
            Save(filepath, content);
        }

        public string GenerateCompletedTasksReport(IEnumerable<Model.Task> completedTasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Strings.CompletedTasksTitle);
            foreach (var task in completedTasks)
            {
                sb.AppendLine($"{task.Title} - {Strings.Deadline} {task.Deadline:yyyy-MM-dd} - {Strings.TaskStatus_Done}");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public string GenerateOverdueTasksReport(IEnumerable<Model.Task> overdueTasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Strings.OverdueTasksTitle);
            foreach (var task in overdueTasks)
            {
                sb.AppendLine($"{task.Title} - {Strings.Deadline} {task.Deadline:yyyy-MM-dd} - {Strings.TaskStatus_NotDone}");
            }
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
