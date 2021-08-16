using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace MyStuff.Models
{
    public partial class Item
    {

        public Item()
        {
            Multimedia = new HashSet<Multimedium>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemCost { get; set; }
        public string SerialNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal ExRate { get; set; }
        public int? BrandId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemLocalizationId { get; set; }
        public int? SupplierId { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public string DisplayImageUri { get; set; }
        public string DisplayImageUrilowRes { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemLocalization ItemLocalization { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Multimedium> Multimedia { get; set; }

        public ObservableCollection<Item> ListarItems()
        {
           //esta funcion entrega un listado que es "dibujable" en el UI
           //El listado tiene forma de Item ya que los atributos deberan ser Vinculables al control
           //ListView
                       
            string SufijoRuta = string.Format("Items/GetItemsList?UserId={0}", UserId);

            string RutaConsumoAPI = ObjetosGlobales.RutaProduccion + SufijoRuta;

            var client = new RestClient(RutaConsumoAPI);

            var request = new RestRequest(Method.GET);

            //agregamos la info de seguridad 
            request.AddHeader(ObjetosGlobales.ApiKeyName, ObjetosGlobales.ApiKeyValue);

            //ejecuta de forma síncrona la consulta contra el API
            IRestResponse Respuesta = client.Execute(request);

            HttpStatusCode CodigoRespuesta = Respuesta.StatusCode;

            var ListaItems = JsonConvert.DeserializeObject<ObservableCollection<Item>>(Respuesta.Content);

            if (CodigoRespuesta == HttpStatusCode.OK)
            {
                return ListaItems;
            }
            else
            {
                return null;
            }
                      
        }

        public async Task<bool> GuardarItem()
        {
            bool R = false;

            string RutaConsumoAPI = ObjetosGlobales.RutaProduccion + "Items";

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