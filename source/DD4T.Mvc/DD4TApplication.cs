using DD4T.Mvc.Binders;
using System.Web.Mvc;
using System.Web.Routing;
using System;
using DD4T.ContentModel.Factories;
using DD4T.Core.Contracts.ViewModels;

namespace DD4T.Mvc
{
    public abstract class DD4TApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            OnApplicationStarting();
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(CreateContainer());
            var typedBinder = new TypedModelBinder(DependencyResolver.Current.GetService<IPageFactory>(), 
                                    DependencyResolver.Current.GetService<IViewModelFactory>(), 
                                    DependencyResolver.Current.GetService<IComponentPresentationFactory>());

            ModelBinderProviders.BinderProviders.Insert(0, typedBinder);
            OnApplicationStarted();
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            
            routes.MapRoute(
                name: "DD4TDefault",
                url: "{*page}",
                defaults:
                new { controller = "DD4TDefault", action = "Page", page = "/" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DD4TDefault", action = "Page", id = UrlParameter.Optional }
            );
        }

        public abstract IDependencyResolver CreateContainer();

        protected virtual void OnApplicationStarting() { }
        protected virtual void OnApplicationStarted() { }
    }
}