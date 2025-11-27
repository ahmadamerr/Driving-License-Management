using System;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;


namespace DVLD.Classes
{
    public  static  class clsGlobal
    {
        public static clsUser CurrentUser;

        public static void LogInformation(string Message)
        {
            const string SourceName = "DVLD";

            if (!EventLog.SourceExists(SourceName))
                EventLog.CreateEventSource(SourceName, "Application");

            EventLog.WriteEntry(SourceName, Message, EventLogEntryType.Error);
        }

        private static void DeleteRegistery()
        {
            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(@"SOFTWARE\DVLD", true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue("Username");
                            key.DeleteValue("Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");

                LogInformation(ex.Message);
            }

        }

        public static bool IsDataDeleted(string Data)
        {
            const string keyPath = @"Software\\DVLD";

            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                using (RegistryKey key = baseKey.OpenSubKey(keyPath))
                {
                    if (key == null)
                    {
                        return true;
                    }

                    object passwordValue = key.GetValue(Data);
                    if (passwordValue == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            const string KeyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {
                if (Username == "" || Password == "")
                {
                    DeleteRegistery();
                    return true;
                }

                Registry.SetValue(KeyName, "Username", Username);
                Registry.SetValue(KeyName, "Password", Password);

                return true;
            }
            catch (Exception ex)
            {
               MessageBox.Show ($"An error occurred: {ex.Message}");
                LogInformation(ex.Message);
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            const string KeyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {
                if (IsDataDeleted("Username") || IsDataDeleted("Password"))
                    return false;

                Username = Registry.GetValue(KeyName, "Username", null).ToString();
                Password = Registry.GetValue(KeyName, "Password", null).ToString();

                if (Username == "" || Password == "") return false;
                else return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                LogInformation(ex.Message);
                return false;   
            }
        }
    }
}
