namespace SourceControlSystem.Api.Infrastructure
{
    using Ninject;

    public static class ObjectFactory
    {
        private static IKernel saveKernel;

        public static void Initialize(IKernel kernel)
        {
            saveKernel = kernel;
        }

        public static T Get<T>()
        {
            return saveKernel.Get<T>();
        }
    }
}