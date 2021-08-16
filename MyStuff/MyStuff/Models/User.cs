 using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace MyStuff.Models
{
    public partial class User
    {
        public User()
        {
            Brands = new HashSet<Brand>();
            ItemCategories = new HashSet<ItemCategory>();
            ItemLocalizations = new HashSet<ItemLocalization>();
            Items = new HashSet<Item>();
            Suppliers = new HashSet<Supplier>();

            //Estos atributos deben llevar un valor ya que en tabla de db no se permite null. 
            //Y para evitar un error a nivel del API es mejor inicializarlos acá 
            //(Aunque también se podría hacer en el API) 

            UserStatusId = 1;

            LastLogin = DateTime.Now.Date;

        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string UserPassword { get; set; }
        public string Phone { get; set; }
        public string BackupEmail { get; set; }
        public DateTime LastLogin { get; set; }
        public int UserStatusId { get; set; }

        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
        public virtual ICollection<ItemLocalization> ItemLocalizations { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }


        //Escribimos las funciones que se comunicarán con el API. 
        //Similar a lo que hucimos en progra 5. 
        //Estas funciones se DEBEN explicar a traves de los diagramas de casos de uso. 


        //función para agregar un usuario a la base de datos. 
        //Siguiendo la dinámica del diagrama de casos de uso, primero deberíamos consultar
        //si el usuario que estamos por agregar ya existe o no. 
        //NOTA: también esta consulta se podría hacer en "caliente" al momento de digitar el correo 
        //en el UI. 

        public async Task<bool> GuardarUsuario()
        {
            bool R = false;

            //tomamos la info base (prefijo) de la ruta del API y agregarmos el sufijo correspondiente
            //para completar  la Ruta de consumo. (paso 1.3.3.1 del ejemplo de secuencia)

            string RutaConsumoAPI = ObjetosGlobales.RutaProduccion + "users";

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

        public async Task<User> ValidarUsuario()
        {
            User R = new User();

            //tomamos la info base (prefijo) de la ruta del API y agregarmos el sufijo correspondiente
            //para completar  la Ruta de consumo. (paso 1.3.3.1 del ejemplo de secuencia)

            string SufijoRuta = string.Format("Users/ValidateUser2?email={0}&pass={1}", this.Username, this.UserPassword);
                       
            string RutaConsumoAPI = ObjetosGlobales.RutaProduccion + SufijoRuta;
                      
            var client = new RestClient(RutaConsumoAPI);

            var request = new RestRequest(Method.GET);

            //agregamos la info de seguridad 
            request.AddHeader(ObjetosGlobales.ApiKeyName, ObjetosGlobales.ApiKeyValue);
            
            //ejecuta de forma asíncrona la consulta contra el API
            IRestResponse Respuesta = await client.ExecuteAsync(request);

            HttpStatusCode CodigoRespuesta = Respuesta.StatusCode;

            if (CodigoRespuesta == HttpStatusCode.OK)
            {

                var MyUser = JsonConvert.DeserializeObject<User>(Respuesta.Content);

                R = MyUser;
            }

            return R;
        }

    }
}
