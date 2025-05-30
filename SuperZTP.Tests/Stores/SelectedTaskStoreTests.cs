using System;
using Xunit;
using SuperZTP.Stores;
using SuperZTP.Model;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.Tests.Stores
{
    public class SelectedTaskStoreTests
    {
        [Fact]
        public void SelectedTask_SetValue_RaisesSelectedTaskChangedEvent()
        {
            // Arrange
            var store = new SelectedTaskStore();
            bool eventRaised = false;
            store.SelectedTaskChanged += () => eventRaised = true;
            var task = new Task { Id = 1, Title = "Test" };

            // Act
            store.SelectedTask = task;

            // Assert
            Assert.True(eventRaised);
            Assert.Equal(task, store.SelectedTask);
        }

        [Fact]
        public void SelectedNote_SetValue_RaisesSelectedNoteChangedEvent()
        {
            // Arrange
            var store = new SelectedTaskStore();
            bool eventRaised = false;
            store.SelectedNoteChanged += () => eventRaised = true;
            var note = new Note { Id = 10, Description = "Note content" };

            // Act
            store.SelectedNote = note;

            // Assert
            Assert.True(eventRaised);
            Assert.Equal(note, store.SelectedNote);
        }

        [Fact]
        public void SelectedTask_DefaultValue_IsNullAndNoEventOnGet()
        {
            // Arrange
            var store = new SelectedTaskStore();
            bool eventRaised = false;
            store.SelectedTaskChanged += () => eventRaised = true;

            // Act
            var current = store.SelectedTask;

            // Assert
            Assert.Null(current);
            Assert.False(eventRaised);
        }

        [Fact]
        public void SelectedNote_DefaultValue_IsNullAndNoEventOnGet()
        {
            // Arrange
            var store = new SelectedTaskStore();
            bool eventRaised = false;
            store.SelectedNoteChanged += () => eventRaised = true;

            // Act
            var current = store.SelectedNote;

            // Assert
            Assert.Null(current);
            Assert.False(eventRaised);
        }

        [Fact]
        public void SelectedTask_MultipleSubscribers_AllReceiveEvent()
        {
            // Arrange
            var store = new SelectedTaskStore();
            int callCount = 0;
            store.SelectedTaskChanged += () => callCount++;
            store.SelectedTaskChanged += () => callCount++;
            var task = new Task { Id = 2, Title = "Multi" };

            // Act
            store.SelectedTask = task;

            // Assert
            Assert.Equal(2, callCount);
        }

        [Fact]
        public void SelectedNote_MultipleSubscribers_AllReceiveEvent()
        {
            // Arrange
            var store = new SelectedTaskStore();
            int callCount = 0;
            store.SelectedNoteChanged += () => callCount++;
            store.SelectedNoteChanged += () => callCount++;
            var note = new Note { Id = 20, Description = "MultiNote" };

            // Act
            store.SelectedNote = note;

            // Assert
            Assert.Equal(2, callCount);
        }
    }
}
