using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeaneryASP.Models.Storage
{
    public interface ILoginStorage
    {
        int WhoLogins(String login, String password);
    }
}
