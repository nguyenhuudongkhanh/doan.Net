using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Nhom9_QuanLyNhaHang
{
    class Appsetting
    {
        Configuration congifg;
        public Appsetting()
        {

            congifg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public string getconnectionstring(string nameCS)
        {
            return congifg.ConnectionStrings.ConnectionStrings[nameCS].ConnectionString;

        }
        public void setconenctionstring(string nameCS, string values)
        {

            congifg.ConnectionStrings.ConnectionStrings[nameCS].ConnectionString = values;
            congifg.ConnectionStrings.ConnectionStrings[nameCS].ProviderName = "System.Data.SqlClient";
            congifg.Save(ConfigurationSaveMode.Modified);
        }
        public string getValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
        public void setValue(string name, string value)
        {
            ConfigurationManager.AppSettings[name] = value;
        }
    }
}
