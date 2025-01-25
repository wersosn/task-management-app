using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using SuperZTP.Stores;
using WpfCommand = System.Windows.Input.ICommand;

using SuperZTP.Command;

namespace SuperZTP.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel
    {
        private readonly SelectedTaskStore _selectedTaskStore;
        public bool HasSelectedTask => _selectedTaskStore.SelectedTask != null;
        public bool HasSelectedNote => _selectedTaskStore.SelectedNote != null;
        public bool HasNoSelection => !HasSelectedTask && !HasSelectedNote;
        public string Title => _selectedTaskStore.SelectedTask?.Title;
        public string Description => _selectedTaskStore.SelectedTask?.Description;
        public DateTime Date => _selectedTaskStore.SelectedTask?.Deadline ?? DateTime.Now;
        public string Category => _selectedTaskStore.SelectedTask?.Category?.Name.Trim() ?? null;

        public string Status => _selectedTaskStore.SelectedTask?.CurrentState?.GetStateName() ?? "Unknown";

		// do obsługi przycisku odznaczenia zadania
		public WpfCommand ChangeStatusCommand { get; }
		public string ButtonLabel => _selectedTaskStore.SelectedTask?.CurrentState?.ButtonLabel ?? "N/A";
		public bool IsButtonEnabled => _selectedTaskStore.SelectedTask?.CurrentState?.IsButtonEnabled ?? false;

		public TaskDetailsViewModel(SelectedTaskStore selectedTaskStore)
        {
            _selectedTaskStore = selectedTaskStore;

            _selectedTaskStore.SelectedTaskChanged += _selectedTaskStore_SelectedTaskChanged;


			// Inicjalizacja komendy
			ChangeStatusCommand = new RelayCommand(ChangeStatus, () => IsButtonEnabled);

		}

		private void ChangeStatus()
		{
			_selectedTaskStore.SelectedTask?.CurrentState?.Start(_selectedTaskStore.SelectedTask);

			// Aktualizacja widoku
			OnPropertyChanged(nameof(ButtonLabel));
			OnPropertyChanged(nameof(IsButtonEnabled));
			OnPropertyChanged(nameof(Status));
		}

		private void _selectedTaskStore_SelectedTaskChanged()
        {
            OnPropertyChanged(nameof(HasSelectedTask));
            OnPropertyChanged(nameof(HasSelectedNote));
            OnPropertyChanged(nameof(HasNoSelection));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Category));
			OnPropertyChanged(nameof(Status));

			OnPropertyChanged(nameof(ButtonLabel));
			OnPropertyChanged(nameof(IsButtonEnabled));
		
		}

		protected override void Dispose()
        {
            _selectedTaskStore.SelectedTaskChanged -= _selectedTaskStore_SelectedTaskChanged;

            base.Dispose();
        }
    }
}
