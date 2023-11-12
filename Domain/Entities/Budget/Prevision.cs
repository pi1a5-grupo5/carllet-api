
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.Budget
{
    public class Prevision
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_previsao")]
        public Guid Id { get; set; }

        [ForeignKey("id")]
        [Column("id_condutor_previsao")]
        public Guid? OwnerId { get; set; }

        [JsonIgnore]
        public User? Owner { get; set; }

        [Column("dt_hr_insercao_previsao")]
        public DateTime InsertionDateTime { get; set; }

        [Column("valor_previsao")]
        public double EarningValue { get; set; }
    }
}


