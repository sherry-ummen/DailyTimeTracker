using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using DailyTimeTracker.Models;
using GalaSoft.MvvmLight;

namespace DailyTimeTracker.ViewModel
{
    public class MainViewModel : ViewModelBase{

        public ObservableCollection<Activity> Activities {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(){
            var category = new ActivityCategory(){Category = "Work", Id = 1};
            Activities = new ObservableCollection<Activity>(){
                new Activity() {Category = category,Description = "Something aljdsfhkjlahds fkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsdjflsjdlfjslkdfjlskdflkjsdlkfjslkdfjlksdflksdfjasd", StartTime = DateTime.Now},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(2)},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(5)},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now, EndTime = DateTime.Now.AddMilliseconds(30)},
            };
        }
    }
}