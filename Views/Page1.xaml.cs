using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp3.ViewModels;

namespace MyApp3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            BindingContext = new Page1ViewModel();
        }
    }
}