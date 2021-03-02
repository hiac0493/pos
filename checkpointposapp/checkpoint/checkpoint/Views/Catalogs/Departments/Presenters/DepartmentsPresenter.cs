using checkpoint.Views.Catalogs.Departments.Services;
using checkpoint.Views.Catalogs.Departments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Departments.Presenters
{
    public class DepartmentsPresenter : IDepartmentsServices
    {
        public readonly IDepartmentsServices _departmentsPresenter;
        public DepartmentsPresenter (IDepartmentsServices departmentsServices)
        {
            _departmentsPresenter = departmentsServices;
        }
        public IEnumerable<Departamento> GetAllDepartamentos()
        {
            return _departmentsPresenter.GetAllDepartamentos();
        }        
        
        public List<Departamento> getDepartmenByName(string name)
        {
            return _departmentsPresenter.getDepartmenByName(name);
        }

        public Task<Departamento> saveDepartment(Departamento departamento)
        {
            return _departmentsPresenter.saveDepartment(departamento);
        }
    }
}
