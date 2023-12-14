using Etiqa.Application.Views;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Etiqa.Application.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7132/api");
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/Index").Result;

            if (response.IsSuccessStatusCode)
            { 
                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }

            return View(userList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            try
            {
                List<UserViewModel> userList = new List<UserViewModel>();

                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/User/AddUser", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "User registered";
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(string username)
        {
            UserViewModel userDetail = new UserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetDetail?username=" + username).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userDetail = JsonConvert.DeserializeObject<UserViewModel>(data);
            }

            return View(userDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string username)
        {
            UserViewModel userDetail = new UserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetDetail?username=" + username).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userDetail = JsonConvert.DeserializeObject<UserViewModel>(data);
            }

            return View(userDetail);    
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            try
            {
                //UserViewModel userList = new UserViewModel();

                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/User/UpdateUser", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "User updated";
                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }


        }

        [HttpGet]
        public async Task<IActionResult> Delete(string username)
        {
            UserViewModel userDetail = new UserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetDetail?username=" + username).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userDetail = JsonConvert.DeserializeObject<UserViewModel>(data);
            }

            return View(userDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string username)
        {
            UserViewModel userDetail = new UserViewModel();
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/User/DeleteUser?username=" + username).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userDetail = JsonConvert.DeserializeObject<UserViewModel>(data);
                return RedirectToAction(nameof(Index));
            }

            return View(userDetail);
        }
    }
}
