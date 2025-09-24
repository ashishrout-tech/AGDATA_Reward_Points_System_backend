using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Product
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Brand { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public Product(string name, string description, string brand)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Brand = brand;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void UpdateDetails(string name, string description, string brand)
        {
            Name = name;
            Description = description;
            Brand = brand;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
