
using QrCodeGenerator.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QrCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QrqrGenerate(string qr)
        {
            return Json(GenerateOrDisplayQRCode(qr));
        }
        public string GenerateOrDisplayQRCode(string data)
        {
   

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();

            var qrCodeData = qRCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            // Convert the image to byte array
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            string base64ImageRepresentation = Convert.ToBase64String(byteImage);
            string DosyaAdi = Sabitler.RandomString(10);
            Sabitler.SaveImage(base64ImageRepresentation, DosyaAdi);
            // Return the image as inline content
            return DosyaAdi;
        }
      
    }
}