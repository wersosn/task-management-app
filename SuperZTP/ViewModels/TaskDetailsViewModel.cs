using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using SuperZTP.Stores;

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

		public TaskDetailsViewModel(SelectedTaskStore selectedTaskStore)
        {
            _selectedTaskStore = selectedTaskStore;

            _selectedTaskStore.SelectedTaskChanged += _selectedTaskStore_SelectedTaskChanged;
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


		}

		protected override void Dispose()
        {
            _selectedTaskStore.SelectedTaskChanged -= _selectedTaskStore_SelectedTaskChanged;

            base.Dispose();
        }
    }
}
