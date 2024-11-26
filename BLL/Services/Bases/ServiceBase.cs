using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Bases
{
    public abstract class ServiceBase
    { 
        public bool IsSuccesful { get; set; }
        public string Message { get; set; } = string.Empty;

        protected readonly Db _db;

        protected ServiceBase(Db db)
        {
            _db = db;
        }

        public ServiceBase Success(string message = "")
        {
            IsSuccesful = true;
            Message = message;
            return this;

        }

        public ServiceBase Error(string message ="")
        {
            IsSuccesful = false;
            Message = message;
            return this;
        }

    }
}
