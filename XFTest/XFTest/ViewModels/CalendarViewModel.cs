using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFTest.ViewModels
{
    public class CalendarViewModel : BindableBase, INotifyPropertyChanged
    {
        public CalendarViewModel(IDialogService dialogService, INavigationService navigationService)
        {

        }
    }
}
