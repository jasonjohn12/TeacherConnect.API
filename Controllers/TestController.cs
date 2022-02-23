using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Dto;
using TeacherConnect.Entities;
using TeacherConnect.Interface;

namespace TeacherConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : BaseApiController
    {



        [HttpGet("hello")]
        public ActionResult Hello()
        {
            return Ok("HELLO WORLD");
        }
    }
}