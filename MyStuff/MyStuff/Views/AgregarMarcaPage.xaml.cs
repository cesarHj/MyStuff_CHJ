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
    public partial class AgregarMarcaPage : ContentPage
    {


        BrandViewModel MarcaVM;

        public AgregarMarcaPage()
        {
            InitializeComponent();

            BindingContext = MarcaVM = new BrandViewModel();
        }

        private bool HayDatosDeRegistro()
        {
            bool R = false;

            if (TxtMarca.Text != null)
            {
                R = true;
            }

            return R;
        }
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (HayDatosDeRegistro() && TxtMarca.Text != null)
            {
                bool R = await MarcaVM.GuardarMarca(TxtMarca.Text.Trim());

                if (R)
                {
                    await DisplayAlert("Aviso", "Marca agregada correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Aviso", "Algo salió mal y la Marca no se agregó", "OK");
                }

            }
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}