using checkpoint.Resources;
using checkpoint.Views.Catalogs.Departments.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Departments.Services
{
    public class DepartmentsServices : IDepartmentsServices
    {
        public IEnumerable<Departamento> GetAllDepartamentos()
        {
            string webApiUrl = WebApiMethods.GetAllDepartamentos;
            IList<Departamento> departamentosList = new List<Departamento>();
            var ResponseOK = App.HttpTools.HttpGetList<Departamento>(webApiUrl, ref departamentosList, "No se encontró el departamento");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return departamentosList.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Departamento> getDepartmenByName(string name)
        {
            string webApiUrl = WebApiMethods.GetDepartmenByName + name;
            IList<Departamento> departamentosList = new List<Departamento>();
            var ResponseOk = App.HttpTools.HttpGetList<Departamento>(webApiUrl, ref departamentosList, "No se encontró el departamento");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return departamentosList.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<Departamento> saveDepartment(Departamento departamento)
        {
            string webApiUrl = WebApiMethods.SaveDepartamento;
            Departamento usuariosResult = await App.HttpTools.
                HttpPostObjectWithResponseDataAsync<Departamento, Departamento>(webApiUrl, departamento, "Hubo un error en el guardado del departamento").ConfigureAwait(false);
            return usuariosResult;
        }
    }
}