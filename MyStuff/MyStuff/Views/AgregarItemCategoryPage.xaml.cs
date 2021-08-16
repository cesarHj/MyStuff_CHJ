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
    public partial class AgregarItemCategoryPage : ContentPage
    {

        ItemCategoryViewModel CategoriaVM;

        public AgregarItemCategoryPage()
        {
            InitializeComponent();

            BindingContext = CategoriaVM = new ItemCategoryViewModel();
        }


        private bool HayDatosDeRegistro()
        {
            bool R = false;

            if (TxtCategoria.Text != null)
            {
                R = true;
            }

            return R;
        }


        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            //primero tenemos que validar que el correo tenga el formato correcto y que además 
            //las contraseñas sean iguales 

            if (HayDatosDeRegistro() && TxtCategoria.Text != null)
            {
                bool R = await CategoriaVM.GuardarCategoria(TxtCategoria.Text.Trim());

                if (R)
                {
                    await DisplayAlert("Aviso", "Categoria agregada correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Aviso", "Algo salió mal y la Categoria no se agregó", "OK");
                }

            }
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}