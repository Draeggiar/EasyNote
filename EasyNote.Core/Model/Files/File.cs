﻿using System.ComponentModel.DataAnnotations;

namespace EasyNote.Core.Model.Files
{
    public class File
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsLocked { get; set; }
        public string Author { get; set; }
        public string ModifiedBy { get; set; }
    }
}
