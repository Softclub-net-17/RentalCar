using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string folder, IFormFile file);
        void DeleteFileAsync(string folder, string filename);
    }
}
