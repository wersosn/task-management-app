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
        public string Title => _selectedTaskStore.SelectedTask?.Title;
        public string Description => _selectedTaskStore.SelectedTask?.Description;
        public DateTime Date => _selectedTaskStore.SelectedTask?.Deadline ?? DateTime.Now;
        public string Category => _selectedTaskStore.SelectedTask?.Category?.Name.Trim() ?? null;

        public TaskDetailsViewModel(SelectedTaskStore selectedTaskStore)
        {
            _selectedTaskStore = selectedTaskStore;

            _selectedTaskStore.SelectedTaskChanged += _selectedTaskStore_SelectedTaskChanged;
        }

        private void _selectedTaskStore_SelectedTaskChanged()
        {
            OnPropertyChanged(nameof(HasSelectedTask));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Category));

        }

        protected override void Dispose()
        {
            _selectedTaskStore.SelectedTaskChanged -= _selectedTaskStore_SelectedTaskChanged;

            base.Dispose();
        }
    }
}
