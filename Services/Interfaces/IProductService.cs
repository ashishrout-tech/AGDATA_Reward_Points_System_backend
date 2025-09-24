using Project.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IProductService
    {
        Product Add(string name, string description, string brand);
        void UpdateDetails(Guid productId, string name, string description, string brand);
        void Deactivate(Guid productId);
        void Activate(Guid productId);
    }
}
