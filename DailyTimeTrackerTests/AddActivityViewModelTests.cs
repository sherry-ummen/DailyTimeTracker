using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DailyTimeTracker.DatabaseLayer;
using DailyTimeTracker.Models;
using DailyTimeTracker.ViewModel;
using DailyTimeTracker.Views;
using Moq;
using NUnit.Framework;

namespace DailyTimeTrackerTests
{
    public class AddActivityViewModelTests
    {
        private Mock<IDatabaseService> _databaseService;
        private Mock<IClosable> _closable;

        private IEnumerable<ActivityCategory> categories = new List<ActivityCategory>() {
                new ActivityCategory() {Id = 1, Category = "Work" },
                new ActivityCategory() {Id = 2, Category = "Lunch" },
                new ActivityCategory() {Id = 3, Category = "Personal" }
            };

        [SetUp]
        public void Setup() {
            _databaseService = new Mock<IDatabaseService>();
            _databaseService.Setup(x => x.GetCategories()).Returns(() => Result.Ok(categories));
            _closable = new Mock<IClosable>();
            _closable.Setup(x => x.Close());
        }

        [Test]
        public void CheckCategories() {
            var addActivityViewModel = new AddActivityViewModel(_databaseService.Object);
            CollectionAssert.AreEqual(categories, addActivityViewModel.Categories);
        }

        [Test]
        public void AddActivity() {
            var addActivityViewModel = new AddActivityViewModel(_databaseService.Object);
            addActivityViewModel.Category = categories.First();
            addActivityViewModel.Description = "Some description";
            addActivityViewModel.OkCommand.Execute(_closable.Object);
            var result = addActivityViewModel.ReturnResult;
            Assert.That(result.Value.Description, Is.EqualTo("Some description"));
            Assert.That(result.Value.Category, Is.EqualTo(categories.First()));
        }
    }
}
