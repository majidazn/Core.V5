using Castle.DynamicProxy;
using Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Framework.Interceptors
{
    public class TransactionalInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (!invocation.MethodInvocationTarget
               .CustomAttributes
               .Any(a => a.AttributeType == typeof(TransactionalAttribute)))
            {
                invocation.Proceed();
                return;
            }
            using (var transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transaction.Complete();

                }
                catch
                {

                }
                finally
                {
                    transaction.Dispose();
                }
            }

        }
    }
}
