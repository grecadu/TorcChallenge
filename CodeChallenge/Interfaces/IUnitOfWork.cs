using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Interfaces
{
    public interface IUnitOfWork
    {
        bool CreateOrderWithTotalCost(int customerId, int productId, int quantity);
    }
}
