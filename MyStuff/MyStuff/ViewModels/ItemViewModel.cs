using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MyStuff.Models;

namespace MyStuff.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        //atribs del vm 

        public Item MyItem { get; set; }

        public Currency MYcurrency{ get; set; }

        public List <Currency> CurrenciesList { get; set; }

        //contructor
        public ItemViewModel()
        {
            MyItem = new Item();

            MYcurrency = new Currency();

            CurrenciesList = new List<Currency>();

            CurrenciesList = ListarMonedas();
        }

        //métodos y funciones
        public ObservableCollection<Item> ListarItems()
        {
            if (IsBusy)
            {
                return null;
            }
            else
            {
                try
                {
                    return MyItem.ListarItems();
                }
                catch (Exception ex)
                {

                    return null;
                }
                finally
                {
                    IsBusy = false;
                }
            }       
        }


        private List<Currency> ListarMonedas()
        {
            if (IsBusy)
            {
                return null;
            }
            else
            {
                try
                {
                    return MYcurrency.ListarMonedas();
                }
                catch (Exception ex)
                {

                    return null;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task<bool> GuardarItem(string nombre, string Descripcion, decimal Costo,
                                            string SerialN, string NuFact, decimal Depreciacion)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {

                MyItem.ItemName = nombre;
                MyItem.ItemDescription = Descripcion;
                MyItem.ItemCost = Costo;
                MyItem.SerialNumber = SerialN;
                MyItem.InvoiceNumber = NuFact;
                MyItem.ExRate = Depreciacion;

                MyItem.UserId = ObjetosGlobales.MiUsuarioGlobal.UserId;

                MyItem.CurrencyId = MYcurrency.CurrencyId;

                bool R = await MyItem.GuardarItem();

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

    }
}
