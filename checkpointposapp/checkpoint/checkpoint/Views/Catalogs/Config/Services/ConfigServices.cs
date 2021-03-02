using checkpoint.Resources;
using checkpoint.Resources.SystemModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace checkpoint.Views.Catalogs.Config.Services
{
    public class ConfigServices : IConfigServices
    {
        public void WriteCashConfig(Caja data)
        {
            XDocument xdoc = XDocument.Load(StringResources.XmlFilePath);
            XmlSerializer deSerializer = new XmlSerializer(typeof(SystemConfig));
            SystemConfig systemConfig = (SystemConfig)deSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(StringResources.CabeceraXml + xdoc.ToString())));
            systemConfig.ticket.caja = data;
            var stringwriter = new StringWriter();
            deSerializer.Serialize(stringwriter, systemConfig);
            xdoc = XDocument.Parse(stringwriter.ToString());
            xdoc.Save(StringResources.XmlFilePath);
        }
    }
}
