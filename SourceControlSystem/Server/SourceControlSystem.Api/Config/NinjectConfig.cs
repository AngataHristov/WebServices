[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SourceControlSystem.Api.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SourceControlSystem.Api.NinjectConfig), "Stop")]

namespace SourceControlSystem.Api
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;

    using Data;
    using Data.Common.Repositories;
    using Common.Constants;
    using Infrastructure;

    public static class NinjectConfig
    {

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel
                    .Bind<Func<IKernel>>()
                    .ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel
                    .Bind<IHttpModule>()
                    .To<HttpApplicationInitializationHttpModule>();

                ObjectFactory.Initialize(kernel);
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));

            kernel.Bind<ISourceControlSystemDbContext>().To<SourceControlSystemDbContext>()
                .InRequestScope();

            kernel.Bind(b => b.From(AssembliesConstants.DataServicesAssembly)
            .SelectAllClasses()
            .BindDefaultInterface());
        }
    }
}
