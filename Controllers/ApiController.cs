using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GreenLeaves.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreenLeaves.Controllers
{
    public class ApiController
    {
        public ApiController()
        {
        }

        [HttpGet]
        [Route("Api/SearchCity/{Cyti}")]
        public async Task<dynamic> SearchCityAsync(string Cyti)
        {
            string endpoint = "http://api.geonames.org/searchJSON?name_startsWith=";
            var Find = Cyti;
            string token = "&username=jmacsm&style=medium";
            string url = endpoint + Find + token;

            string response = await GetHttp(url);
            String[] value = response.Split("[");
            response = value[1].Insert(0, "[");
            int lastIdx = response.LastIndexOf(']');
            response = response.Remove(lastIdx+1, 1);

            Console.WriteLine(response);
              List<Root> root = JsonConvert.DeserializeObject<List<Root>>(response);

            return response;
        }

        public async Task<string> GetHttp(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();
            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
            return streamReader.ReadToEnd().Trim();
        }
        [HttpPost]
        [Route("Api/ContactSave")]
        public int ContactSave([FromBody] ContactoClass contacto)
        {
            try
            {
                using(GreenLeavesContext db=new GreenLeavesContext() )
                {
                    Contacto contact = new Contacto()
                    {
                        IdContact = 0,
                        Nombre = contacto.nombre,
                        Email = contacto.email,
                        Telefono = contacto.telefono,
                        Ciudad=contacto.ciudad,
                        Fecha=contacto.fecha



                    };
                    db.Contactos.Add(contact);
                    db.SaveChanges();
                }
                SetSenMailTest(contacto);

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
      


        public int SetSenMailTest(ContactoClass contacto)
        {
            try
            {

                var body = "<div style='width: 50rem;height:10rem; background-color: rgb(96, 236, 96); border-radius: 2rem !important'><h1 style='color:white;padding-top:4rem;padding-left:5rem;'>Green Leaves</h1></div><p style='font-size:30px'> Estimado <strong>"+contacto.nombre.ToUpper()+ " </strong>,<br><br>Hemos recibido sus datos y nos pondremos en contacto con usted en labrevedad posible. Enviaremos un correo con información a su cuenta: <strong>" + contacto.email.ToUpper() + "</strong><br><br><br><p style='text-align:right;font-size:30px'> Atte. <br><strong style='color:rgb(96, 236, 96) ;'>Green Leaves </strong><br>" + contacto.ciudad.ToUpper() + " <br> " + contacto.fecha.ToUpper()+ " <br></p></p></body>";


                MailMessage pruebamail = new MailMessage("greenleavestest@gmail.com", contacto.email, "Green Leaves Contact", body)
                {

                    IsBodyHtml = true

                };

              
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                SmtpClient smtprueba = new SmtpClient("smtp.gmail.com")
                {

                    EnableSsl = true,

                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("greenleavestest@gmail.com", "qwdkkphpspewwbdn")
                };

                smtprueba.Send(pruebamail);
                smtprueba.Dispose();
                return 1;



            }
            catch (Exception ex)
            {


                return 0;
            }





        }




    }

    public class ContactoClass
    {
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string fecha { get; set; }
        public string ciudad { get; set; }
    }

    public class Root
    {
        public string adminCode1 { get; set; }
        public string lng { get; set; }
        public int geonameId { get; set; }
        public string toponymName { get; set; }
        public string countryId { get; set; }
        public string fcl { get; set; }
        public int population { get; set; }
        public string countryCode { get; set; }
        public string name { get; set; }
        public string fclName { get; set; }
      //  public string  adminCodes1 { get; set; }
        public string countryName { get; set; }
        public string fcodeName { get; set; }
        public string adminName1 { get; set; }
        public string lat { get; set; }
        public string fcode { get; set; }
    }
}
