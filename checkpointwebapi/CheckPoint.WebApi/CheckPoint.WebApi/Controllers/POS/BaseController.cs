using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pos.BAL.Interface.DomainUoW;
using Pos.DAL.Repository.DomainUoW;
using Pos.EntityFramework.Edbm;
using Security.BAL.Interface.DomainUoW;
using Security.DAL.Repository.DomainUoW;
using Security.EntityFramework.Edbm;

namespace CheckPoint.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment)
        {
            DbSettings = dbSettings;
            Environment = environment;

            //ErpDbContext = new ErpDbContext(DbSettings.Value.ConnString);
            PosDbContext = new PosDbContext(DbSettings.Value.ConnString);
            SecurityDbContext = new SecurityDbContext(DbSettings.Value.ConnString);
            //ToolsDbContext = new ToolsDbContext(DbSettings.Value.ConnString);

            //PayrollUoW = new PayrollUnitOfWork(ErpDbContext);
            PosUoW = new PosUnitOfWork(PosDbContext);
            SecurityUoW = new SecurityUnitOfWork(SecurityDbContext);
            //ToolsUoW = new ToolsUnitOfWork(ToolsDbContext);
        }

        //public ErpDbContext ErpDbContext { get; }

        public PosDbContext PosDbContext { get; }

        public SecurityDbContext SecurityDbContext { get; }

        //public ToolsDbContext ToolsDbContext { get; }

        //public IPayrollUnitOfWork PayrollUoW { get; }

        public IPosUnitOfWork PosUoW { get; }

        public ISecurityUnitOfWork SecurityUoW { get; }

        //public IToolsUnitOfWork ToolsUoW { get; }


        public IOptions<DatabaseSettings> DbSettings { get; set; }

        public IHostingEnvironment Environment { get; set; }
    }
}
