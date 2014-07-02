using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WebServiceProxyClient.mtsWebServiceAB;
using WebServiceProxyClient.mtsWebServices;
using System.Reflection;
using System.Data;

namespace WebServiceProxyClient
{
    public class WSProxyClient : IDisposable
    {
        ServicePeak wsAB;
        MTSWebServices wsMTS;

        ABWebMethodMap abMethodNames;
        MTSWebServicesMethodMap mtsMethodNames;

        Type typeWebServiceMTS;
        Type typeWebServiceAB;
        Type _methodReturnType;

        Object _methodReturnData;
        Object _whichService;

        public Object WhichService
        {
            get { return _whichService; }
        }

        public Object MethodReturnData
        {
            get { return _methodReturnData; }
        }

        public Type MethodReturnType
        {
            get { return _methodReturnType; }
        }

        private string _passWord;
        private string _userID;
        private string _coID;
        private string _repID;
        private string _isSuper;
        private string _globalRepName;
        private string _coName;

        private bool debug = false;

        public bool IsDebug
        {
            get { return debug; }
            set { debug = value; }
        }

        public string CoName
        {
            get { return _coName; }
            set { _coName = value; }
        }

        public string GlobalRepName
        {
            get { return _globalRepName; }
            set { _globalRepName = value; }
        }

        public string IsSuper
        {
            get { return _isSuper; }
            set { _isSuper = value; }
        }

        public string RepID
        {
          get { return _repID; }
          set { _repID = value; }
        }

        public string UsrID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        public string CoID
        {
            get { return _coID; }
            set { _coID = value; }
        }

        /// <summary>
        /// Internal method to initialize objects and private variables.
        /// </summary>
        private void init()
        {
            wsAB = new ServicePeak();
            wsMTS = new MTSWebServices();

            typeWebServiceAB = wsAB.GetType();
            typeWebServiceMTS = wsMTS.GetType();

            abMethodNames = new ABWebMethodMap();
            mtsMethodNames = new MTSWebServicesMethodMap();
        }

        /// <summary>
        /// WSProxyClient constructor base method
        /// </summary>
        public WSProxyClient()
        {
           init();
        }

        /// <summary>
        /// Constructor overload for debugging. Used to initiate developer created debugging code.
        /// </summary>
        /// <param name="isDebug">set to true to activate debugging features and set default values</param>
        public WSProxyClient(bool isDebug)
        {
            debug = isDebug;
            init();

            if (debug)
            {
                _passWord = "wave123";
                _userID = "mikE@itraycer.com";
            }
        }

        /// <summary>
        /// Constructor overload for debugging. Used to initiate developer created debugging code.
        /// </summary>
        /// <param name="isDebug">set to true to activate debugging features and set default values</param>
        /// <param name="methodName">Required, a string that defines the name of the method to call.</param>
        /// <param name="prms">Required, an array of Strings for the method.</param>
        public WSProxyClient(bool isDebug, string methodName, String[] prms)
        {
            debug = isDebug;
            init();

            if (debug)
            {
                _passWord = "wave123";
                _userID = "mike@itraycer.com";
                CallWebMethod(methodName, prms);
            }
        }

        /// <summary>
        /// This method deciphers which web service the method belongs to and gets the information pertaining to that method.
        /// </summary>
        /// <param name="methodName">Required, a string that defines the name of the method to call.</param>
        /// <param name="parameters">Required, an array of parameter objects for the method.</param>
        /// <returns>MethodInfo, which holds the information for the requested method.</returns>
        private MethodInfo getWebMethod(string methodName, params object[] parameters)
        {
            if (debug)
            {
                Debug.Print("Call to getWebMethod--> " + methodName + "\t" + parameters.ToString() + "\n");
            }
            
            MethodInfo webMethodInfoAB;
            MethodInfo webMethodInfoMTS;

            webMethodInfoAB = typeWebServiceAB.GetMethod(methodName);
            webMethodInfoMTS = typeWebServiceMTS.GetMethod(methodName);

            if (webMethodInfoAB == null)
            {
                if (debug) { Debug.Print("Method Found in MTS\n"); }
                _whichService = wsMTS;
                return webMethodInfoMTS;
            }
            else if (webMethodInfoMTS == null)
            {
                if (debug) { Debug.Print("Method Found in AlphaBravo\n"); }
                _whichService = wsAB;
                return webMethodInfoAB;
            }
            else // means we have two webmethods named the same from 2 services at different endpoints - Handled here.
            {
                if (debug) { Debug.Print("Both Services contain web a Method Named: " + methodName + "!!"); }
                ParameterInfo[] paramInfoAB = webMethodInfoAB.GetParameters();
                ParameterInfo[] paramInfoMTS = webMethodInfoMTS.GetParameters();
                int ask = parameters.Length;
                if (debug) { Debug.Print("The method has " + ask.ToString() + " Params"); }
                if (ask == paramInfoAB.Length && ask != paramInfoMTS.Length)
                {
                    if (debug) { Debug.Print("The same number of Params was found in the AlphaBravo web method"); }
                    _whichService = wsAB;
                    return webMethodInfoAB;
                }
                else if (ask == paramInfoMTS.Length && ask != paramInfoAB.Length)
                {
                    if (debug) { Debug.Print("The same number of Params was found in the MTS web method"); }
                    _whichService = wsMTS;
                    return webMethodInfoMTS;
                }
                else if (ask == paramInfoAB.Length && ask == paramInfoMTS.Length)
                {
                    if (debug) { Debug.Print("You better find that pooch, because its screwed (same number of params in both methods)."); }
                    // TODO: check parameter types.
                }
            }
            // yeah--you shouldn't get here....ever.
            return null;
        }

        /// <summary>
        /// This method calls the web method and sets the return from the web method into the read only property MethodReturnData.
        /// </summary>
        /// <param name="methodName">Required, a string that defines the name of the method to call.</param>
        /// <param name="parameters">Required, a parameter collection for the web method call, accepts null when no parameters are needed.</param>
        public void CallWebMethod(string methodName, params object[] parameters)
        {
            ParameterInfo[] paramInfo;
            MethodInfo webMethodInfo;

            //webMethodInfo contains the entire method handle, this includes the return type, method name, and all parameter info (name & type).
            webMethodInfo = getWebMethod(methodName, parameters);

            //methodReturnType contains the return type of the method being called.
            _methodReturnType = webMethodInfo.ReturnType;

            if (webMethodInfo != null)
            {
                //paramInfo contains an array of parameters.
                paramInfo = webMethodInfo.GetParameters();
                if (paramInfo.Length == 0)
                {
                    _methodReturnData = webMethodInfo.Invoke(_whichService, null);
                }
                else if (parameters == null)
                {
                    if (debug && methodName.Equals(abMethodNames.GetCoIDandRepID))
                    {
                        Debug.Print("\n**   Calling web method under DEBUG - Params is NULL -- Using debug deafaults----------------\n");
                        String[] param = { _passWord, _userID };
                        _methodReturnData = webMethodInfo.Invoke(_whichService, param);
                    }
                    else
                    {
                        //throw error.
                    }
                }
                else
                {
                    _methodReturnData = webMethodInfo.Invoke(_whichService, parameters);
                }
            }
            else
            {
                //throw error, there is no information for the method. 
            }

            if (debug)
            {
                if (_methodReturnType.Equals(typeof(System.String)))
                    Debug.Print("\nCalled remote method! - in WSProxyClient:\n" + methodName + "\n\n" + "RESULTSET----------------\n" + _methodReturnData.ToString() + "\n");
            }

            if (methodName.Equals(abMethodNames.GetCoIDandRepID))
            {
                string tmp = _methodReturnData.ToString();
                string[] temp2 = tmp.Split(',');
                _coID = temp2[0];
                _repID = temp2[1];
                _isSuper = temp2[2];
                _globalRepName = temp2[3];
                _coName = temp2[4];
            }
        }

        /// <summary>
        /// Explicit implimentation of deconstructor. Calls GC.Collect() directly.
        /// </summary>
        void IDisposable.Dispose()
        {
            wsAB = null;
            abMethodNames = null;

            System.GC.Collect();
        }
    }
}