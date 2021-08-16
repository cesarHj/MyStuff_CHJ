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
    public partial class AgregarProveedorPage : ContentPage
    {
         SupplierViewModel ProveedorVM;

        public AgregarProveedorPage()
        {
            InitializeComponent();

            BindingContext = ProveedorVM = new SupplierViewModel();
        }


        private bool HayDatosDeRegistro()
        {
            bool R = false;

            if (TxtNombre.Text != null && TxtTelefono.Text != null && TxtCorreo.Text != null)
            {
                R = true;
            }

            return R;
        }



        private async void BtnGuardarClicked(object sender, EventArgs e)
        {
            if (HayDatosDeRegistro() && TxtNombre.Text != null && TxtTelefono.Text != null && TxtCorreo.Text != null)
            {
                bool R = await ProveedorVM.GuardarProveedor(TxtNombre.Text.Trim(), TxtTelefono.Text.Trim(), TxtCorreo.Text.Trim());

                if (R)
                {
                    await DisplayAlert("Aviso", "Proveedor agregado correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Aviso", "Algo salió mal y el Proveedor no se agregó", "OK");
                }

            }
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}