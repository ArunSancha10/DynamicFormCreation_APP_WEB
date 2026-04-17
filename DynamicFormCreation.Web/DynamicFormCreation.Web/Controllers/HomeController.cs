using DynamicFormCreation.Web.Data;
using DynamicFormCreation.Web.Models;
using Microsoft.AspNetCore.Mvc;
using static DynamicFormCreation.Web.Models.HomeModel;

public class HomeController : Controller
{
    private readonly ApiService _api;

    public HomeController(ApiService api)
    {
        _api = api;
    }


    public IActionResult Index()
    {
            return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var forms = await _api.GetAsync<List<HomeModel>>("api/Form");

        var model = forms.Select(x => new HomeModel
        {
            Id = x.Id,
            FormName = x.FormName,
            Description = x.Description
        }).ToList();

        return View(model);
    }
}