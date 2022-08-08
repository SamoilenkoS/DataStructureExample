﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EFDemo
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }

        public override string ToString()
        {
            return $"{BlogId} {Url} {Rating}";
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public override string ToString()
        {
            return $"{PostId} {Title} {Content} {Blog}";
        }
    }

    public class BloggingContext : DbContext
    {
        public BloggingContext()
        {
        }

        public BloggingContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=EPUADNIW02B7;Initial Catalog=AdvancedNew;Integrated Security=True");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            using (var db = new BloggingContext())
            {
                var post = db.Posts.Include(x => x.Blog).First();

                Console.WriteLine(post);
            }
        }
    }
}