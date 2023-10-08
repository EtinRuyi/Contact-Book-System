﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContactBookAPI.Core.Interfaces;
using ContactBookAPI.Model;
using ContactBookAPI.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookAPI.Controllers.CRUD
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly ICrudRepository _crudrepository;
        private readonly UserManager<User> _userManager;

        public CRUDController(ICrudRepository crudRepository, UserManager<User> userManager)
        {
            _crudrepository = crudRepository;
            _userManager = userManager;
        }

        [HttpPost("add-new-user")]
        public async Task<IActionResult> AddNewUser([FromBody] PostNewUserViewModel model)
        {
            var result = await _crudrepository.CreateNewUserAsync(model, ModelState);
            if (!result)
            {
                return BadRequest(ModelState);
            }
            return Ok(new
            {
                Message = "User created successfully"
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] PutViewModel model)
        {
            var userUpdate = await _crudrepository.UpdateUserAsync(id, model);
            if (!userUpdate)
            {
                return BadRequest(new
                {
                    Message = "Update failed"
                });
            }
            return Ok(new
            {
                Message = "User Updated successfully"
            });

        }
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers(int page, int pageSize)
        {
            var paginatedResult = await _crudrepository.GetAllUserAsync(page, pageSize);
            return Ok(paginatedResult);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _crudrepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User not found"
                });
            }
            return Ok($"User '{user.UserName}' was found ");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _crudrepository.GetUserByidAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User not found"
                });
            }
            return Ok($"User '{user.UserName}' was found ");
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userDeleted = await _crudrepository.DeleteUserAsync(id);
            if (userDeleted == null)
            {
                return BadRequest(new
                {
                    Message = "User not found or failed to delete user"
                });
            }
            return Ok(new
            {
                Message = "User deleted successfully"
            });
        }

        [HttpPatch("image/{id}")]
        public async Task<IActionResult> UpUserLoadImage(string id, IFormFile image)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Messsage = "User not found" });
            }
            if (image == null)
            {
                return BadRequest(new { Messsage = "image file is required" });
            }
            if (image.Length <= 0)
            {
                return BadRequest(new { Messsage = "image file is empty" });
            }
            var cloudinary = new Cloudinary(new Account(
              "dyilwiqml",
              "848933569826895",
              "0fCIt8pSR15e08AA60RNKrg26pY"
            ));
            var upLoad = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream())
            };
            var upLoadResult = await cloudinary.UploadAsync(upLoad);

            user.ImageURL = upLoadResult.SecureUri.AbsoluteUri;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return BadRequest(new { Messsage = "image update failed" });
            }
            return Ok(new { Messsage = "user image updated successfully" });
        }
    }
}
