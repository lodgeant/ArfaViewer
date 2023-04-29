using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{


        
    [Serializable]
    public class Tree : SettlerObject
    {
        [XmlAttribute("TreeSize")]
        public string TreeSize { get; set; }
        
        [XmlAttribute("TreeType")]
        public string TreeType { get; set; }



        [XmlElement("LastTransitionTime")]
        public float LastTransitionTime { get; set; }

        [XmlElement("TransitionDelay")]
        public float TransitionDelay { get; set; }

        [XmlElement("FallRate")]
        public float FallRate { get; set; }

        [XmlElement("InOrchard")]
        public bool InOrchard { get; set; }

        [XmlElement("OrchardTransitionDelay")]
        public float OrchardTransitionDelay { get; set; }





        public string SerializeToString(bool omitDeclaration)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                if (omitDeclaration)
                {
                    return textWriter.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
                }
                else
                {
                    return textWriter.ToString();
                }
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            // Use UTF8 encoding but write no BOM to the wire
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
            }
        }

        public Tree DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (Tree)serializer.Deserialize(reader);
            }
        }


        public static string GenerateRandomTreeType()
        {
            string TreeType = "T1";
            try
            {
                Random rnd = new Random();
                int num = rnd.Next(1, 4);  // creates a number between 1 and 3
                if(num == 2) TreeType = "T2";                
                else if (num == 3) TreeType = "T3";               
                return TreeType;
            }
            catch(Exception ex)
            {
                return TreeType;
            }
        }


    }

}
