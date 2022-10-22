using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StudyTimeManager.Domain.Models
{
    public class User
    {
        [Column("UserId")]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Semester>? Semesters { get; set; }
    }
}
