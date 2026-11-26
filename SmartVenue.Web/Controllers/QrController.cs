
using Microsoft.AspNetCore.Mvc;
using SmartVenue.Business.DTOs.Ticketing;
using SmartVenue.Business.Interfaces;
using QRCoder;

namespace SmartVenue.Controllers
{
    public class QrController : Controller
    {
        private readonly IQrTicketService _qrTicketService;

        public QrController(IQrTicketService qrTicketService)
        {
            _qrTicketService = qrTicketService;
        }

        // GET: Qr/Generate
        [HttpGet]
        public IActionResult Generate()
        {
            return View();
        }

        // POST: Qr/Generate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(GenerateQrTicketDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var ticket = await _qrTicketService.GenerateQrTicketAsync(dto);

                // Generate QR Code
                using var qrGenerator = new QRCodeGenerator();

                using var qrCodeData = qrGenerator.CreateQrCode(
                    ticket.Token,
                    QRCodeGenerator.ECCLevel.Q
                );

                var qrCode = new PngByteQRCode(qrCodeData);

                byte[] qrCodeBytes = qrCode.GetGraphic(20);

                ViewBag.QrCodeImage = Convert.ToBase64String(qrCodeBytes);

                return View("Ticket", ticket);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // GET: Qr/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _qrTicketService
                .GetQrTicketByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        // POST: Qr/Revoke/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revoke(int id)
        {
            var result = await _qrTicketService
                .RevokeQrTicketAsync(id);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
