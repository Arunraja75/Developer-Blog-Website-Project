using ArunBlog.Web.Data;
using ArunBlog.Web.Models.Domain;
using ArunBlog.Web.Models.Domain.ViewModel;
using ArunBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ArunBlog.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);

            if (ModelState.IsValid == false)
            {
                return View();
            }
            //Mapping AddTagRequest to Tag domain model 
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //show success notification
            }
            else
            {
                //show error notification 
            }
            //error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            //error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        private void ValidateAddTagRequest(AddTagRequest  request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }

    }

}


