using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyStuff.Models;
namespace MyStuff.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {

        public Supplier Myproveedor { get; set; }

        //contructor
        public SupplierViewModel()
        {
            Myproveedor = new Supplier();
        }

        public async Task<bool> GuardarProveedor(string PSupname, string Pphone, string Pemail)
        {
            if (IsBusy) return false;

            IsBusy = true;

            try
            {

                Myproveedor.SupplierName = PSupname;
                Myproveedor.SupplierPhone = Pphone;
                Myproveedor.SupplierEmail = Pemail;

                Myproveedor.UserId = ObjetosGlobales.MiUsuarioGlobal.UserId;

                bool R = await Myproveedor.GuardarProveedor();

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
