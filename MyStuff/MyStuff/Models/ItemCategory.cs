using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyStuff.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            Items = new HashSet<Item>();
        }

        public int ItemCategoryId { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Item> Items { get; set; }



        public async Task<bool> GuardarCategoria()
        {
            bool R = false;


            string RutaConsumoAPI = ObjetosGlobales.RutaProduccion + "ItemCategories";

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
