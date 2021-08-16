using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStuff.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStuff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarItemPage : ContentPage
    {

        ItemViewModel ItemVM;
        public AgregarItemPage()
        {
            InitializeComponent();

            BindingContext = ItemVM = new ItemViewModel();

            CboMoneda.ItemsSource = ItemVM.CurrenciesList;
        }

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            bool R = await ItemVM.GuardarItem(TxtNombre.Text.Trim(), TxtDescripcion.Text.Trim(),
                Convert.ToDecimal(TxtCosto.Text.Trim()), TxtSerie.Text.Trim(), TxtDepreciacion.Text.Trim(),
                Convert.ToDecimal(TxtFact.Text.Trim()));

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

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}