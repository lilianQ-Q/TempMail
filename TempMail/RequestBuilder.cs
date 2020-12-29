using System;
using System.Collections.Generic;
using System.Text;

namespace TempMail
{
    public class RequestBuilder
    {
        #region Fields

        /// <summary>
        /// Represents the url which is going to receive the request.
        /// </summary>
        private string baseURL { get; set; }
        /// <summary>
        /// Dictionnary of getParameters with params name and params value.
        /// </summary>
        private Dictionary<string,string> getParamaters { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new Instance of the request builder.
        /// </summary>
        /// <param name="baseUrl"></param>
        public RequestBuilder(string baseUrl)
        {
            this.baseURL = baseUrl;
            this.getParamaters = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new paramaters inside the current object.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public bool AddParameters(string parameterName, string parameterValue)
        {
            bool result = false;
            if (!this.getParamaters.ContainsKey(parameterName))
            {
                this.getParamaters.Add(parameterName, parameterValue);
            }
            return (result);
        }

        /// <summary>
        /// Get the url with all setted get parameters.
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            string finalURL = this.baseURL + "?";
            foreach(KeyValuePair<string,string> element in this.getParamaters)
            {
                finalURL += element.Key + "=" + element.Value + "&";
            }
            return (finalURL);
        }

        #endregion
    }
}
