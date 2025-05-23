﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using SuperZTP.Command;
using SuperZTP.Facade;
using SuperZTP.Model;
using SuperZTP.Views;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.ViewModels
{
    public class DisplayTaskPreviewViewModel : BaseViewModel
    {
        private readonly List<Model.Task> _tasks;
        private readonly List<Model.Note> _notes;
        private readonly TaskState _taskState;
        private readonly CommandInvoker _invoker;
        private readonly Action _refreshMenu;
        private MenuViewModel _viewModel;

        public Model.Task Task { get; }
        public Model.Note Note { get; }
        public Model.Header Header { get; }
        public string Title => Task?.Title ?? Note?.Title ?? Header.Title;
        public System.Windows.Input.ICommand EditCommand { get; }
        public System.Windows.Input.ICommand DeleteCommand { get; }
        public System.Windows.Input.ICommand EditNCommand { get; }
        public System.Windows.Input.ICommand DeleteNCommand { get; }
        public bool IsTask => Task != null;
        public bool IsNote => Note != null;
        public bool IsHeader => Header != null;
        public DisplayTaskPreviewViewModel(Task task, TaskState state, CommandInvoker invoker, Action refreshMenu, MenuViewModel viewModel)
        {
            Task = task;
            _tasks = state.Tasks;
            _notes = state.Notes;
            _taskState = state;
            _invoker = invoker;
            _refreshMenu = refreshMenu;
            _viewModel = viewModel;

            DeleteCommand = new RelayCommand(DeleteTaskCommand);
            EditCommand = new RelayCommand(EditTaskCommand);
        }
        public DisplayTaskPreviewViewModel(Note note, TaskState state, CommandInvoker invoker, Action refreshMenu, MenuViewModel viewModel)
        {
            Note = note;
            _tasks = state.Tasks;
            _notes = state.Notes;
            _taskState = state;
            _invoker = invoker;
            _refreshMenu = refreshMenu;
            _viewModel = viewModel;

            DeleteNCommand = new RelayCommand(DeleteNoteCommand);
            EditNCommand = new RelayCommand(EditNoteCommand);
        }

        public DisplayTaskPreviewViewModel(Header header, TaskState state, CommandInvoker invoker, Action refreshMenu)
        {
            Header = header;
            _tasks = state.Tasks;
            _notes = state.Notes;
            _taskState = state;
            _invoker = invoker;
            _refreshMenu = refreshMenu;

            DeleteCommand = new RelayCommand(DeleteTaskCommand);
            EditCommand = new RelayCommand(EditTaskCommand);
        }
        private void DeleteTaskCommand()
        {
            _invoker.AddCommand(new DeleteTask(_tasks, Task, _refreshMenu));
            _invoker.Execute();
            _viewModel.UpdateHistory();
            _taskState.FileHandler.SaveTasksToFile("tasks.txt");
        }

        private void EditTaskCommand()
        {
            var editTaskWindow = new EditTaskWindow(Task, _taskState.FileHandler, _taskState.Categories, _taskState.Tags, _invoker, _viewModel, _tasks);
            if (editTaskWindow.ShowDialog() == true)
            {
                var editedTask = editTaskWindow.EditedTask;
                var taskIndex = _taskState.Tasks.FindIndex(t => t.Id == editedTask.Id);
                if (taskIndex >= 0)
                {
                    _taskState.Tasks[taskIndex] = editedTask;
                }
                _refreshMenu.Invoke();
            }
            //proxy.ClearTaskCache();
        }

        private void DeleteNoteCommand()
        {
            _invoker.AddCommand(new DeleteNote(_notes, Note, _refreshMenu));
            _invoker.Execute();
            _viewModel.UpdateHistory();
            _taskState.FileHandler.SaveNotesToFile("notes.txt");
        }

        private void EditNoteCommand()
        {
            var editNoteWindow = new EditNoteWindow(Note, _taskState.FileHandler, _taskState.Categories, _taskState.Tags, _invoker, _viewModel, _notes);
            if (editNoteWindow.ShowDialog() == true)
            {
                var editedNote = editNoteWindow.EditedNote;
                var noteIndex = _taskState.Notes.FindIndex(n => n.Id == editedNote.Id);
                if (noteIndex >= 0)
                {
                    _taskState.Notes[noteIndex] = editedNote;
                }
                _refreshMenu.Invoke();
            }
            //proxy.ClearTaskCache();
        }
    }
}
