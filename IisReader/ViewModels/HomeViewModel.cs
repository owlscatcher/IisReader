using IisReader.Commands;
using IisReader.Data;
using IisReader.Models;
using IisReader.Models.DataModels;
using IisReader.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Input;

namespace IisReader.ViewModels
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        struct ThemesName
        {
            public static string Light => "Light";
            public static string Dark => "Dark";
        }

        struct ColorName
        {
            public static string Light => "#FF303030";
            public static string Dark => "#FFFFFFFF";
        }

        struct MinMaxIconName
        {
            public static string Minimize => "Minimize";
            public static string Maximize => "Maximize";
        }
        struct WindowStateName
        {
            public static string Noraml => "Normal";
            public static string Maximize => "Maximized";
            public static string Minimize => "Minimized";

        }

        private string _actualTheme = ThemesName.Light;
        public string ActualTheme
        {
            get => _actualTheme;
            set
            {
                _actualTheme = value;
                OnPropertyChanged();
            }
        }

        private string _actualColor = ColorName.Light;
        public string ActualColor
        {
            get => _actualColor;
            set
            {
                _actualColor = value;
                OnPropertyChanged();
            }
        }

        private string _actualMinMaxIcon = MinMaxIconName.Maximize;
        public string ActualMinMaxIcon
        {
            get => _actualMinMaxIcon;
            set
            {
                _actualMinMaxIcon = value;
                OnPropertyChanged();
            }
        }

        private string _actualWindiowState = WindowStateName.Noraml;

        public string ActualWindiowState
        {
            get { return _actualWindiowState; }
            set
            {
                _actualWindiowState = value;
                OnPropertyChanged();
            }
        }

        public bool IsThemeDark { get; set; } = false;
        public bool IsMaximize { get; set; } = false;

        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get => items;
            set { items = value; OnPropertyChanged(); }
        }
        public ObservableCollection<DataRaw> DataRaws { get; set; }

        public System.Timers.Timer GridUpdateTimer { get; set; } = new()
        {
            Enabled = true,
            Interval = 1000,
            AutoReset = true
        };

        public HomeViewModel()
        {
            Items = GetItems();

            foreach (var item in Items)
                item.SetNormalizeDateTime();

            GridUpdateTimer.Elapsed += GridUpdateTimerElapsed;
        }

        private void GridUpdateTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var ctx = new IisdbContext();
            Items = GetItems();

            foreach (var item in Items)
            {
                item.SetNormalizeDateTime();
                item.Value = ctx.DataRaws.Where(x => x.ArchiveItemid == item.Id && x.SourceTime == item.LastTime).FirstOrDefault().Value;
            }
        }

        private ObservableCollection<Item> GetItems()
        {
            var repo = new ItemsRepository(new IisdbContext());
            return new ObservableCollection<Item>(repo.GetItems()
                .OrderBy(x => x.Id));
        }

        private ObservableCollection<DataRaw> GetDataRaws()
        {
            var repo = new DataRawsRepository(new IisdbContext());
            return new ObservableCollection<DataRaw>(repo.GetDataRaws()
                .OrderBy(x => x.ArchiveItemid)
                .ThenBy(n => n.SourceTime));
        }

        public ICommand LoadItems
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Items = GetItems();

                    foreach(var item in Items)
                        item.SetNormalizeDateTime();
                });
            }
        }
        public ICommand CloseWindow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (obj is not null)
                    {
                        Window homeWindow = obj as Window;
                        homeWindow.Close();
                    }
                });
            }
        }

        public ICommand ChangeTheme
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    IsThemeDark = !IsThemeDark;
                    ActualTheme = IsThemeDark ? ThemesName.Dark : ThemesName.Light;
                    ActualColor = IsThemeDark ? ColorName.Dark : ColorName.Light;
                });
            }
        }

        public ICommand ChangeWindowSize
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    IsMaximize = !IsMaximize;
                    ActualMinMaxIcon = IsMaximize ?  MinMaxIconName.Minimize : MinMaxIconName.Maximize;
                    ActualWindiowState = IsMaximize ? WindowStateName.Maximize : WindowStateName. Noraml;
                });
            }
        }
    }
}
