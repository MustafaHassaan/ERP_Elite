using Microsoft.AspNetCore.Mvc;
using Reositories.Interfaces;

namespace ERP_Task_Elite.Controllers
{
    public class Basecontroller : Controller
    {
        protected readonly IUnitofwork _IUW;
        public Basecontroller(IUnitofwork IUW)
        {
            _IUW = IUW;
        }
    }
}
