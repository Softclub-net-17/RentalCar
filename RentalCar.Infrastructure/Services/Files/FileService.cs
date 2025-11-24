using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RentalCar.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Infrastructure.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(string folder, IFormFile file)
        {
            var webRootPath = _environment.WebRootPath;
            var path = Path.Combine(webRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fullPath = Path.Combine(path, file.FileName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return file.FileName;
        }
        public Task DeleteFileAsync(string folder, string filename)
        {
            var webRootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(webRootPath, folder,filename);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        } 
    }
}
