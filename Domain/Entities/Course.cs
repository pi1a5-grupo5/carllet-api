using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Percurso")]
    public class Course
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public Guid Id { get; set; }

        [ForeignKey("id")]
        [Column("id_condutor")]
        public Guid? OwnerId { get; set; }
        public User? Owner { get; set; }

        [Column("distancia_percurso")]
        public int CourseLength { get; set; }

        [Column("data_inicio_percurso")]
        public DateTime CourseStartTime { get; set; }

        [Column("data_fim_percurso")]
        public DateTime CourseEndTime { get; set; }

    }
}
