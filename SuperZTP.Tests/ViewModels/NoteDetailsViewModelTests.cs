using Xunit;
using SuperZTP.ViewModels;
using SuperZTP.Model;
using SuperZTP.Stores;
using System;

namespace SuperZTP.Tests.ViewModels
{
    public class NoteDetailsViewModelTests
    {
        [Fact]
        public void Properties_ShouldReflectSelectedNote()
        {
            var note = new Note
            {
                Title = "Test",
                Description = "Opis",
                Category = new Category("Kat"),
                Tag = new Tag("Tag")
            };
            var store = new SelectedTaskStore { SelectedNote = note };
            var vm = new NoteDetailsViewModel(store);

            Assert.True(vm.HasSelectedNote);
            Assert.Equal("Test", vm.Title);
            Assert.Equal("Opis", vm.Description);
            Assert.Equal("Kat", vm.Category);
            Assert.Equal("Tag", vm.Tag);
        }

        [Fact]
        public void Properties_ShouldBeNull_WhenNoteIsNull()
        {
            var store = new SelectedTaskStore { SelectedNote = null };
            var vm = new NoteDetailsViewModel(store);

            Assert.False(vm.HasSelectedNote);
            Assert.Null(vm.Title);
            Assert.Null(vm.Description);
            Assert.Null(vm.Category);
            Assert.Null(vm.Tag);
        }
    }
}