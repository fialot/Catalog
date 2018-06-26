using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace Katalog
{
    static class Lng
    {
        static ResourceManager resources = new ResourceManager("Katalog.languages.lang", typeof(frmMain).Assembly);

        /// <summary>
        /// Get Language String
        /// </summary>
        /// <param name="ID">ID of string</param>
        /// <returns></returns>
        public static string Get(string ID)
        {
            string res;
            try
            {
                res = (string)resources.GetObject(ID);
                if (res == null) return ID;
            }
            catch
            {
                return ID;
            }
            return res;

        }

        /// <summary>
        /// Get Language String
        /// </summary>
        /// <param name="ID">ID of string</param>
        /// <param name="def">Default value</param>
        /// <returns></returns>
        public static string Get(string ID, string def)
        {
            string res;
            try
            {
                res = (string)resources.GetObject(ID);
                if (res == null) return def;
            }
            catch
            {
                return def;
            }
            return res;
        }
    }
}
