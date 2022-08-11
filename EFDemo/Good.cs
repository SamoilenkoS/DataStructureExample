using System;
using System.ComponentModel.DataAnnotations;

namespace EFDemo
{
    public class Good
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}