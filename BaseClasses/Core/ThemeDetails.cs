using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Linq;



namespace BaseClasses
{

    public class ThemeDetails
    {
        public string Theme { get; set; }
        public List<string> SubThemeList { get; set; }



        public ThemeDetails()
        {
            SubThemeList = new List<string>();
        }

    }





}
