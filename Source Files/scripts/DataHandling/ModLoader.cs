using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace DataHandling
{
    class ModLoader
    {
        [XmlAttribute("Type")]
        public string modType;

        [XmlAttribute("Description")]
        public string modDescription;

        [XmlAttribute("Icon")]
        public string modIcon;

        [XmlAttribute("Model")]
        public string modModel;

        [XmlAttribute("Texture")]
        public string modTexture;

        [XmlAttribute("Folder")]
        public string modFolder;
    }


}
