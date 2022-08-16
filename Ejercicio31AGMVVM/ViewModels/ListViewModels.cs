using Ejercicio31AGMVVM.Models;
using Ejercicio31AGMVVM.Services;
using Ejercicio31AGMVVM.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ejercicio31AGMVVM.ViewModels
{
    public class ListViewModels : BaseViewModels
    {
        private List<Employee> _listEmployee;
        ServicesEmployee services;

        public List<Employee> ListEmployee
        {
            get { return _listEmployee; }
            set
            {
                _listEmployee = value;
                OnPropertyChanged();
            }
        }

        public ListViewModels()
        {
            services = new ServicesEmployee();
            UpdateEmployeeCommand = new Command<Employee>(async (Employee) => await UpdateEmployee(Employee));
            DeleteEmployeeCommand = new Command<Employee>(async (Employee) => await DeleteEmployee(Employee));
        }

        public async Task DeleteEmployee(Employee employee)
        {
            bool response = await services.DeleteEmloyee(employee.Key);

            if (response)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "successfully deleted", "Ok");

                recharge();
            }
        }

        private async Task UpdateEmployee(Employee employee)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddEmployeePage("Update", employee));
        }

        public async void recharge()
        {
            ListEmployee = await services.ListEmployee();
           
        }

        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }

    }

}
