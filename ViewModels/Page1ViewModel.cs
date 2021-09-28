using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmHelpers;
using System.Linq;
using System.Collections.Generic;
using Command = MvvmHelpers.Commands.Command;
using MyApp3.Models;
using Xamarin.Forms;

namespace MyApp3.ViewModels
{
    public class Page1ViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Money> Money { get; set; }
        public ObservableRangeCollection<Grouping<string, Money>> MoneyGroups { get; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Money> FavoriteCommand { get; }
        public AsyncCommand<Money> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public Page1ViewModel()
        {
            Title = "Money Slots";

            Money = new ObservableRangeCollection<Money>();
            MoneyGroups = new ObservableRangeCollection<Grouping<string, Money>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Money>(Favorite);
            SelectedCommand = new AsyncCommand<Money>(Selected);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }

        async Task Favorite(Money money)
        {
            if (money == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Favorite", money.standing, "OK");
        }

        Money previouslySelected;
        Money selectedMoney;
        public Money SelectedMoney
        {
            get => selectedMoney;
            set => SetProperty(ref selectedMoney, value);
        }

        async Task Selected(object args)
        {
            var money = args as Money;
            if (money == null)
                return;
            SelectedMoney = null;

            await Application.Current.MainPage.DisplayAlert("Selected", money.standing, "OK");
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Money.Clear();
            LoadMore();

            IsBusy = false;
        } 
        void LoadMore()
        {
            if (Money.Count >= 20)
                return;

            var image = "https://content.usaa.com/mcontent/static_assets/Media/mkt_mainBnnr_bckgrnd_resize_1263.jpg";
            Money.Add(new Models.Money { amount = 0, standing = "n/a", Image = image });
            Money.Add(new Models.Money { amount = 0, standing = "n/a", Image = image });
            Money.Add(new Models.Money { amount = 0, standing = "n/a", Image = image });
            Money.Add(new Models.Money { amount = 0, standing = "n/a", Image = image });
            Money.Add(new Models.Money { amount = 0, standing = "n/a", Image = image });

            MoneyGroups.Clear();

            MoneyGroups.Add(new Grouping<string, Money>("good", Money.Where(c => c.amount <= 50)));
            MoneyGroups.Add(new Grouping<string, Money>("good", Money.Where(c => c.amount > 50)));
        }

        void DelayLoadMore()
        {
            if (Money.Count <= 10)
                return;

            LoadMore();
        }

        void Clear()
        {
            Money.Clear();
            MoneyGroups.Clear();
        }
    }
}
