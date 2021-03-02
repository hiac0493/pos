using HttpClientCommunicationClassLibraryTest.Model;

namespace HttpClientCommunicationClassLibraryTest.Data
{
    public static class EmployeeData
    {
        public static Employee GetEmployeeData()
        {
            return new Employee
            {
                EmployeeId =  203,
                Name = "Florencio",
                FirstLastName = "Nava",
                SecondLastName = "Patricio"
            };
        }
    }
}
