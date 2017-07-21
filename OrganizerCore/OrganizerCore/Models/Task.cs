using Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Priority { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        public ICollection<Subtask> Subtasks { get; set; }
    }
}