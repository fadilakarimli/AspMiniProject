﻿using AspMiniProject.Services.Interfaces;
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
    private readonly ITeamService _teamService;
     private readonly IBrandService _brandService;

    public AboutController(IAboutService aboutService,ITeamService teamService,IBrandService brandService)
    {
        _aboutService = aboutService;
         _teamService = teamService;
         _brandService = brandService;
    }

    public async Task<IActionResult> Index()
    {
    

        AboutPageVM model = new()
        {
            Abouts = await _aboutService.GetAllAsync(),
            Teams = await _teamService.GetAllAsync(),
            Brands = await _brandService.GetAllAsync()
        };

        return View(model);
    }
   }

 }
