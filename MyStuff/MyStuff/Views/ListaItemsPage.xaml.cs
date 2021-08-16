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
    public partial class ListaItemsPage : ContentPage
    {

        ItemViewModel VmItem;


        public ListaItemsPage()
        {
            InitializeComponent();

            BindingContext = VmItem = new ItemViewModel();
 

            VmItem.MyItem.UserId = ObjetosGlobales.MiUsuarioGlobal.UserId;

            LtsItems.ItemsSource = VmItem.ListarItems();

        }

        private void LtsItems_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}