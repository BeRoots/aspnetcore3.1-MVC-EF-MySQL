using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace weblab2.Models
{
    [Table("todo")]
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public UInt64 Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(TodoCategory))]
        [Column("category")]
        public TodoCategory Category { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(2)]
        [MaxLength(300)]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(TodoPriority))]
        [Column("priority")]
        public TodoPriority Priority { get; set; }

        [Required]
        [Timestamp]
        [Column("dueDate")]
        public string DueDate { get; set; }

        [Required]
        [Timestamp]
        [Column("createDate")]
        public string CreateDate { get; set; } = DateTime.UtcNow.ToString();

        [Required]
        [Column("state")]
        public bool State { get; set; } = false;
    }
}
