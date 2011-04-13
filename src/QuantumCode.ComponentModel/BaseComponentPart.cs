using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hosting = System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using QuantumCode.WinForm;

namespace QuantumCode.ComponentModel
{
    public abstract class BaseComponentPart
    {
        protected Hosting.DirectoryCatalog _Catalog = null;
        protected Hosting.CompositionContainer _Container = null;

        public BaseComponentPart()
        {
            var _Catalog = new AggregateCatalog(
                new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()),
                new Hosting.DirectoryCatalog("."));

            foreach (string s in WinFormContext.Current.ExtensionDirectoies)
            {
                _Catalog.Catalogs.Add(new DirectoryCatalog(s));
            }

            var _Container = new Hosting.CompositionContainer(_Catalog);
            _Container.ComposeParts(this);
        }
    }
}
