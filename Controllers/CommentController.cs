﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public PractDbContext Context { get; }

        public CommentController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CommentTable> users = Context.CommentTables.ToList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            CommentTable? comment = Context.CommentTables.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null)
            {
                return BadRequest("Not found");
            }
            return Ok(comment);
        }
        [HttpPut]
        public IActionResult Update(int id, string newText)
        {
            CommentTable? comment = Context.CommentTables.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null)
            {
                return BadRequest("Not found");
            }
            comment.CommentText = newText;
            Context.SaveChanges();
            return Ok(comment);
        }
        [HttpPost]
        public IActionResult Add(string text, int userId, int objectId)
        {
            CommentTable comment = new CommentTable() { CommentText = text, ObjectId = objectId, UserId = userId };
            Context.CommentTables.Add(comment);
            Context.SaveChanges();
            return Ok(comment);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Role? comment = Context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (comment == null) return BadRequest("Not found");
            Context.Roles.Remove(comment);
            Context.SaveChanges();
            return Ok(comment);
        }
    }
}
