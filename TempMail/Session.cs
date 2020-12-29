using System;
using System.Collections.Generic;
using System.Text;

namespace TempMail
{
    class Session
    {
        #region Fields
        /// <summary>
        /// inutile enft
        /// </summary>
        private string token { get; set; }
        public Mail mail { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Create a new user session.
        /// </summary>
        public Session()
        {
            this.token = "";
            this.mail = Mail.GetInstance();
        }

        /// <summary>
        /// Create an new user session via a token.
        /// </summary>
        /// <param name="token"></param>
        public Session(string token)
        {
            this.token = token;
            this.mail = Mail.GetInstance();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Change the mail of the current sesssion.
        /// </summary>
        public void NewMail()
        {
            this.mail.SetNewMail();
        }

        /// <summary>
        /// Check the mails of the current session.
        /// </summary>
        public void CheckMail()
        {
            Console.WriteLine("[-] Checking mail for " + this.mail.address +  "...");
            this.mail.CheckMail();
        }

        #endregion
    }
}
