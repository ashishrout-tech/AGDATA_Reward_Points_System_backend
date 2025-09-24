using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Enums;

namespace Project.Domain.Entities.User
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string EmployeeId { get; }
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }

        public User(string name, string email, string employeeId, UserRole role)
        {
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
