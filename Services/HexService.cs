using HexToAscii.OCR.Models;
using System.Text;

namespace HexToAscii.OCR.Services
{
    public class HexService : IHexService
    {
        private const string NewLine = "OA";
        private const string Space = "20";

        public ConvertToAsciiResponse ConvertToAscii(string hexText)
        {
            var response = new ConvertToAsciiResponse();

            // Replace the newlines and spaces from the ocr with their hex values
            hexText = hexText.Replace("\n", NewLine);
            hexText = hexText.Replace(" ", Space);

            byte[] byteArray = new byte[hexText.Length / 2];

            try
            {
                for (int i = 0; i < byteArray.Length; i++)
                {
                    var hexChar = hexText.Substring(i * 2, 2);
                    byteArray[i] = Convert.ToByte(hexChar, 16);
                }
            }
            // A bad character will likely throw the rest of the parse off, so just give them back what we have so far :(
            catch (Exception)
            {
                response.Success = false;
            }

            // Convert the byte array to an ascii string
            response.Value = Encoding.ASCII.GetString(byteArray);

            return response;
        }


    }
}
