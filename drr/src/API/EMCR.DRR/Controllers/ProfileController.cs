﻿using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMCR.DRR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileDetails>> ProfileDetails()
        {
            var profile = new ProfileDetails
            {
                FirstName = "John",
                LastName = "Doe",
                Title = "Sr. Manager",
                Department = "IT",
                Phone = "123-456-7890",
                Email = "jd@example.com"
            };

            return Ok(await Task.FromResult(profile));
        }
    }

    public class ProfileDetails
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Title { get; set;}
        public string? Department { get; set; }
        public string? Phone{ get; set; }
        public required string Email { get; set; }
    }
}
