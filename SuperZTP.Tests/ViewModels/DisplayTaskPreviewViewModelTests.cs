using Xunit;
using SuperZTP.ViewModels;
using SuperZTP.Model;
using SuperZTP.Command;
using System.Collections.Generic;
using System;
using TaskModel = SuperZTP.Model.Task;
using SuperZTP.Facade;
using SuperZTP.Stores;

namespace SuperZTP.Tests.ViewModels
{
    public class DisplayTaskPreviewViewModelTests
    {
        [Fact]
        public void Title_ShouldReturnTaskTitle_WhenTaskSet()
        {
            var task = new TaskModel(1, "TestTitle", "Opis", new Tag("T"), new Category("C"), DateTime.Now, "Średni", false, null!);
            var tasks = new List<TaskModel>();
            var notes = new List<Note>();
            var fileHandler = new FileHandler(tasks, notes, new List<Category>(), new List<Tag>());
            var state = new TaskState(tasks, notes, new List<Category>(), new List<Tag>(), fileHandler);
            var invoker = new CommandInvoker();
            var vm = new MenuViewModel(new SelectedTaskStore(), state);
            var previewVM = new DisplayTaskPreviewViewModel(task, state, invoker, () => { }, vm);

            Assert.Equal("TestTitle", previewVM.Title);
            Assert.True(previewVM.IsTask);
        }

        [Fact]
        public void Title_ShouldReturnNoteTitle_WhenNoteSet()
        {
            var note = new Note { Title = "NoteTitle", Description = "Opis", Category = new Category("C"), Tag = new Tag("T") };
            var tasks = new List<TaskModel>();
            var notes = new List<Note>();
            var fileHandler = new FileHandler(tasks, notes, new List<Category>(), new List<Tag>());
            var state = new TaskState(tasks, notes, new List<Category>(), new List<Tag>(), fileHandler);
            var invoker = new CommandInvoker();
            var vm = new MenuViewModel(new SelectedTaskStore(), state);
            var previewVM = new DisplayTaskPreviewViewModel(note, state, invoker, () => { }, vm);

            Assert.Equal("NoteTitle", previewVM.Title);
            Assert.True(previewVM.IsNote);
        }
    }
}
