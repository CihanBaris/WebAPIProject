using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIProject.Entity.Models
{
    [NotMapped]
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
    }
}