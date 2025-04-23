using AspMiniProject.ViewModels.Admin.About;
using AspMiniProject.ViewModels.Admin.Brand;
using AspMiniProject.ViewModels.Admin.Team;
using System;

namespace AspMiniProject.ViewModels.UI
{
    public class AboutPageVM
    {
        public IEnumerable<AboutVM> Abouts { get; set; }
        public IEnumerable<TeamVM> Teams { get; set; }
        public IEnumerable<BrandVM> Brands { get; set; }
    }
}
