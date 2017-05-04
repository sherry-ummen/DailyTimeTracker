using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace DailyTimeTracker.ViewModel {
    public class MenuItem : ViewModelBase {
        private object _icon;
        private string _text;
        private RelayCommand _command;
        private Uri _navigationDestination;

        public object Icon {
            get => this._icon;
            set => this.Set(ref this._icon, value);
        }

        public string Text {
            get => this._text;
            set => this.Set(ref this._text, value);
        }

        public ICommand Command {
            get => this._command;
            set => this.Set(ref this._command, (RelayCommand)value);
        }

        public Uri NavigationDestination {
            get => this._navigationDestination;
            set => this.Set(ref this._navigationDestination, value);
        }

        public bool IsNavigation => this._navigationDestination != null;
    }
}
