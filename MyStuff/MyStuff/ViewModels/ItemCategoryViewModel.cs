using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyStuff.Models;
namespace MyStuff.ViewModels
{
    public class ItemCategoryViewModel : BaseViewModel
    {

        public ItemCategory TheCategory { get; set; }

        //contructor
        public ItemCategoryViewModel()
        {
            TheCategory = new ItemCategory();
        }

        public async Task<bool> GuardarCategoria(string PDescr)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {
                TheCategory.Category = PDescr;

                TheCategory.UserId = ObjetosGlobales.MiUsuarioGlobal.UserId;

                bool R = await TheCategory.GuardarCategoria();

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
