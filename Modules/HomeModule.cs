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
      Get["/clients/{id}"] = parameters => {
       Dictionary<string, object> model = new Dictionary<string, object>();
       var selectedClient = Clients.Find(parameters.id);
       var selectedStylist = Stylists.Find(selectedClient.GetStylistId());
       model.Add("stylist", selectedStylist);
       model.Add("client", selectedClient);
       return View["client.cshtml", model];
      };
      Get["/stylists/{id}"]= parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedStylist = Stylists.Find(parameters.id);
        var ClientStylist = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("clients", ClientStylist);
        return View["stylist.cshtml", model];
      };
      Post["/stylists/cleared"] = _ =>{
        Clients.DeleteAll();
        Stylists.DeleteAll();
        return View["cleared.cshtml"];
      };
      Post["/clients/cleared"] = _ =>{
        Clients.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["/stylists/edit/{id}"] = parameters => {
        Stylists SelectedStylist = Stylists.Find(parameters.id);
        return View["stylist_edit.cshtml", SelectedStylist];
      };
      Patch["/stylists/edit/{id}"] = parameters =>{
        Stylists SelectedStylists = Stylists.Find(parameters.id);
        SelectedStylists.Update(Request.Form["name"]);
        return View["success.cshtml"];
      };
      Get["/clients/edit/{id}"] = parameters => {
       Clients selectedClient = Clients.Find(parameters.id);
       return View["client_edit.cshtml", selectedClient];
      };
      Patch["/clients/edit/{id}"] = parameters =>{
        Clients SelectedClient = Clients.Find(parameters.id);
        SelectedClient.Update(Request.Form["name"]);
        return View["success.cshtml"];
      };
      Get["stylists/delete/{id}"] = parameters => {
       Stylists SelectedStylist = Stylists.Find(parameters.id);
       return View["stylist_delete.cshtml", SelectedStylist];
      };
      Delete["stylists/delete/{id}"] = parameters => {
        Stylists SelectedStylist = Stylists.Find(parameters.id);
        SelectedStylist.Delete();
        return View["success.cshtml"];
      };
      Get["clients/delete/{id}"] = parameters => {
        Clients SelectedClient = Clients.Find(parameters.id);
        return View["client_delete.cshtml", SelectedClient];
      };
      Delete["clients/delete/{id}"] = parameters => {
        Clients SelectedClient = Clients.Find(parameters.id);
        SelectedClient.Delete();
        return View["success.cshtml"];
      };

    }
  }
}
