using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZTP.Model;
using SuperZTP.Stores;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.ViewModels
{
    public class DisplayTasksViewModel : BaseViewModel
    {
        private readonly SelectedTaskStore _selectedTaskStore;
        private readonly ObservableCollection<DisplayTaskPreviewViewModel> _previews;
        public IEnumerable<DisplayTaskPreviewViewModel> Previews => _previews;
        private DisplayTaskPreviewViewModel _selectedTaskViewModel;
        public DisplayTaskPreviewViewModel SelectedTaskViewModel
        {
            get
            {
                return _selectedTaskViewModel;
            }
            set
            {
                _selectedTaskViewModel = value;
                OnPropertyChanged(nameof(SelectedTaskViewModel));
                _selectedTaskStore.SelectedTask =_selectedTaskViewModel?.Task;
            }
        }

        public DisplayTasksViewModel(SelectedTaskStore selectedTaskStore)
        {
            _selectedTaskStore = selectedTaskStore;
            _previews = new ObservableCollection<DisplayTaskPreviewViewModel>();
            _previews.Add(new DisplayTaskPreviewViewModel(
            new Task(1,"Test","Lorem",new Tag("Tag"),new Category("Category"),DateTime.Now, "low",true)
                ));
            _previews.Add(new DisplayTaskPreviewViewModel(
                new Task(2, "Prototyp", "Lorem", new Tag("Tag"), new Category("Category"), DateTime.Now, "low", true)
            ));
        }
    }
}
