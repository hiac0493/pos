using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HttpClientCommunicationClassLibrary;
using HttpClientCommunicationClassLibraryTest.Data;
using HttpClientCommunicationClassLibraryTest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpClientCommunicationClassLibraryTest
{
    [TestClass]
    public class HttpComunicationTest
    {
        private readonly HttpClientTools http;

        public HttpComunicationTest()
        {
            http = new HttpClientTools(true)
            {
                WebApiUrl = "http://192.168.22.32:5230/",
                UserName = "admin@flora-farms.com",
                PassWord = "tim3cl0ckfl0ra*"
            };
        }

        //***********************************************************************
        //* HttpGetSingle
        //***********************************************************************

        [TestMethod]
        public void HttpGetSingle_mock_TestMethod()
        {
            var expected = new Employee();
            var actual = EmployeeData.GetEmployeeData();
            var url = "GetEmployeeById?employeeId=203";
            var code = http.HttpGetSingle(url, ref expected, "Error, no se puede encontrar al Empleado!");
            Assert.AreEqual(expected.EmployeeId, actual.EmployeeId);
        }

        //***********************************************************************
        //* HttpGetList
        //***********************************************************************

        [TestMethod]
        public void HttpGetList_mock_TestMethod()
        {
            IList<Employee> expected = new List<Employee>();
            var actual = EmployeeData.GetEmployeeData();
            var url = "GetAllEmployees";
            var code = http.HttpGetList(url, ref expected, "Error, no se puede encontrar al Empleado!");
            var e = expected.Where(x => x.EmployeeId == actual.EmployeeId).SingleOrDefault();
            Assert.IsNotNull(e);
        }

        //***********************************************************************
        //* HttpGetSingleAsync
        //***********************************************************************

        [TestMethod]
        public void HttpGetSingleAsync_mock_TestMethod()
        {
            var expected = new Employee();
            var actual = EmployeeData.GetEmployeeData();
            var url = "GetEmployeeById?employeeId=203";
            expected = http.HttpGetSingleAsync<Employee>(url, "Error, no se puede encontrar al Empleado!").Result;
            Assert.AreEqual(expected.EmployeeId, actual.EmployeeId);
        }

        //***********************************************************************
        //* HttpGetListAsync
        //***********************************************************************

        [TestMethod]
        public void HttpGetListAsync_mock_TestMethod()
        {
            IList<Employee> expected = new List<Employee>();
            var actual = EmployeeData.GetEmployeeData();
            var url = "GetAllEmployees";
            expected = http.HttpGetListAsync<Employee>(url, "Error, no se puede encontrar al Empleado!").Result;
            var e = expected.Where(x => x.EmployeeId == actual.EmployeeId).SingleOrDefault();
            Assert.IsNotNull(e);
        }

        //***********************************************************************
        //* HttpGetDocument
        //***********************************************************************

        [TestMethod]
        public void HttpGetDocument_TestMethod()
        {
            var actual = EmployeeData.GetEmployeeData();
            var urlDownload = "DownloadDocument?fileName=";
            var fileName = "203ANAC.pdf";
            var responseDownload = http.HttpGetDocument(urlDownload, fileName, "Error, no se pudo descargar el archivo!").Result;
            Assert.IsNotNull(responseDownload);
        }

        //***********************************************************************
        //* HttpPostString
        //***********************************************************************

        [TestMethod]
        public void HttpPostString_TestMethod()
        {
            var url = "DeleteWorkingGroupTypeById?workingGroupTypeId=";
            var hash = new Dictionary<string, string>();
            hash.Add("workingGroupTypeId", "1");
            var response = http.HttpPostString(url, hash, "Hubo un error al intentar dar de baja el grupo de trabajo");
            Assert.AreEqual(response, HttpStatusCode.BadRequest);
        }

        //***********************************************************************
        //* HttpPostObject
        //***********************************************************************

        [TestMethod]
        public void HttpPostObject_TestMethod()
        {
            var url = "AddEmployeeApplication";
            var employee = new Employee
            {
                Name = "Test name",
                FirstLastName = "Test Last Name",
                SecondLastName = "Test Second Last Name",
                CivilStatusId = 1,
                CompanyId = 1,
                CurrencyTypeId = 1,
                CustomerJobId = 1,
                PayrollItemId = 1,
                PayrollTypeId = 1,
                GenderTypeId = 1,
                SalaryNormal = 0,
                ShiftTypeId = 1,
                IsActive = 0,
                StatusTypeId = 3,
                SalaryPaymentTypeId = 1
            };

            var response = http.HttpPostObject(url, employee, "Error al insertar al empleado");
            Assert.AreEqual(response, HttpStatusCode.OK);
        }

        //***********************************************************************
        //* HttpPostObjectAsync
        //***********************************************************************

        [TestMethod]
        public async Task HttpPostObjectAsync_TestMethodAsync()
        {
            var url = "UpdateEmployeePayrollData";
            var model = new PayrollDataUpdModelView
            {
                EmployeeId = 2,
                PayrollTypeId = 10,
                EmployeeFormulaId = 4,
                SalaryNormal = 3100,
                CurrencyTypeId = 1,
                HolidayPayable = 0,
                HolidayPayableFactor = 1,
                StatusTypeId = 2,
                WorkingGroupTypeId = 1
            };

            var response = await http.HttpPostObjectAsync(url, model, "Error al insertar al empleado");
            Assert.AreEqual(response, HttpStatusCode.OK);
        }

        //***********************************************************************
        //* HttpGetAsync
        //***********************************************************************

        [TestMethod]
        public async Task TestHttpGetAsync()
        {
            var response = await http.HttpGetAsync("GetAllPayrollTypes");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //***********************************************************************
        //* HttpPostAsync
        //***********************************************************************

        [TestMethod]
        public async Task TestHttpPostAsync()
        {
            var model = new PayrollTypeModelView
            {
                CompanyId = 1,
                PeriodPaymentId = 2,
                Description = "TestCreate",
                Notes = "empty"
            };

            var response = await http.HttpPostAsync("CreatePayrollType", model);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //***********************************************************************
        //* HttpPutAsync
        //***********************************************************************

        [TestMethod]
        public async Task TestHttpPutAsync()
        {
            var model1 = new PayrollTypeModelView
            {
                CompanyId = 1,
                PeriodPaymentId = 2,
                Description = "TestCreate",
                Notes = "empty"
            };

            var response1 = await http.HttpPostAsync("CreatePayrollType", model1);
            var model2 = response1.Data<PayrollTypeModelView>();
            model2.Description = "TestUpdate";
            var response2 = await http.HttpPostAsync("UpdatePayrollType", model2);
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
        }

        //// ***********************************************************************
        ////      HttpPostObjectAsync
        //// ***********************************************************************
        //[TestMethod]
        //public void HttpPostCreateObjectAsync_TestMethod()
        //{
        //    string url = "AddEmployeeApplication";
        //    var model = new Employee
        //    {
        //        Name = "Test name CreateAsync",
        //        FirstLastName = "Test Last Name CreateAsync",
        //        SecondLastName = "Test Second Last Name CreateAsync",
        //        CivilStatusId = 1,
        //        CompanyId = 1,
        //        CurrencyTypeId = 1,
        //        CustomerJobId = 1,
        //        PayrollItemId = 1,
        //        PayrollTypeId = 1,
        //        GenderTypeId = 1,
        //        SalaryNormal = 0,
        //        ShiftTypeId = 1,
        //        IsActive = 0,
        //        StatusTypeId = 3,
        //        SalaryPaymentTypeId = 1
        //    };
        //    Employee newEmployee = null;
        //    var response = http.HttpPostCreateObjectAsync(url, model, newEmployee, "Error al insertar al empleado").Result;
        //    var e = response as Employee;
        //    Assert.IsNotNull(e);
        //}
    }
}
