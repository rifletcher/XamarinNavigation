using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinNavigation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1View : ContentPage
    {
        public Page1View()
        {
            InitializeComponent();
            var page1ViewModel = ServiceLocator.Current.GetInstance<Page1ViewModel>();
            BindingContext = page1ViewModel;
        }
    }
}