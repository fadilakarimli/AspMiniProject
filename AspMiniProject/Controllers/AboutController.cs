using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.About;
using AspMiniProject.ViewModels.Admin.Team;
using AspMiniProject.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspMiniProject.Controllers
{
   public class AboutController : Controller
{
    private readonly IAboutService _aboutService;
    // private readonly ITeamService _teamService;
    // private readonly IBrandService _brandService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
        // _teamService = teamService;
        // _brandService = brandService;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Bütün about məlumatlarını çək
        List<AboutVM> aboutList = await _aboutService.GetAllAsync();

        // 2. İlk about məlumatını götür
        AboutVM about = aboutList.FirstOrDefault();

        // 3. Yeni model yaradıb, içini doldur
        AboutPageVM model = new()
        {
            About = about,
            // Teams = await _teamService.GetAllAsync(),
            // Brands = await _brandService.GetAllAsync()
        };

        return View(model);
    }
}

}
