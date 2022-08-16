using Ejercicio31AGMVVM.Models;
using Ejercicio31AGMVVM.Services;
using Ejercicio31AGMVVM.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ejercicio31AGMVVM.ViewModels
{
    public class AddViewModels : BaseViewModels
    {
        private string _Name;
        private string _LastName;
        private string _Age;
        private string _Address;
        private string _Position;
        private string _Photo;
        Image imageEmployee;
        ServicesEmployee services;
        private string option;
        private string key;
        private bool _IsImageDefault;
        private bool _IsImageUpdate;

        #region obj
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }

        }

        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return _Position; }
            set
            {
                _Position = value;
                OnPropertyChanged();
            }
        }

        public string Photo
        {
            get { return _Photo; }
            set
            {
                _Photo = value;
                OnPropertyChanged();
            }
        }

        public bool IsImageDefault
        {
            get { return _IsImageDefault; }
            set
            {
                _IsImageDefault = value;
                OnPropertyChanged();
            }
        }

        public bool IsImageUpdate
        {
            get { return _IsImageUpdate; }
            set
            {
                _IsImageUpdate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AddViewModels(Image imageP, string optionR, Employee employeeR)
        {
            imageEmployee = imageP;
            services = new ServicesEmployee();
            option = optionR;

            if (option.Equals("Update"))
            {
                fillUpdate(employeeR);
                IsImageDefault = false;
                IsImageUpdate = true;
            }
            else
            {
                IsImageDefault = true;
                IsImageUpdate = false;
            }

            CapturePhotoCommand = new Command(async () => await CaptureImage());
            AddCommand = new Command(() => AddEmployee());
            ListCommand = new Command(() => ListEmployee());
        }

        private async void AddEmployee()
        {
            string response = validate();
            if (!response.Equals("ok"))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", response, "OK");
                return;
            }

            Employee employee = new Employee()
            {
                Name = Name,
                LastName = LastName,
                Age = Age,
                Address = Address,
                Position = Position,
                Photo = Photo

            };

            if (option.Equals("Update"))
            {
                bool conf = await services.UpdateEmployee(employee, key);
                if (conf)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Updated successfully", "OK");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Failed to Update", "OK");
                }
            }
            else
            {
                bool conf = await services.AddEmployee(employee);
                if (conf)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "successfully added", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Failed to Add", "OK");
                }
            }
            
        }

        private async void ListEmployee()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ListEmployeePage());
        }

        public void clean()
        {
            Name = "";
            LastName = "";
            Age = "";
            Address = "";
            Position = "";
            Photo = "";
            imageEmployee.Source = "person.png";
        }

        private string validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "Por favor ingrese un nombre";
            }else if (string.IsNullOrEmpty(LastName))
            {
                return "Por favor ingrese un apellido";
            }else if (string.IsNullOrEmpty(Age))
            {
                return "Por favor ingrese su edad";
            }else if (string.IsNullOrEmpty(Address))
            {
                return "Por favor ingrese su direccion";
            }else if (string.IsNullOrEmpty(Position))
            {
                return "Por favor ingrese su edad";
            }else if (string.IsNullOrEmpty(Photo))
            {
                return "Por favor ingrese una fotografia";
            }

            return "ok";
        }

        private void fillUpdate(Employee employeeR)
        {
            Name = employeeR.Name;
            LastName = employeeR.LastName;
            Age = employeeR.Age;
            Address = employeeR.Address;
            Position = employeeR.Position;
            Photo = employeeR.Photo;
            key = employeeR.Key;
        }

        private Task CaptureImage()
        {
            GetImageFromCamera();
            return Task.CompletedTask;
        }

        private async void GetImageFromCamera()
        {
            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                });

                if (file == null)
                    return;

                imageEmployee.Source = ImageSource.FromStream(() => { return file.GetStream(); });
                byte[] byteArray = File.ReadAllBytes(file.Path);
                Photo = System.Convert.ToBase64String(byteArray);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Se produjo un error.", "Ok");
            }
        }


        public ICommand CapturePhotoCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ListCommand { get; set; }
    }
}
