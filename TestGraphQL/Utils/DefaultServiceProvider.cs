using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL
{
    public class DefaultServiceProvider: IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            try
            {
                return Activator.CreateInstance(serviceType);
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to call Activator.CreateInstance. Type: {serviceType.FullName}", exception);
            }
        }
    }
}
