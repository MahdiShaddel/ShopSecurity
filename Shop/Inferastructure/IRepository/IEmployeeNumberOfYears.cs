using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Inferastructure.IRepository
{
    public interface IEmployeeNumberOfYears
    {
        public int Get(string Name)
        {
            if (Name=="test2")
            {
                return 21;
            }
            return 10;
        }
    }
}
