using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Ninject;
using Akios.Admin.Infrastructure.Abstract;
using Akios.Admin.Infrastructure.Concrete;
using Akios.Domain.Interface;
using Akios.Domain.Repo;

namespace Akios.Admin.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /*
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product(){Name = "Football", Price = 25},
                new Product(){Name = "Surf board", Price = 179},
                new Product(){Name = "Running shoes", Price = 95}
            });

            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
            */
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Bind<IGrupKullaniciRepo>().To<GrupKullaniciRepo>();
            kernel.Bind<IGrupRepo>().To<GrupRepo>();
            kernel.Bind<IGrupYetkiRepo>().To<GrupYetkiRepo>();
            kernel.Bind<IKonfigurasyonRepo>().To<KonfigurasyonRepo>();
            kernel.Bind<IKullaniciRepo>().To<KullaniciRepo>();
            kernel.Bind<IMusteriRepo>().To<MusteriRepo>();
            kernel.Bind<IPersonelRepo>().To<PersonelRepo>();
            kernel.Bind<IRefDataDetayRepo>().To<RefDataDetayRepo>();
            kernel.Bind<IRefDataRepo>().To<RefDataRepo>();
            kernel.Bind<IRefDetaySiparisSeriRepo>().To<RefDetaySiparisSeriRepo>();
            kernel.Bind<ISayacRepo>().To<SayacRepo>();
            kernel.Bind<ISiparisSeriRepo>().To<SiparisSeriRepo>();
            kernel.Bind<ITeslimatKotaRepo>().To<TeslimatKotaRepo>();
            kernel.Bind<IYetkiRepo>().To<YetkiRepo>();
        }
    }
}