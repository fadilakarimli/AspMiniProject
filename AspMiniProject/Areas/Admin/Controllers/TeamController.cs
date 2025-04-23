using AspMiniProject.Models;
using AspMiniProject.Services;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Brand;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using AspMiniProject.ViewModels.Admin.Team;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<IActionResult> Index()
        {
            var teams = await _teamService.GetAllAsync();
            return View(teams);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string uniqueFileName = null;
            if (vm.Image != null)
            {
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(fileStream);
                }
            }

            var team = new Team
            {

                FullName = vm.FullName,
                Position = vm.Position,
                Image = uniqueFileName

            };

            await _teamService.CreateAsync(team);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team is null) return NotFound();

            var vm = new TeamEditVM
            {
                Image = team.Image,
                FullName = team.FullName,
                Position = team.Position,
               
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var updatedTeam = new Team
            {
               Image = vm.Image,
               FullName = vm.FullName,
               Position = vm.Position,
            };

            await _teamService.UpdateAsync(id, updatedTeam);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();

            var vm = new TeamDetailVM
            {
                Image = team.Image,
                FullName = team.FullName,
                Position = team.Position,
            };

            return View(vm);
        }
    }
}
