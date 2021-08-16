using System;
using System.Collections.Generic;
using System.Text;
using MyStuff.Models;

using MyStuff.Clases;

using System.Threading.Tasks;

namespace MyStuff.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        public User MyUser { get; set; }

        public UserViewModel()
        {
            MyUser = new User();

            //TODO: Implementar los Command correspondientes. 
        }

        //Ahora implementamos una función para ser consumida desde la vista 

        public async Task<bool> GuardarUsuario(string Pusername, string Pname, string PuserPassword,
                                               string Pphone, string PbackupEmail)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {
                Crypto MiEncriptador = new Crypto();
                             
                //Usaremos SHA256 para generar un Hash que NO se pueda descifrar ni por nosotros los programadores
                string PassEncriptado = MiEncriptador.EncriptarEnUnSentido(PuserPassword);

                MyUser.Username = Pusername;
                MyUser.Name = Pname;
                MyUser.UserPassword = PassEncriptado;
                MyUser.Phone = Pphone;
                MyUser.BackupEmail = PbackupEmail;

                bool R = await MyUser.GuardarUsuario();

                return R;

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        
        }

        public async Task<User> ValidarUsuario(string Pusername, string PuserPassword)
        {
            if (IsBusy) return null;

            IsBusy = true;

            try
            {
                Crypto MiEncriptador = new Crypto();
                                
                string PassEncriptado = MiEncriptador.EncriptarEnUnSentido(PuserPassword);

                MyUser.Username = Pusername;
                MyUser.UserPassword = PassEncriptado;
               
                User R = await MyUser.ValidarUsuario();

                return R;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                IsBusy = false;
            }

        }


    }
}
