using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typings.Models;
using Typings.Services;

namespace Typings.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TypeTestController : Controller
    {
        private readonly TypeTestService _typeTestService;
        
        public TypeTestController(TypeTestService typeTestService)
        {
            _typeTestService = typeTestService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TypeTestModel model)
        {
            await _typeTestService.Add(User.Identity!.Name!, model.Wpm, model.Accuracy);
            
            return Ok();
        }
    }
}