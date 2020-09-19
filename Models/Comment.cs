using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BSExam.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set;}
    }
}