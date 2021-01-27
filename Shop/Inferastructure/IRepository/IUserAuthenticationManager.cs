using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Inferastructure.IRepository
{
    public interface IUserAuthenticationManager
    {
        string Authenticate(string username, string password);
        IDictionary<string,Tuple<string,string>> Tokens { get; }
    }
}
