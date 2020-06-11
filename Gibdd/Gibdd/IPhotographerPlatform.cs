using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gibdd
{
    public interface IPhotographerPlatform
    {
        void TakePhoto();
        bool IsCameraAvailable();
        Action<byte[]> PhotoCallback { get; set; }
        void SaveImage(byte[] data);

        Task<Stream> GetImageStreamAsync();
    }
}
