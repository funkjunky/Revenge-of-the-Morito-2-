using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Morito.Utilities
{
    public static class Vector3Extender
    {
        public static Vector3 loadFromXElement(this Vector3 vector3, XElement objectXML)
        {
            vector3.X = Convert.ToSingle(objectXML.Attribute("X").Value);
            vector3.Y = Convert.ToSingle(objectXML.Attribute("Y").Value);
            vector3.Z = Convert.ToSingle(objectXML.Attribute("Z").Value);

            return vector3;
        }

        public static XElement serializeToXElement(this Vector3 vector3)
        {
            return
                new XElement("Vector3",
                    new XAttribute("X", vector3.X),
                    new XAttribute("Y", vector3.Y),
                    new XAttribute("Z", vector3.Z)
                );
        }
    }
}
