using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStuff.ViewModels;
namespace MyStuff.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarLocalizacionPage : ContentPage
    {

        ItemLocalizationViewModel LocalizacionVM;

        public AgregarLocalizacionPage()
        {
            InitializeComponent();
             BindingContext = LocalizacionVM = new ItemLocalizationViewModel();
        }

        private bool HayDatosDeRegistro()
        {
            bool R = false;

            if (TxtLocalizacion.Text != null)
            {
                R = true;
            }

            return R;
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (HayDatosDeRegistro() && TxtLocalizacion.Text != null)
            {
                bool R = await LocalizacionVM.GuardarLugar(TxtLocalizacion.Text.Trim());

                if (R)
                {
                    await DisplayAlert("Aviso", "Localizacion agregada correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Aviso", "Algo salió mal y la Localizacion no se agregó", "OK");
                }

            }
        }


        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {

        }
        
        
  
    }
}
