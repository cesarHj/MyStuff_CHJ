using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyStuff.Models
{
    public partial class Supplier
    {

        public Supplier()
        {
            Items = new HashSet<Item>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Item> Items { get; set; }


        public async Task<bool> GuardarProveedor()
        {
            bool R = false;

            //tomamos la info base (prefijo) de la ruta del API y agregarmos el sufijo correspondiente
            //para completar  la Ruta de consumo. (paso 1.3.3.1 del ejemplo de secuencia)

            string RutaConsumoAPI = ObjetosGlobales.RutaProduccion + "Suppliers";

            var client = new RestClient(RutaConsumoAPI);

            var request = new RestRequest(Method.POST);

            //agregamos la info de seguridad
            request.AddHeader(ObjetosGlobales.ApiKeyName, ObjetosGlobales.ApiKeyValue);
            request.AddHeader("Content-Type", "application/json");

            //ahora serializamos esta clase ya que hemos definido que se enviará un json
            string ClaseEnJson = JsonConvert.SerializeObject(this);

            request.AddParameter("application/json", ClaseEnJson, ParameterType.RequestBody);

            //ejecuta de forma asíncrona la consulta contra el API
            IRestResponse Respuesta = await client.ExecuteAsync(request);

            HttpStatusCode CodigoRespuesta = Respuesta.StatusCode;

            if (CodigoRespuesta == HttpStatusCode.Created)
            {
                R = true;
            }

            return R;
        }
    }
}
