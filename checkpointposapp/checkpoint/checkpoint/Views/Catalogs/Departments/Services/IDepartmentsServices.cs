using checkpoint.Views.Catalogs.Departments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Departments.Services
{
    public interface IDepartmentsServices
    {
        IEnumerable<Departamento> GetAllDepartamentos();
        List<Departamento> getDepartmenByName(string name);
        Task<Departamento> saveDepartment(Departamento departamento);
    }
}
