using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyStuff.Models;
namespace MyStuff.ViewModels
{
    public class BrandViewModel : BaseViewModel
    {
        public Brand Thebrand { get; set; }

        //contructor
        public BrandViewModel()
        {
            Thebrand = new Brand();
        }

        public async Task<bool> GuardarMarca(string PDescr)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {

                Thebrand.BrandName = PDescr;

                Thebrand.UserId = ObjetosGlobales.MiUsuarioGlobal.UserId;

                bool R = await Thebrand.GuardarMarca();

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