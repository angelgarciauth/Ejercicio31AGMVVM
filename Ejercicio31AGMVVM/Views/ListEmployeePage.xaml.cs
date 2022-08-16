using Ejercicio31AGMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio31AGMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEmployeePage : ContentPage
    {
        ListViewModels listViewModels;
        public ListEmployeePage()
        {
            InitializeComponent();
            listViewModels = new ListViewModels();
            BindingContext = listViewModels;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listViewModels.recharge();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEmployeePage());
        }
    }
}