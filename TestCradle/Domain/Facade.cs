using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestCradle.Domain
{
    public class Facade
    {
        private string _email;
        private string _pass;
        private string _devId;
        private string _token;
        private string _json;

        // Receiver, Mobile
        private string _options;
        private string _responseBody;
        private int _login;
        private int _appLaunchCount;
        private HttpWebRequest _connection;

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string Pass
        {
            get
            {
                return this._pass;
            }
            set
            {
                this._pass = value;
            }
        }

        public string DevId
        {
            get
            {
                return this._devId;
            }
            set
            {
                this._devId = value;
            }
        }

        public string Token
        {
            get
            {
                return this._token;
            }
            set
            {
                this._token = value;
            }
        }

        public string Json
        {
            get
            {
                return this._json;
            }
            set
            {
                this._json = value;
            }
        }

        public string Options
        {
            get
            {
                return this._options;
            }
            set
            {
                this._options = value;
            }
        }

        public string ResponseBody
        {
            get
            {
                return this._responseBody;
            }
            set
            {
                this._responseBody = value;
            }
        }

        public int Login
        {
            get
            {
                return this._login;
            }
            set
            {
                this._login = value;
                SetLogin();
            }
        }

        public int AppLaunchCount
        {
            get
            {
                return this._appLaunchCount;
            }
            set
            {
                this._appLaunchCount = value;
            }
        }

        public HttpWebRequest Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
            }
        }

        public void SetLogin()
        {
            switch (this.Login)
            {
                case 0:
                    this.Email = "mike@itraycer.com";
                    this.Pass = "mzaros123";
                    break;

                case 1:
                    this.Email = "kevin@sourcesurgical.com";
                    this.Pass = "wave123";
                    break;

                case 2:
                    this.Email = "jwagner@spinewave.com";
                    this.Pass = "jwagner123";
                    break;

                case 3:
                    this.Email = "mobile@medicaltracking.com";
                    this.Pass = "wave123";
                    break;

                case 4:
                    this.Email = "mobile2@itraycer.com";
                    this.Pass = "wave123";
                    break;

            }
        }
    }
}
