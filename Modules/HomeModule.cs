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
      Get["/clients/new"] = _ =>  {
        List<Stylists> AllStylists = Stylists.GetAll();
        return View["/add_client.cshtml", AllStylists];
      };
      Get["/stylists/new"] = _ =>  {
        return View["/add_stylist.cshtml"];
      };
      Post["/clients"]= _ =>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        Clients newClients = new Clients(Request.Form["client"], Request.Form["stylist_id"]);
        var selectedStylist = Stylists.Find(newClients.GetStylistId());
        newClients.Save();
        model.Add("stylist", selectedStylist);
        model.Add("client", newClients);
        return View["client_added.cshtml", model];
      };
      Post["/stylists"]= _ =>{
        Stylists newStylists = new Stylists(Request.Form["name"]);
        newStylists.Save();
        return View["stylist_added.cshtml", newStylists];
      };

    }
  }
}
