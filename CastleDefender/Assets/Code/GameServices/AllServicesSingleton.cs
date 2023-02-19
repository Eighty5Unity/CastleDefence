namespace Code.GameServices
{
    public class AllServicesSingleton
    {
        private static AllServicesSingleton _instance;
        public static AllServicesSingleton Container => _instance ?? (_instance = new AllServicesSingleton());

        public void RegisterService<TService>(TService service) where TService : IService
        {
            Implementation<TService>.Service = service;
        }

        public TService GetService<TService>() where TService : IService
        {
            return Implementation<TService>.Service;
        }
        
        private static class Implementation<TService> where TService : IService
        {
            public static TService Service;
        }
    }
}