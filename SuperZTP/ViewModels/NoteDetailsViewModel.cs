using SuperZTP.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperZTP.ViewModels
{
    public class NoteDetailsViewModel : BaseViewModel
    {
        private readonly SelectedTaskStore _selectedTaskStore;

        public bool HasSelectedNote => _selectedTaskStore.SelectedNote != null;
        public string Title => _selectedTaskStore.SelectedNote?.Title;
        public string Description => _selectedTaskStore.SelectedNote?.Description;
        public string Category => _selectedTaskStore.SelectedNote?.Category?.Name.Trim() ?? null;
        public string Tag => _selectedTaskStore.SelectedNote?.Tag?.Name.Trim() ?? null;
        public Visibility NoteDetailsVisibility => HasSelectedNote ? Visibility.Visible : Visibility.Collapsed;


        public NoteDetailsViewModel(SelectedTaskStore selectedTaskStore)
        {
            _selectedTaskStore = selectedTaskStore;

            _selectedTaskStore.SelectedNoteChanged += OnSelectedNoteChanged;
        }

        private void OnSelectedNoteChanged()
        {
            OnPropertyChanged(nameof(HasSelectedNote));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Tag));
        }

        protected override void Dispose()
        {
            _selectedTaskStore.SelectedNoteChanged -= OnSelectedNoteChanged;
            base.Dispose();
        }
    }
}
