using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyStuff.Models;
namespace MyStuff.ViewModels
{
    public class ItemLocalizationViewModel : BaseViewModel
    {

        public ItemLocalization MyLocation { get; set; }

        //contructor
        public ItemLocalizationViewModel()
        {
            MyLocation = new ItemLocalization();
        }


        public async Task<bool> GuardarLugar(string PLocated)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {

                MyLocation.Localization = PLocated;

                MyLocation.UserId = ObjetosGlobales.MiUsuarioGlobal.UserId;

                bool R = await MyLocation.GuardarLocated();

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
