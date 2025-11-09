using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Domain.Entities.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; }
        public string Email { get; private set; } = null!;
        public string EmployeeId { get; private set; } = null!;
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }

        public UserAccount UserAccount { get; private set; } = null!;

        private User() { }

        public User(string name, string email, string employeeId, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(employeeId))
                throw new ArgumentException("EmployeeId cannot be null or empty.", nameof(employeeId));
            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new ArgumentException("Invalid user role.", nameof(role));

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            EmployeeId = employeeId;
            Role = role;
            IsActive = true;
        }

        public void UpdateUserRole(UserRole role)
        {
            Role = role;
        }

        public void DeactivateUser()
        {
            IsActive = false;
        }

        public void ActivateUser()
        {
            IsActive = true;
        }

        public bool IsUserActive()
        {
            return IsActive;
        }
    }
}
