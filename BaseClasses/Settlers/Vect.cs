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
    public class Vect
    {
        [XmlAttribute("X")]
        public float X;

        [XmlAttribute("Y")]
        public float Y;

        [XmlAttribute("Z")]
        public float Z;



        public Vect()
        {
        }

        public Vect(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return "{" + X + "," + Y + "," + Z + "}";
        }


        


    }

}
