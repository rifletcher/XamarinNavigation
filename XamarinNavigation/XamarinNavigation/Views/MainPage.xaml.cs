using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinNavigation.ViewModels;

namespace XamarinNavigation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();
            var mainPageViewModel = ServiceLocator.Current.GetInstance<MainPageViewModel>();
            BindingContext = mainPageViewModel;
        }
    }
}