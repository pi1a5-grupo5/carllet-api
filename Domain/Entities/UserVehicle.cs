﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.VehicleNS;
using Domain.Entities.Budget.Expenses;

namespace Domain.Entities

{
    [Table("Condutores_Veiculos")]
    public class UserVehicle
    {
        public UserVehicle(Guid vehicleId, Guid userId)
        {
            VehicleId = vehicleId;
            UserId = userId;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserVehicleId { get; set; }

        [Column("id_veiculo")]
        public Guid VehicleId { get; set; }

        [Column("id_condutor")]
        public Guid UserId { get; set; }
        public Vehicle Vehicle { get; set; }
        public User User { get; set; }

        public List<Expense>? Expenses{ get; set; }
        public List<Course>? Courses{ get; set; }




    }
}