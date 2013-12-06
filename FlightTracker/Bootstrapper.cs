using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace FlightTracker
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<Provider.ICache, Provider.Cache>();
        container.RegisterType<Provider.Services.IAirportInfoService, Provider.Services.AirportInfoService>();
        container.RegisterType<Provider.Services.IFlightTrackService, Provider.Services.FlightTrackService>();
        container.RegisterType<Provider.IFlightTrackerProvider, Provider.FlightTrackerProvider>();
        container.RegisterType<Provider.IAirportInfoProvider, Provider.AirportInfoProvider>();
        container.RegisterType<FlightTracker.Controllers.HomeController>();
        container.RegisterType<FlightTracker.Controllers.Api.AirPortInfoController>();    
    }
  }
}