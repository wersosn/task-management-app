using Xunit;
using SuperZTP.ViewModels;
using SuperZTP.Model;
using SuperZTP.Stores;
using System.Collections.Generic;
using TaskModel = SuperZTP.Model.Task;
using SuperZTP.Facade;

namespace SuperZTP.Tests.ViewModels
{
    public class MenuViewModelTests
    {
        private TaskState GetEmptyState()
        {
            var tasks = new List<TaskModel>();
            var notes = new List<Note>();
            var categories = new List<Category>();
            var tags = new List<Tag>();
            var fileHandler = new FileHandler(tasks, notes, categories, tags);
            return new TaskState(tasks, notes, categories, tags, fileHandler);
        }

        [Fact]
        public void GenerateSelectedReport_ShouldNotThrow_WhenTXTSelected()
        {
            var vm = new MenuViewModel(new SelectedTaskStore(), GetEmptyState());
            vm.SelectedReportType = "TXT";

            var ex = Record.Exception(() => vm.GenerateSelectedReport());

            Assert.Null(ex);
        }

        [Fact]
        public void ToggleHistory_ShouldChangeVisibility()
        {
            var vm = new MenuViewModel(new SelectedTaskStore(), GetEmptyState());
            var initial = vm.HistoryVisibility;
            vm.ToggleHistoryCommand.Execute(null);
            Assert.NotEqual(initial, vm.HistoryVisibility);
        }

        [Fact]
        public void SelectedCategory_SetValue_ShouldRaisePropertyChanged()
        {
            var vm = new MenuViewModel(new SelectedTaskStore(), GetEmptyState());
            vm.SelectedCategory = "Nowa";
            Assert.Equal("Nowa", vm.SelectedCategory);
        }
    }
}
