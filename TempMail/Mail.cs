using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;

namespace TempMail
{
    class Mail
    {
        #region Fields

        /// <summary>
        /// Current mail address.
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Todo : List of mails (new class ?).
        /// </summary>
        public Dictionary<string, List<string>> mails { get; set; }

        /// <summary>
        /// Api url.
        /// </summary>

        private static string APIURL = "https://www.1secmail.com/api/v1/";

        #endregion

        #region Singleton
        private static Mail _instance;
        /**
         * Ne pas mettre de singleton c'est inutile
         **/
        public static Mail GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Mail();
            }
            return (_instance);
        }
        #endregion

        #region Constructor
        private Mail()
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Change the current mail address.
        /// </summary>
        public void SetNewMail()
        {
            RequestBuilder builder = new RequestBuilder(APIURL);
            builder.AddParameters("action", "genRandomMailbox");
            string getRequest = builder.GetUrl();
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getRequest);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            JArray jsonData = JArray.Parse(html);
            this.address = jsonData[0].ToString();
        }

        /// <summary>
        /// Check if the current mailbox has received mails.
        /// </summary>
        public void CheckMail()
        {
            bool isOk = false;
            int count = 0;
            string html = "";
            JArray jsonData = new JArray();
            RequestBuilder builder = new RequestBuilder(APIURL);
            builder.AddParameters("action", "getMessages");
            builder.AddParameters("login", this.address.Split('@')[0]);
            builder.AddParameters("domain", this.address.Split('@')[1]);

            string getRequest = builder.GetUrl();
            Console.WriteLine("[*] Sending request to api...");
            while (!isOk && count <= 100)
            {
                /*
                 * Utiliser les thread.Sleep() à la place. pour faire 10 tentatives
                 */
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getRequest);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                jsonData = JArray.Parse(html);
                if(jsonData.Count > 0)
                {
                    isOk = true;
                }
                else
                {
                    Console.Write(".");
                    count++;
                }
            }
            if (isOk)
            {
                Console.WriteLine("\n[*] " + jsonData.Count + " mail(s) trouvé(s) sur l'adresse courante !");
                foreach (JObject element in jsonData)
                {
                    string subject = element.Value<string>("subject");
                    string from = element.Value<string>("from");
                    string date = element.Value<string>("date");
                    Console.WriteLine("[-] " + subject + " from " + from + " the " + date);
                }
            }
            else
            {
                Console.WriteLine("\n[-] Aucun mails trouvés sur l'adresse courante.");
            }

        }
        #endregion
    }
}
