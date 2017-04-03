/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DailyTimeTracker"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using DailyTimeTracker.BusinessLogic;
using DailyTimeTracker.DatabaseLayer;
using DailyTimeTracker.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DailyTimeTracker.ViewModel {
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IDatabaseService, DatabaseService>();
            SimpleIoc.Default.Register<IIdleTimeNotifier, IdleTimeNotifier>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddActivityViewModel>();
            SimpleIoc.Default.Register<AfterIdleQueryViewModel>();
        }

        public static MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static AddActivityViewModel AddActivityViewModel => ServiceLocator.Current.GetInstance<AddActivityViewModel>();

        public static AfterIdleQueryViewModel AfterIdleQueryViewModel(TimeSpan timeSpan) => new AfterIdleQueryViewModel(ServiceLocator.Current.GetInstance<IDatabaseService>(), timeSpan);

        public static void Cleanup() {
            // TODO Clear the ViewModels
        }
    }
}