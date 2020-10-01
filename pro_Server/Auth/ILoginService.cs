using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Auth
{
    public interface ILoginService
    {
        Task Login(User user);
        Task Logout();
    }
}
