﻿using bloggerServer.Data;
using bloggerServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace bloggerServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PostController(AppDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        [ActionName("GetAllPosts")]
        public async Task<IEnumerable<BlogPost>> GetAllPosts()
        {
            return await _context.PostTable.ToListAsync();
        }

        [HttpPost]
        [ActionName("AddPosts")]
        public async Task<ActionResult<BlogPost>> AddPosts(BlogPost posts)
        {
            try
            {
                var newPosts = new BlogPost();
                newPosts.PostUserId = posts.PostUserId;
                newPosts.Description = posts.Description;
                newPosts.CreatedOn = DateTime.UtcNow;
                newPosts.PostTitle = posts.PostTitle;
                newPosts.isAnon = posts.isAnon;
                newPosts.PostBody = posts.PostBody;
                newPosts.PostImagePathName = posts.PostImagePathName;

                _context.PostTable.Add(newPosts);
                await _context.SaveChangesAsync();

                return newPosts;

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        

        [HttpPost]
        [ActionName("AddImage")]
        public IActionResult AddImage([FromForm] UploadImage upload)
        {
            if (upload != null && upload.PostImage != null && upload.PostImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.PostImage.FileName);
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    upload.PostImage.CopyTo(fileStream);
                }

                var imageUrl = $"/uploads/{fileName}";
                var imagePathName = fileName;

                return Ok(new { imageUrl, imagePathName });
            }
            else
            {
                return BadRequest("No file");
            }
        }


        [HttpPut("{id}")]
        [ActionName("UpdatePosts")]
        public async Task<ActionResult<BlogPost>> UpdatePosts(int id, BlogPost posts)
        {
            try
            {
                if (id != posts.Id)
                {
                    return BadRequest();
                }


                _context.Entry(posts).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpDelete("{id}")]
        [ActionName("DeletePosts")]
        public async Task<ActionResult<BlogPost>> DeletePosts(int id)
        {
            try
            {
                var findId = await _context.PostTable.FindAsync(id);
                if(findId == null)
                {
                    return NotFound();
                }

                var findPinned = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == findId.PostUserId); //look for the object that has the same userId and deletes
                                                                                                                      //the pinned post on the profile if the post is pinned.
                if(findPinned != null)
                {
                    if(findPinned.PinnedOne == findId.PostTitle)
                    {
                        findPinned.PinnedOne = "";
                        
                    }
                    else if(findPinned.PinnedTwo == findId.PostTitle)
                    {
                        findPinned.PinnedTwo = "";
                    }
                    else
                    {
                        findPinned.PinnedThree = "";
                    }
                    _context.Entry(findPinned).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                
                _context.PostTable.Remove(findId);
                await _context.SaveChangesAsync();

                return Ok("Deleted");
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
