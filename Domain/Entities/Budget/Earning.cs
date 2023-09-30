using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace Domain.Entities.Budget
{
    [Table("Ganho")]
    public class Earning
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_ganho")]
        public Guid Id { get; set; }

        [ForeignKey("id")]
        [Column("id_condutor_ganho")]
        public Guid? OwnerId { get; set; }

        [JsonIgnore]
        public User? Owner { get; set; }

        [Column("dt_hr_insercao_ganho")]
        public DateTime InsertionDateTime { get; set; }

        [Column("valor_ganho")]
        public double EarningValue { get; set; }

    }
}
