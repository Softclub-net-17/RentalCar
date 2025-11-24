using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Application.Common.Constants
{
    public static class UploadFolders
    {
        public static readonly string Cars = Path.Combine("uploads", "cars");
        public static readonly string Makes = Path.Combine("uploads", "makes");
    }
}
