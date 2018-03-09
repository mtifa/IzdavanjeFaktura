using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IssuingInvoices.MEFExtensions
{
    [Export]
    public class MefContentLoader
    {
        public static void Compose(object environment)
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(environment);
        }
    }
}