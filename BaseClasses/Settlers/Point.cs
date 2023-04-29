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
    public class Point
    {
        [XmlAttribute("X")]
        public int X;

        [XmlAttribute("Y")]
        public int Y;

        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "{" + X + "," + Y + "}";
        }

        public static Point ConvertCorePointToRealPoint(Point corePoint, Point topLeft)
        {
            Point realPoint = new Point();
            realPoint.X = corePoint.X + topLeft.X;
            realPoint.Y = topLeft.Y - corePoint.Y;
            return realPoint;
        }

        public static Point ConvertRealPointToCorePoint(Point realPoint, Point topLeft)
        {
            Point corePoint = new Point();
            corePoint.X = realPoint.X - topLeft.X;
            corePoint.Y = topLeft.Y - realPoint.Y;
            return corePoint;
        }



    }

}
