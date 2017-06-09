using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];
      Get["/stylists"] = _ => {
        List<Stylists> AllStylists = Stylists.GetAll();
        return View["stylists.cshtml", AllStylists];
      };
      Get["/clients"]= _ => {
        List<Clients> AllClients = Clients.GetAll();
        return View["clients.cshtml", AllClients];
      };

    }
  }
}
