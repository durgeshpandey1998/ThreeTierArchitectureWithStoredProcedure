using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PracticalProject.Models;
using System.Diagnostics;

namespace PracticalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRegisterUserService _registerUserService;
        public HomeController(ILogger<HomeController> logger, IRegisterUserService registerUserService)
        {
            _logger = logger;
            _registerUserService = registerUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> RegisterData()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterData(TblUserRegistration tblUserRegistration)
        {
            var registerUser = await _registerUserService.AddTblUserRegisterationAsync(tblUserRegistration);
            return View();
        }

        [HttpPost]
        public async Task<bool> DeleUser(int Id)
        {
            bool isDeleted = false;
            if (Id > 0)
            {
                await _registerUserService.UserDeleteAsync(Id);
                isDeleted = true;
                return isDeleted;
            }
            else
            {
                return isDeleted;
            }
        }

        public async Task<IActionResult> DisplayUser()
        {
            //var registerUser = await _registerUserService.GetTblUserRegistrationsAsync();
            //return View(registerUser);
            var registerUser = _registerUserService.GetAllUserByProcedure();
            return View(registerUser);
        }

        [HttpPost("GetAllCity")]
        public async Task<JsonResult> GetAllCity(int stateId)
        {
            var cityData = await _registerUserService.GetTblCityAsync(stateId);
            return Json(cityData);
        }
        [HttpGet("GetAllState")]
        public async Task<JsonResult> GetAllState()
        {
            var cityData = await _registerUserService.GetTblStateAsync();
            return Json(cityData);
         }

        [HttpPost("GetUserById")]
        public async Task<JsonResult> GetUserById(int userId)
        {
            var userData = await _registerUserService.GetUserById(userId);
            return Json(userData);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}