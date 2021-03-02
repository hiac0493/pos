using checkpoint.Resources;
using checkpoint.Resources.SystemModels;
using checkpoint.Views.MainTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace checkpoint.Views.MainTemplate.Services
{
    public class MainTemplateServices : IMainTemplateServices
    {
        public List<Pantallas> GetAllPrincipalPantallas()
        {
            string webApiUrl = WebApiMethods.GetAllPrincipalPantallas;
            IList<Pantallas> screensResult = new List<Pantallas>();
            var result = App.HttpTools.HttpGetList<Pantallas>(webApiUrl, ref screensResult, "Hubo un error en la lectura de las pantallas de la aplicación");
            if (result == HttpStatusCode.OK)
                return screensResult.ToList();
            else
                return null;
        }

        public SystemConfig cashConfig()
        {                       
            XmlSerializer deSerializer = new XmlSerializer(typeof(SystemConfig));
            SystemConfig systemConfig = (SystemConfig)deSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(StringResources.CabeceraXml + XDocument.Load(StringResources.XmlFilePath).ToString())));   
            return systemConfig;
        }    
    }
}
