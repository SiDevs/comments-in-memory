﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentApi.Models;

namespace CommentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentController(CommentContext context)
        {
            _context = context;

            if (_context.CommentItems.Count() == 0)
            {
                // Create a new CommentItem if collection is empty,
                // which means you can't delete all comments.
                _context.CommentItems.Add(new CommentItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
    }
}