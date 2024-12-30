using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            var upcomingDeadlines = tasks.Where(task => task.Deadline > DateTime.Now && task.Deadline <= DateTime.Now.AddDays(7));
            var content = GenerateUpcomingTaskReport(upcomingDeadlines);
            Save(filepath, content);
        }

        public string GenerateUpcomingTaskReport(IEnumerable<Model.Task> tasks)
        {
            return "Zbliżające się terminy wykonania zadań:\n" +
                   string.Join("\n", tasks.Select(task => $"{task.Title} - Deadline: {task.Deadline:yyyy-MM-dd}")) + "\n\n";
        }

        // Generowanie podsumowania
        public void GenerateSummary(string filepath)
        {
            var completedTasks = tasks.Where(task => task.IsDone);
            var overdueTasks = tasks.Where(task => !task.IsDone && task.Deadline < DateTime.Now);
            var content1 = GenerateCompletedTasksReport(completedTasks);
            var content2 = GenerateOverdueTasksReport(overdueTasks);
            var content = content1 + content2;
            Save(filepath, content);
        }

        public string GenerateCompletedTasksReport(IEnumerable<Model.Task> completedTasks)
        {
            return "Wykonane zadania:\n" +
                   string.Join("\n", completedTasks.Select(task => $"{task.Title} - Deadline: {task.Deadline:yyyy-MM-dd} - Status: Wykonane")) + "\n\n";
        }

        public string GenerateOverdueTasksReport(IEnumerable<Model.Task> overdueTasks)
        {
            return "Zaległe zadania:\n" +
                   string.Join("\n", overdueTasks.Select(task => $"{task.Title} - Deadline: {task.Deadline:yyyy-MM-dd} - Status: Niewykonane")) + "\n\n";
        }
    }
}
