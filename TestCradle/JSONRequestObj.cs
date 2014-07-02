using System;
using System.Linq;
using TestCradle.Domain;

namespace TestCradle.MTSMobileService
{
    class JSONRequestObj
    {
        public static String CreateJSONObj(Facade facadeObj, params string[] values)
        {
            string jsonEnvelope =   "{"+
                                        "\"CoID\": \"\"," +
                                        "\"RepID\": \"\"," +
                                        "\"Role\": \"\"," +
                                        "\"DevID\": \"" + facadeObj.DevId + "\"," +
                                        "\"AppID\": \"\"," +
                                        "\"SyncRequestTime\": \"" + DateTime.UtcNow + "\"," +
                                        "\"SyncResponseTime\": \"\"," +
                                        "\"LocationVector\": \"\"," +
                                        "\"MtsToken\": \"" + facadeObj.Token + "\"," +
                                        "\"AppLaunchCount\": \"" + facadeObj.AppLaunchCount + "\"," +
                                        "\"SQL\": {}," +
                                        "\"ServiceQueue\": [";

            // get the total amount of objects in the service queue request
            int valTotal = values.Length-1;

            foreach(string val in values){
                bool addComma = (valTotal > 0);
                valTotal--;

                switch (val)
                {
                    case "MTSMobileAuth":
                        jsonEnvelope += "{" +
                                            "\"MTSMobileAuth\": {" +
                                            "\"Email\": \"" + facadeObj.Email + "\"," +
                                            "\"Password\": \"" + facadeObj.Pass + "\"" +
                                            "}" +
                                        "}";
                        break;

                    case "MobileDeviceRegister":
                        jsonEnvelope += "{" +
                                            "\"MobileDeviceRegister\": {" +
                                            "\"DeviceID\": \"" + facadeObj.DevId + "\"," +
                                            "\"DevicePlatform\": \"TestDevice\"," +
                                            "\"DeviceOSVersion\": \"123\"" +
                                            "}" +
                                        "}";
                        break;

                    case "InitCases":
                        jsonEnvelope += "{" +
                                            "\"InitCases\": {" +
                                            "}" +
                                        "}";
                        break;

                    case "InitInventory":
                        jsonEnvelope += "{" +
                                            "\"InitInventory\": {" +
                                             "}" +
                                        "}";
                        break;

                    case "InitDoctors":
                        jsonEnvelope += "{" +
                                            "\"InitDoctors\": {" +
                                            "}" +
                                        "}";
                        break;

                    case "InitAddresses":
                        jsonEnvelope += "{" +
                                            "\"InitAddresses\": {" +
                                            "}" +
                                        "}";
                        break;

                    case "InitStatus":
                        jsonEnvelope += "{" +
                                            "\"InitStatus\": {" +
                                             "}" +
                                        "}";
                        break;
                    case "InitKitAllocation":
                    jsonEnvelope += "{" +
                                        "\"InitKitAllocation\": {" +
                                            "}" +
                                    "}";
                    break;

                    case "InitTrayTypesBySurgeryType":
                    jsonEnvelope += "{" +
                                        "\"InitTrayTypesBySurgeryType\": {" +
                                            "}" +
                                    "}";
                    break;
                    
                    case "GetAddressesByLatLong":
                    jsonEnvelope += "{" +
                                    "\"GetAddressesByLatLong\": {" +
                                            "\"Latitude\": \"43.150657\", " +
                                            "\"Longitude\": \"-93.22345678\", " +
                                     "}" +
                                "}";
                    break;

                    case "CreateCase":
                    jsonEnvelope += "{" +
                                     "\"CreateCase\": {" +
                                                "\"LocalId\": \"1\", " +
                                                "\"CaseId\": \"\", " +
                                                "\"Surgeon\": \"\", " +
                                                "\"SurgeonId\": \"1413\", " +
                                                //"\"Surgeon\": \"Doogie Howser\", " +
                                                //"\"SurgeonId\": \"1905\", " +
                                                //"\"SurgeryDate\": \"" + DateTime.UtcNow.AddDays(5) + "\", " + 
                                                "\"SurgeryDate\": \"5/21/2015 11:00:00 PM\", " +  
                                                "\"DeliverByDate\": \"\", " +
                                                "\"VerdibraeLevel\": \"\", " +
                                                "\"ModifiedDate\": \"\", " +
                                                "\"SurgeryType\": \"PLIF\", " +
                                                "\"MedicalRecordNumber\": \"\", " +
                                                "\"PatientId\": \"\", " +
                                                "\"SurgeryStatus\": \"7\", " +
                                                "\"LocationId\": \"1003\", " +
                                                "\"LoanerFlag\": \"N\", " +
                                                "\"KitTypeNumber\": \"10-1705 1,10-1812 1\", " +
                                                "\"PartNumber\": \"10-1100 1,10-1102 1\", " +
                                                "\"CreatedDate\": \"" + DateTime.Now.ToString() + "\"" +
                                        "}" +
                                    "}";
                        break;

                    case "UpdateTrayItemsUsage":
                        jsonEnvelope += "{" +
                                            "\"UpdateTrayItemsUsage\": {" +
                                                "\"TrayId\": \"1166\", " +
                                                "\"LotNumber\": \"\", " +
                                                "\"PartNumber\": \"10-1352\", " +
                                                "\"QntyUsed\": \"1\", " +
                                                "\"Type\": \"E\"" +
                                            "}" +
                                        "}";
                        break;

                    case "GenerateInvoice" :
                        jsonEnvelope += "{" +
                                            "\"GenerateInvoice\": {" +
                                                "\"CaseId\": \"1\", " +
                                                "\"RepSig\": \"data:image/png;base64,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\", " +
                                                "\"HosSig\": \"AAEAAAD/////AQAAAAAAAAAMAgAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTeXN0ZW0uRHJhd2luZy5CaXRtYXABAAAABERhdGEHAgIAAAAJAwAAAA8DAAAA/hQAAAKJUE5HDQoaCgAAAA1JSERSAAABwgAAAEsIBgAAAOPQh+EAAAABc1JHQgCuzhzpAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwwAADsMBx2+oZAAAFJNJREFUeF7t3QV0XFW7xnH4cKe4uxct7i1StDgFFlrctbi7u0Ox4u7uFHd39+LusO/97ZuTmy9MmkkyE5v3v9asQGcyOXPmnP28vodLQRAEQVDDhBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYRAEQVDThBAGQRAENU0IYTfhjz/+SO+//3564IEH0vnnn58OPPDAtOGGG6Z55pknTT755Gn00UdP//nPf/JjhBFG+NejeG6MMcZIo402Wn5MP/30ackll0wDBgxIF110UXrxxRfTb7/9VvcXgyAIugchhF2IX3/9NT3xxBPp3HPPTZtvvnkad9xx03DDDZcfI488cpp55plTv3790u67754uvfTSLFxfffVV/r1//vmn7l3K5++//04//PBDeuutt9I555yTttxyyzT//POnscceO80444xp4403Th9//HHdq4MgCLomIYSdlJ9++inddtttaeutt64XPJ7bZJNNltZdd93soX333Xd1r25fiOqff/6ZhXauueZKyy67bDr55JPTN998U/eKIGgbDLiDDjoojT/++PXG3kwzzZTeeOONulcEQeUIIewkEBFiQlSEMUcZZZQ0xxxzpO222y59+umnda/qXBBEHuG+++6bFlxwwfTyyy+nv/76q1XeZxCIXDz99NPp6KOPTlNPPXW+plxbQ4cOzc8TwYUXXjgLJKEMgkoRQtiB8OgGDRqUVl555ZybY/12hZuct3rHHXdkkZZH3H///dNnn30WAhi0mu+//z5HOVZdddXUq1evkp7fBx98kNZff/203377pSFDhmThDIJKEELYAXz77bfp9NNPT/PNN1+adNJJO734Eb6bbropbbHFFtlSH2ussVKfPn3SMcccE5Z5UBEIHw9w1113Ta+99loOvTfm888/z0aXXLWf/j8IKkEIYTsjzMmTEvacffbZOyznwXtTAfrll1+mxx57LA0ePDgdcMABqX///vm4VI2OOeaYadRRR82e6i677JJuv/32LHyKaIKgErieGIIiC/LhrjGGVykIn9ced9xx6c4772zydUHQUkII2xnCp/LSTU+Aqt2OYLGwuOy5555Z5Aiw/GPDatNZZpklrbjiilnsbr311vTuu+/mdowgqCbuBZXOww8/fFmFMJ6PHGFQDUII2xk385xzzpkWWmihHN754osv6p4JgtpAQRXvjtElH9icsAmTykHfcsstZb0+CFpKCGEHUISDWMP77LNPeuihh9Ivv/xS92wQdE6KthmFLc8991wWMj2thxxySBo4cGDOIa+++uppvfXWS1tttVXaeeedc0/rTjvtlCMOSy+9dFpllVXSXnvtlZZbbrn6IQ/6Ug19EJmYddZZ63/Kn8tHTzvttGnCCSfMYXrP+X39tKIdEaYPKkEIYQfCOxQSEqIcb7zx8oJxzTXX5Ab2CE0G1YZnpm1HP6jWnW222SYts8wyaZJJJskCJE8sbOn6HGmkkdKUU06ZJxWpctbi4Fp9+OGH00svvZQ9PO/XHHLk2267bc5D9+zZs77lpincI8L5PEHphIsvvjiLr8pRYqrgzPSjCy+8MBfZ/P7773W/GQTlE0LYSWBlX3755WnHHXfMeRCLkAVIWwVLWjXdJZdc0mFN9EHXR8+naUCmAhE2AxommmiiLCY8OmLy+OOPVyU6UXiTRJeoGf9nYMTPP/9c94r/poiaGNhw2WWXDTNvTfyef/75dMIJJ6Qddtghph0FLSaEsJPjJn/99dfTDTfckOeHWrQmnnjiXOQiVGRRYcnfeOON6e233w5PMvgvGkYdhBq17fDg2ntmbCFsjmXvvfdODz74YJOC65hd55tsskm66667mhTLhjAQzz777Cz0hWcYfa1BuYQQdmGElEzdUH3Kol900UXTOOOMk0YcccQ0wQQT5JDSBhtskC1lw7jLWVCC7sWP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\"" +
                                            "}" +
                                    "}";
                        break;

                }
                if (addComma) jsonEnvelope += ",";
            }
            jsonEnvelope +=  "]," +
                                "\"Command\": {}," +
                                 "\"Commit\": \"false\"" +
                             "}";
            return jsonEnvelope;
        }

        public static String ReceiverJSONObject(params string[] values)
        {
            string jsonEnvelope = "{" +
                                        "\"ReturnKitTrays\": \" " + values[0] + "\"" +
                                    "}";

            return jsonEnvelope;
        }
    }
}
