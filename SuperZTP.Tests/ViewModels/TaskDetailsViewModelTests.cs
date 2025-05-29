using Xunit;
using SuperZTP.ViewModels;
using SuperZTP.Model;
using SuperZTP.Stores;
using System;

namespace SuperZTP.Tests.ViewModels
{
    public class TaskDetailsViewModelTests
    {
        [Fact]
        public void Properties_ShouldReflectSelectedTask()
        {
            var task = new SuperZTP.Model.Task
            {
                Title = "Zadanie",
                Description = "Opis",
                Deadline = new DateTime(2024, 1, 1),
                Category = new Category("Kat"),
                Tag = new Tag("Tag"),
                Priority = "Wysoki",
                IsDone = true
            };
            var store = new SelectedTaskStore { SelectedTask = task };
            var vm = new TaskDetailsViewModel(store);

            Assert.True(vm.HasSelectedTask);
            Assert.Equal("Zadanie", vm.Title);
            Assert.Equal("Opis", vm.Description);
            Assert.Equal("Kat", vm.Category);
            Assert.Equal("Tag", vm.Tag);
            Assert.Equal("Wysoki", vm.Priority);
            Assert.Equal("Ukończone", vm.IsDone);
            Assert.Equal(task.Deadline, vm.Date);
        }

        [Fact]
        public void HasNoSelection_ShouldBeTrue_WhenNoNoteOrTask()
        {
            var store = new SelectedTaskStore();
            var vm = new TaskDetailsViewModel(store);
            Assert.True(vm.HasNoSelection);
        }
    }
}