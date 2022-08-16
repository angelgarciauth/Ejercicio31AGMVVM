using Ejercicio31AGMVVM.Models;
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
    public partial class AddEmployeePage : ContentPage
    {
       
        public AddEmployeePage(string option = "Save", Employee employee = null)
        {
            InitializeComponent();
            if (option.Equals("Save"))
            {
                BindingContext = new AddViewModels(imgPerson, option, employee);
            }
            else
            {
                BindingContext = new AddViewModels(imgPersonB64, option, employee);
                
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}