using Xunit;
using SuperZTP.ViewModels;
using SuperZTP.Stores;
using SuperZTP.Model;
using SuperZTP.Command;
using System.Collections.Generic;
using System.Linq;
using System;
using TaskModel = SuperZTP.Model.Task;
using SuperZTP.Facade;

namespace SuperZTP.Tests.ViewModels
{
    public class DisplayTasksViewModelTests
    {
        [Fact]
        public void RefreshTasks_ShouldAddPreviews_ForTasksAndNotes()
        {
            var task = new TaskModel(1, "Tytuł", "Opis", new Tag("Tag"), new Category("Cat"), DateTime.Now, "Wysoki", false, null!);
            var note = new Note { Title = "Notatka", Description = "Opis", Category = new Category("C"), Tag = new Tag("T") };
            var tasks = new List<TaskModel> { task };
            var notes = new List<Note> { note };
            var categories = new List<Category>();
            var tags = new List<Tag>();
            var fileHandler = new FileHandler(tasks, notes, categories, tags);
            var state = new TaskState(tasks, notes, categories, tags, fileHandler);
            var store = new SelectedTaskStore();
            var menuVM = new MenuViewModel(store, state);
            var vm = new DisplayTasksViewModel(store, state, new CommandInvoker(), menuVM);

            vm.RefreshTasks();

            Assert.True(vm.Previews.Count() >= 3);
        }

        [Fact]
        public void SelectedTaskViewModel_SetTask_ShouldUpdateStore()
        {
            var task = new TaskModel(1, "Tytuł", "Opis", new Tag("T"), new Category("C"), DateTime.Now, "Wysoki", false, null!);
            var tasks = new List<TaskModel> { task };
            var notes = new List<Note>();
            var categories = new List<Category>();
            var tags = new List<Tag>();
            var fileHandler = new FileHandler(tasks, notes, categories, tags);
            var state = new TaskState(tasks, notes, categories, tags, fileHandler);
            var store = new SelectedTaskStore();
            var menuVM = new MenuViewModel(store, state);
            var preview = new DisplayTaskPreviewViewModel(task, state, new CommandInvoker(), () => { }, menuVM);
            var vm = new DisplayTasksViewModel(store, state, new CommandInvoker(), menuVM);

            vm.SelectedTaskViewModel = preview;

            Assert.Equal(task, store.SelectedTask);
            Assert.Null(store.SelectedNote);
        }
    }
}
