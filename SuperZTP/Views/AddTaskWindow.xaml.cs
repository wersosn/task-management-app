﻿using SuperZTP.Builder;
using SuperZTP.Command;
using SuperZTP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SuperZTP.ViewModels;

namespace SuperZTP.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddTask.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private List<SuperZTP.Model.Task> tasks;
        private CommandInvoker invoker;
        private TaskBuilder taskBuilder = new TaskBuilder();
        private FileHandler fileHandler;
        public event Action TaskAdded;
        private MenuViewModel _viewModel;

        public AddTaskWindow(List<SuperZTP.Model.Task> tasks, FileHandler fileHandler, List<Category> categories, List<Tag> tags, MenuViewModel _viewModel, CommandInvoker invoker)
        {
            InitializeComponent();
            this.tasks = tasks;
            this.fileHandler = fileHandler;
            this._viewModel = _viewModel;
            this.invoker = invoker;
            LoadCategoriesToComboBox(categories);
            LoadTagsToComboBox(tags);
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            int taskId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            string title = TitleTextBox.Text;
            string description = DescriptionTextBox.Text;

            var selectedCategoryItem = (ComboBoxItem)CategoryComboBox.SelectedItem;
            var selectedCategory = selectedCategoryItem?.Tag as Category;

            var selectedTagItem = (ComboBoxItem)TagComboBox.SelectedItem;
            var selectedTag = selectedTagItem?.Tag as Tag;

            string priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString() ?? "Niski";
            DateTime selectedDate = TaskDatePicker.SelectedDate ?? DateTime.Now;
            bool isCompleted = IsCompletedCheckBox.IsChecked ?? false;

            if(title != "")
            {
                var zadanie = taskBuilder
               .setTitle(title)
               .setDescription(description)
               .setTag(selectedTag ?? new Tag("Inna"))
               .setCategory(selectedCategory ?? new Category("Inna"))
               .build();

                zadanie.Id = GetNextTaskId(tasks);
                zadanie.SetDeadline(selectedDate);
                zadanie.SetPriority(priority);
                if (isCompleted)
                {
                    zadanie.MarkAsDone();
                }

                invoker.AddCommand(new AddTask(tasks, zadanie, RefreshTasks));
                invoker.Execute();
                fileHandler.SaveTasksToFile("tasks.txt");
                _viewModel.UpdateHistory();
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Tytuł jest wymagany!");
            }
        }

        private void RefreshTasks()
        {
            TaskAdded?.Invoke(); // Powiadamiamy `DisplayTasksViewModel`
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public static int GetNextTaskId(List<Model.Task> tasks)
        {
            return tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        }

        // Odczytywanie kategorii:
        private void LoadCategoriesToComboBox(List<Category> categories)
        {
            CategoryComboBox.Items.Clear();
            foreach (var category in categories)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = category.Name,
                    Tag = category
                };
                CategoryComboBox.Items.Add(item);
            }
        }

        // Odczytywanie tagów:
        private void LoadTagsToComboBox(List<Tag> tags)
        {
            TagComboBox.Items.Clear();
            foreach (var tag in tags)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = tag.Name,
                    Tag = tag
                };
                TagComboBox.Items.Add(item);
            }
        }
    }
}
