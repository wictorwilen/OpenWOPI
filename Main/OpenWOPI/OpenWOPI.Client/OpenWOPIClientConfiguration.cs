using OpenWOPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenWOPI.Client
{
    public class OpenWOPIClientConfiguration : OpenWOPIConfiguration
    {
        private static volatile object syncObject = new object();
        private static OpenWOPIClientConfiguration current = null;
        public static OpenWOPIClientConfiguration Current
        {
            get
            {
                if (current == null)
                {
                    lock (syncObject)
                    {
                        current = new OpenWOPIClientConfiguration(HttpContext.Current);
                    }
                }
                return current;
            }
        }
        private readonly string _source;
        public OpenWOPIClientConfiguration(HttpContext context)
        {
            _source = HttpContext.Current.Server.MapPath("~/App_Data/openwopi.ini");
            loadSettings();
            // TODO: Add FileSystemWatcher here
        }



        private void loadSettings() {
            this.Clear();
            if (!System.IO.File.Exists(_source))
            {
                return;
            }
            System.IO.File.ReadAllLines(_source).ToList().ForEach(l =>
            {
                if (!l.StartsWith("#"))
                {
                    string[] arr = l.Split(new char[]{':'},2);
                    if (arr.Length == 2)
                    {
                        this.Add(arr[0], arr[1]);
                    }
                }
            });
        }
        public override void Save() {
            List<string> lines = new List<string>();
            foreach (string key in this.Keys)
            {
                lines.Add(string.Format("{0}:{1}", key, this[key]));
            }
            System.IO.File.WriteAllLines(_source, lines);
        }

    }
}
