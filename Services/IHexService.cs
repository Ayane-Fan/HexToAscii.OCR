using HexToAscii.OCR.Models;

namespace HexToAscii.OCR.Services
{
    public interface IHexService
    {
        /// <summary>
        /// Converts a "dirty" hexadecimal string to ascii
        /// </summary>
        ConvertToAsciiResponse ConvertToAscii(string hexText);
    }
}
