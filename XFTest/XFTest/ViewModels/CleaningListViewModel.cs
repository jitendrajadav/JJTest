using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XFTest.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace XFTest.ViewModels
{
    public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
        public DelegateCommand ShowDialogCommand { get; set; }
        readonly IList<CleaningList> source;
        CleaningList selectedMonkey;
        int selectionCount = 1;

        public ObservableCollection<CleaningList> Monkeys { get; private set; }
        public IList<CleaningList> EmptyMonkeys { get; private set; }

        public CleaningList SelectedMonkey
        {
            get
            {
                return selectedMonkey;
            }
            set
            {
                if (selectedMonkey != value)
                {
                    selectedMonkey = value;
                }
            }
        }

        ObservableCollection<object> selectedMonkeys;
        public ObservableCollection<object> SelectedMonkeys
        {
            get
            {
                return selectedMonkeys;
            }
            set
            {
                if (selectedMonkeys != value)
                {
                    selectedMonkeys = value;
                }
            }
        }

        public string SelectedMonkeyMessage { get; private set; }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        public ICommand MonkeySelectionChangedCommand => new Command(MonkeySelectionChanged);

        public DelegateCommand ShowCalanderCommand { get; set; }

        private bool _ShowCal = false;
        /// <summary>
        /// SLRow1
        /// </summary>
        public bool ShowCal
        {
            get { return this._ShowCal; }
            set { this.SetProperty(ref this._ShowCal, value); }
        }

        public CleaningListViewModel( IDialogService dialogService, INavigationService navigationService)
        {
            //ShowCalanderCommand = new DelegateCommand(() =>
            //{
            //    ShowCal = true;
            //});

            source = new List<CleaningList>();
            CreateMonkeyCollection();

            selectedMonkey = Monkeys.Skip(3).FirstOrDefault();
            MonkeySelectionChanged();

            SelectedMonkeys = new ObservableCollection<object>()
            {
                Monkeys[1], Monkeys[3], Monkeys[4]
            };

            ShowDialogCommand = new DelegateCommand(async () =>
            {

                await navigationService.NavigateAsync("CalendarDialogPopUpPage");
                //  var param = new DialogParameters();
                //    param.Add("Message", "I'm a dialog");
                //  await  dialogService.ShowDialog("CalendarDialogView", param, CloseDialogCallback);
            });



        }
        void CloseDialogCallback(IDialogResult dialogResult)
        {

        }

        void CreateMonkeyCollection()
        {
            source.Add(new CleaningList
            {
                Name = "Michael Jensen",
                Location = "Udfort",
                Details = "Baboons are African and Arabian",
                ShortDetail = "Ny",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                Color = "#25a87b",
                dTime = "08:00",
                dMinute = "15 min",
                Address = "johan vj 52,4000 Rosklide",
                Distance = ""
            });

            source.Add(new CleaningList
            {
                Name = "Ida Svendsen",
                Location = "I gang",
                Details = "The capuchin monkeys are New World",
                ShortDetail = " ",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg",
                Color = "#f5c709",
                dTime = "08:30 / 08:00 - 10:00",
                dMinute = "40 min",
                Address = "Radhusvejen 12,4000 Rosklide",
                Distance = "1.9 km"
            });

            source.Add(new CleaningList
            {
                Name = "Mikkel Olsen",
                Location = "Afvist",
                Details = "The blue monkey or diademed monkey",
                ShortDetail = " ",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg",
                Color = "#ef6565",
                dTime = "09:00",
                dMinute = "70 min",
                Address = "Radhusvejen 12,4000 Rosklide",
                Distance = "2,3 km"
            });

            source.Add(new CleaningList
            {
                Name = "Charlotte Jensen",
                Location = "Todo",
                Details = "The squirrel monkeys are the New",
                ShortDetail = " ",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg",
                Color = "#4e77d6",
                dTime = "08:30 / 08:00 - 10:00",
                dMinute = "70 min",
                Address = "Radhusvejen 12,4000 Rosklide",
                Distance = "2,3 km"
            });

            source.Add(new CleaningList
            {
                Name = "Bent Jorgensen",
                Location = "Todo",
                Details = "The golden lion tamarin also",
                ShortDetail = " ",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg",
                Color = "#4e77d6",
                dTime = "08:30 / 08:00 - 10:00",
                dMinute = "70 min",
                Address = "Radhusvejen 12,4000 Rosklide",
                Distance = "2,3 km"
            });

            Monkeys = new ObservableCollection<CleaningList>(source);
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where(monkey => monkey.Name.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var monkey in source)
            {
                if (!filteredItems.Contains(monkey))
                {
                    Monkeys.Remove(monkey);
                }
                else
                {
                    if (!Monkeys.Contains(monkey))
                    {
                        Monkeys.Add(monkey);
                    }
                }
            }
        }

        void MonkeySelectionChanged()
        {
            SelectedMonkeyMessage = $"Selection {selectionCount}: {SelectedMonkey.Name}";
            OnPropertyChanged("SelectedMonkeyMessage");
            selectionCount++;
        }

        void ShowCalander()
        {
            ShowCal = !ShowCal;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
