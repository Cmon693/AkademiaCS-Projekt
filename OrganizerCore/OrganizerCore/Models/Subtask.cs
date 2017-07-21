using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Models
{
    public class Subtask
    {
        public int SubtaskID { get; set; }
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Priority { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Task Task { get; set; }
    }
}
