using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyStuff.ViewModels;
using MyStuff.Models;

namespace MyStuff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresoPage : ContentPage
    {

        UserViewModel Vm;

        public IngresoPage()
        {
            InitializeComponent();

            BindingContext = Vm = new UserViewModel();

        }

        private void CmdVerPassword(object sender, ToggledEventArgs e)
        {
            if (SwVerPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void CmdIngresar(object sender, EventArgs e)
        {

            User R = await Vm.ValidarUsuario(TxtUsuario.Text.Trim(), TxtPassword.Text.Trim());

            if (R != null && R.UserId > 0)
            {

                await Navigation.PushAsync(new SelectorDeAccionPage());

            }
            else
            {
                await DisplayAlert("Informacion", "Usuario Incorrecto", "OK");
            }


        }

        private async void CmdRegistro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPage());
        }
    }
}