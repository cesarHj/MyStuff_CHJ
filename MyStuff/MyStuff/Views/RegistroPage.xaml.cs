using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyStuff.ViewModels;
using MyStuff.Clases;

namespace MyStuff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {

        UserViewModel UsuarioVM;

        public RegistroPage()
        {
            InitializeComponent();

            BindingContext = UsuarioVM = new UserViewModel();

        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
           await Navigation.PopAsync();
        }


        private bool EmailCorrecto(string email)
        {    
            return Tools.ValidarEmail(email);                               
        }

        private bool PasswordIguales()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(TxtPassword.Text.Trim()) && !string.IsNullOrEmpty(TxtPassword2.Text.Trim()))
            {
                if (TxtPassword.Text.Trim() == TxtPassword2.Text.Trim())
                {
                    R = true;
                }
            }

            return R;        
        }

        private bool HayDatosDeRegistro()
        {
            bool R = false;

            if (TxtUsuario.Text != null && TxtBackUpEmail.Text != null && TxtPassword.Text != null && TxtPassword2.Text != null)
            {
                R = true;
            }

            return R;
        }


        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            //primero tenemos que validar que el correo tenga el formato correcto y que además 
            //las contraseñas sean iguales 

            if (HayDatosDeRegistro() &&
                EmailCorrecto(TxtUsuario.Text.Trim()) &&
                EmailCorrecto(TxtBackUpEmail.Text.Trim()) &&
                PasswordIguales())
            {
                bool R = await UsuarioVM.GuardarUsuario(TxtUsuario.Text.Trim(), TxtNombreUsuario.Text.Trim(),
                           TxtPassword.Text.Trim(), TxtTelefono.Text.Trim(), TxtBackUpEmail.Text.Trim());

                if (R)
                {
                    await DisplayAlert("Aviso", "Usuario agregado correctamente", "OK");

                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Aviso", "Algo salió mal y el usuario no se agregó", "OK");
                }

            }
            else
            {
                //le indicamos al usuario en qué se está fallando
                if (TxtUsuario.Text != null && !EmailCorrecto(TxtUsuario.Text.Trim()))
                {
                    await DisplayAlert("Error de Validación", "El email de usuario no tiene el formato correcto", "OK");
                    return;
                }

                if (TxtBackUpEmail.Text != null && !EmailCorrecto(TxtBackUpEmail.Text.Trim()))
                {
                    await DisplayAlert("Error de Validación", "El email de respaldo no tiene el formato correcto", "OK");
                    return;
                }

                if (TxtPassword.Text != null && TxtPassword2.Text != null && !PasswordIguales())
                {
                    await DisplayAlert("Error de Validación", "Las contraseñas no son iguales", "OK");
                    return;
                }

            }
                      

        }
    }
}