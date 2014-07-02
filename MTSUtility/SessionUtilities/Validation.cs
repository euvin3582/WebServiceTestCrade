using DataLayer.domains;
using MTSUtility.SessionUtilities;
using System;
using System.Configuration;
using System.Data;
using System.Linq;

namespace MTSUtility.UserUtilities
{
    public static class Validation
    {
        public static void CreateUserSession(String email, String pass, iTraycerDeviceInfo itd)
        {
            // encrypt password before storing it
            string hashPass = DataLayer.Controller.CreateHash(pass);
            UserInfo userInfo = new UserInfo();
            userInfo.RepEmail = email;
            userInfo.PassCode = hashPass;

            userInfo = ValidateUser(userInfo);

            if (userInfo == null)
            {
                Console.Write("No valid user was found");
                return;
            }

            if (!ValidateApplicationDeviceInfo(userInfo, itd))
            {
                Console.Write("No valid device was specified");
                return;
            }

            // create session GUID, start session timer
            iTraycerSession its = new iTraycerSession();
            its.RepId = userInfo.Id;
            its.Guid = Guid.NewGuid().ToString();
            its.SessionStartDateTime = DateTime.UtcNow;
            its.UserInfo = Common.ObjSerializer(userInfo);

            if (DataLayer.Controller.InsertiTraycerSessionInfo(its) == 0)
            {
                Console.Write("Fail to insert create row in session table");
                return;
            }

            // save token to the app config at run time to validate agaisn't
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app = config.AppSettings;
            app.Settings.Add("Token", its.Guid);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            ValidateSession();
        }

        public static UserInfo ValidateUser(UserInfo userInfo)
        {
            // use to validate the user name and pass
            userInfo = DataLayer.Controller.GetUserInfoFromEmailPassword(userInfo.RepEmail, userInfo.PassCode);

            if (userInfo == null)
            {
                return null;
            }
            return userInfo;
        }

        public static bool ValidateApplicationDeviceInfo(UserInfo userInfo,  iTraycerDeviceInfo itd)
        {
            DataTable DevAppTable = DataLayer.Controller.GetItraycerApplicationDeviceInfoByRepCoDevId(userInfo.Id, userInfo.CustomerId, itd.DeviceId);

            if (DevAppTable.Rows.Count == 0)
            {
                if (DataLayer.Controller.GetiTraycerDeviceInfoByDeviceId(itd.DeviceId) == null)
                {
                    if (DataLayer.Controller.InsertiTraycerDeviceInfo(itd) == 0)
                        Console.Write("Fail to write to db");
                }

                if (DataLayer.Controller.GetiTraycerApplicationInfoByRepIdCoIdDeviceId(userInfo.Id, userInfo.CustomerId, itd.DeviceId) == null)
                {
                    iTraycerApplication ita = new iTraycerApplication();
                    // create iTraycerApplication object
                    ita.RepId = userInfo.Id;
                    ita.CoId = userInfo.CustomerId;
                    ita.CreatedDate = DateTime.UtcNow;
                    ita.LastSync = DateTime.UtcNow;
                    ita.DeviceId = itd.DeviceId;
                    ita.LaunchCount = 1;

                    //// insert a new row to the application table
                    if (DataLayer.Controller.InsertiTraycerApplicationInfo(ita) == 0)
                        Console.Write("Fail to insert row into Application Table");
                }
                return true;
            }
            else
            {
                if (DataLayer.Controller.UpdateiTraycerApplicationLaunchCount(userInfo.Id, userInfo.CustomerId, itd.DeviceId) > 0)
                    return true;
            }
            return false;
        }

        public static bool ValidateSession()
        {
            // the time the session lives too
            String guid = ConfigurationManager.AppSettings["Token"];
            iTraycerSession its = DataLayer.Controller.GetiTraycerSessionInfo(guid);
            DateTime timeToLive = its.SessionStartDateTime.AddMinutes( Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeout"]));
            
            // 0 = time is equal, 1 = left is greater than right, < 0 time on left is less than time on right
            if (DateTime.Compare(timeToLive, DateTime.UtcNow) >= 0)
            {
                Console.ReadKey();
                return true;
            }
            Console.Write("Session expired");
            return false;
        }
    }
}
