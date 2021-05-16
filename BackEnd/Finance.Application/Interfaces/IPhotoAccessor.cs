﻿using System.Threading.Tasks;
using Finance.Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}