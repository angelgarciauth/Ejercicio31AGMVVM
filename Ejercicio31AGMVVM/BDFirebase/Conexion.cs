using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercicio31AGMVVM.BDFirebase
{
    public class Conexion
    {
        public static FirebaseClient clienteFirebase = new FirebaseClient("https://pm2crudmvvm-default-rtdb.firebaseio.com/");
    }
}
