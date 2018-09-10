using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GeekRegistrationSystem.Common
{
    public class TransactionUtils
    {
        public static TransactionScope CreateTransactionScope()
        {
            var transactionOptions = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout,
            };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }
    }
}
