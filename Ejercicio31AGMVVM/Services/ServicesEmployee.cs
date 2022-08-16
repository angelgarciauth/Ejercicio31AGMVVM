using Ejercicio31AGMVVM.BDFirebase;
using Ejercicio31AGMVVM.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio31AGMVVM.Services
{
    public class ServicesEmployee
    {
        public async Task<bool> AddEmployee(Employee employee )
        {
            bool response = false;
            try
            {
                await Conexion.clienteFirebase
                    .Child("Employee")
                    .PostAsync(new Employee()
                    {  
                        Name = employee.Name,
                        LastName = employee.LastName,
                        Address = employee.Address,
                        Age = employee.Age,
                        Position = employee.Position,
                        Photo = employee.Photo
                    });
                response = true;
            }
            catch (Exception e)
            {
                response = false;
                Debug.WriteLine(e.Message);
            }
            return response;
        }

        public async Task<bool> UpdateEmployee(Employee employee, string key)
        {
            bool response = false;

            try
            {
                await Conexion.clienteFirebase
                              .Child("Employee")
                              .Child(key)
                              .PutAsync(employee);
                response = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                response = false;
            }
            return response;
        }

        public async Task<bool> DeleteEmloyee(string key)
        {
            bool response = false;
            try
            {
                await Conexion.clienteFirebase.Child("Employee").Child(key).DeleteAsync();
                response = true;
            }catch(Exception e)
            {
                response = false;
            }
            return response;
         }

        public async Task<List<Employee>> ListEmployee()
        {
            try
            {
                var data = (await Conexion.clienteFirebase
                                      .Child("Employee")
                                      .OnceAsync<Employee>()).Select(item => new Employee
                                      {
                                          Key = item.Key,
                                          Name = item.Object.Name,
                                          LastName = item.Object.LastName,
                                          Address = item.Object.Address,
                                          Age = item.Object.Age,
                                          Position = item.Object.Position,
                                          Photo = item.Object.Photo
                                      }).ToList();
                return data;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
    }

}
