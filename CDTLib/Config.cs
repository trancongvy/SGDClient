using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace CDTLib
{
    public class Config
    {
        static Hashtable _variables = new Hashtable();
        public static Info info;
        public static Hashtable Variables
        {
            get { return Config._variables; }
            set { Config._variables = value; }
        }

        public static void InitData(DataTable dtData)
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if(dtData.Rows[i] != null)
                    try
                    {
                        _variables.Add(dtData.Rows[i]["_Key"], dtData.Rows[i]["_Value"]);
                    }
                    catch (Exception ex)
                    {
                    }

                   
            }
        }

        public static void NewKeyValue(object key, object value)
        {
            if (_variables.Contains(key))
                _variables.Remove(key);
            _variables.Add(key, value);
        }

        public static object GetValue(string key)
        {
            return (_variables[key]);
        }
        public static Info GetInfo()
        {
            info.config = _variables;
            return info;
        }
    }
    public partial class UserConnection
    {
        public string DatabaseName { get; set; }
        public string ComputerName { get; set; }
        public string LicenceKey { get; set; }
        public string StructDb { get; set; }
        public Nullable<System.DateTime> TimeEx { get; set; }
        public int stt { get; set; }
    }
    public partial class ComputerConnection
    {
        public string DatabaseName { get; set; }
        public string ComputerName { get; set; }
        public string CPUID { get; set; }
        public string StructDB { get; set; }
        public string LicenceKey { get; set; }
        public int stt { get; set; }
    }
    public partial class ServerAPI
    {
        public string DatabaseName { get; set; }
        public string StructDB { get; set; }
        public int stt { get; set; }
        public string ApiUrl { get; set; }
    }
    public class Info
    {

        public string Token { get; set; }
        public string ComputerName { get; set; }
        public string StructCon { get; set; }
        public string DataCon { get; set; }
        public string DataName { get; set; }
        public string StructName { get; set; }
        public DateTime ExDatetime { get; set; }
        public string UserID;
        public string UserGroupID;
        public bool isAdmin;
        public Hashtable config;
        public void JustConnected()
        {
            ExDatetime = DateTime.Now.AddMinutes(5);
        }
    }
}