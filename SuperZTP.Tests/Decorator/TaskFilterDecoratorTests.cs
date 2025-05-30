using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using SuperZTP.Decorator;
using SuperZTP.Model;
using Task = SuperZTP.Model.Task;

namespace SuperZTP.Tests.Decorator
{
    public class TaskFilterDecoratorTests
    {
        private readonly List<Task> _tasks =
        [
            new()
            {
                Id = 1,
                Title = "Alpha Task",
                Description = "First",
                Category = new Category("CatA"),
                Tag = new Tag("Tag1"),
                Deadline = new DateTime(2025, 5, 1),
                Priority = "Low",
                IsDone = false
            },

            new()
            {
                Id = 2,
                Title = "Beta Task",
                Description = "Second",
                Category = new Category("CatB"),
                Tag = new Tag("Tag2"),
                Deadline = new DateTime(2025, 5, 2),
                Priority = "High",
                IsDone = true
            },

            new()
            {
                Id = 3,
                Title = "Gamma Task",
                Description = "Third",
                Category = new Category("CatA"),
                Tag = new Tag("Tag2"),
                Deadline = new DateTime(2025, 5, 1),
                Priority = "Medium",
                IsDone = false
            }
        ];

        [Fact]
        public void TitleTaskFilter_NullOrEmptyTitle_ReturnsAllTasks()
        {
            var filterNull = new TitleTaskFilter(null);
            var filterEmpty = new TitleTaskFilter(string.Empty);

            Assert.Equal(_tasks, filterNull.ApplyFilter(_tasks));
            Assert.Equal(_tasks, filterEmpty.ApplyFilter(_tasks));
        }

        [Fact]
        public void TitleTaskFilter_MatchingTitle_ReturnsFiltered()
        {
            var filter = new TitleTaskFilter("Alpha");
            var result = filter.ApplyFilter(_tasks).ToList();

            Assert.Single(result);
            Assert.Equal("Alpha Task", result[0].Title);
        }

        [Fact]
        public void CategoryTaskFilter_NullOrEmptyCategory_ReturnsAllTasks()
        {
            var filterNull = new CategoryTaskFilter(null);
            var filterEmpty = new CategoryTaskFilter(string.Empty);

            Assert.Equal(_tasks, filterNull.ApplyFilter(_tasks));
            Assert.Equal(_tasks, filterEmpty.ApplyFilter(_tasks));
        }

        [Fact]
        public void CategoryTaskFilter_MatchingCategory_ReturnsFiltered()
        {
            var filter = new CategoryTaskFilter("CatA");
            var result = filter.ApplyFilter(_tasks).ToList();

            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.Equal("CatA", t.Category.Name));
        }

        [Fact]
        public void TagTaskFilter_NullOrEmptyTag_ReturnsAllTasks()
        {
            var filterNull = new TagTaskFilter(null);
            var filterEmpty = new TagTaskFilter(string.Empty);

            Assert.Equal(_tasks, filterNull.ApplyFilter(_tasks));
            Assert.Equal(_tasks, filterEmpty.ApplyFilter(_tasks));
        }

        [Fact]
        public void TagTaskFilter_MatchingTag_ReturnsFiltered()
        {
            var filter = new TagTaskFilter("Tag2");
            var result = filter.ApplyFilter(_tasks).ToList();

            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.Equal("Tag2", t.Tag.Name));
        }

        [Fact]
        public void DueDateTaskFilter_NullDate_ReturnsAllTasks()
        {
            var filter = new DueDateTaskFilter(null);
            Assert.Equal(_tasks, filter.ApplyFilter(_tasks));
        }

        [Fact]
        public void DueDateTaskFilter_SpecificDate_ReturnsFiltered()
        {
            var target = new DateTime(2025, 5, 1);
            var filter = new DueDateTaskFilter(target);
            var result = filter.ApplyFilter(_tasks).ToList();

            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.Equal(target.Date, t.Deadline.Date));
        }

        [Theory]
        [InlineData(CompletionStatus.Default, 3)]
        [InlineData(CompletionStatus.ShowAll, 3)]
        [InlineData(CompletionStatus.Completed, 1)]
        [InlineData(CompletionStatus.NotCompleted, 2)]
        public void IsDoneFilter_VariousStatuses_FilterCorrectly(CompletionStatus status, int expectedCount)
        {
            var filter = new IsDoneFilter(status);
            var result = filter.ApplyFilter(_tasks).ToList();

            Assert.Equal(expectedCount, result.Count);
            if (status == CompletionStatus.Completed)
                Assert.All(result, t => Assert.True(t.IsDone));
            if (status == CompletionStatus.NotCompleted)
                Assert.All(result, t => Assert.False(t.IsDone));
        }

        [Theory]
        [InlineData(SortOptions.Alphabetical, new[] { "Alpha Task", "Beta Task", "Gamma Task" })]
        [InlineData(SortOptions.ReverseAlphabetical, new[] { "Gamma Task", "Beta Task", "Alpha Task" })]
        [InlineData(SortOptions.Date, new[] { 1, 3, 2 })]
        [InlineData(SortOptions.ReverseDate, new[] { 2, 1, 3 })]
        public void SortOptionDecorator_SortsCorrectly(SortOptions option, object expectedOrder)
        {
            var decorator = new SortOptionDecorator(option);
            var sorted = decorator.ApplyFilter(_tasks).ToList();

            if (expectedOrder is string[] titles)
            {
                Assert.Equal(titles, sorted.Select(t => t.Title));
            }
            else if (expectedOrder is int[] ids)
            {
                Assert.Equal(ids, sorted.Select(t => t.Id));
            }
        }

        [Fact]
        public void GroupTaskFilter_GroupByCategory_IncludesHeaders()
        {
            var filter = new GroupTaskFilter(GroupingOption.GroupByCategory);
            var result = filter.ApplyFilter(_tasks).ToList();

            
            Assert.Equal(5, result.Count);
            Assert.True(result[0].IsHeader && result[0].Title.Contains("CatA"));
            Assert.False(result[1].IsHeader);
            Assert.True(result[3].IsHeader && result[3].Title.Contains("CatB"));
        }

        [Fact]
        public void GroupTaskFilter_GroupByTag_IncludesHeaders()
        {
            var filter = new GroupTaskFilter(GroupingOption.GroupByTag);
            var result = filter.ApplyFilter(_tasks).ToList();

            Assert.Equal(5, result.Count);
            Assert.True(result[0].IsHeader && result[0].Title.Contains("Tag1"));
            Assert.False(result[1].IsHeader);
            Assert.True(result[2].IsHeader && result[2].Title.Contains("Tag2"));
        }

        [Fact]
        public void ChainedFilters_CombineBehaviors()
        {
            
            ITaskFilter combined = new IsDoneFilter(CompletionStatus.Completed,
                new CategoryTaskFilter("CatA"));
            var result = combined.ApplyFilter(_tasks).ToList();
            Assert.Empty(result);
        }
    }
}