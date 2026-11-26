
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

using SmartVenue.Business.DTOs.Ticketing;
using SmartVenue.Business.Entities.Ticketing;
using SmartVenue.Business.Enums;
using SmartVenue.Business.Interfaces;

namespace SmartVenue.Business.Services.Ticketing
{
    public class QrTicketService : IQrTicketService
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public QrTicketService(
            IAppDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // 1. Generate a new QR Ticket
        public async Task<QrTicketDto> GenerateQrTicketAsync(
            GenerateQrTicketDto dto)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == dto.BookingId);

            if (booking == null)
                throw new Exception("Booking not found.");

            if (booking.Status != BookingStatus.Confirmed)
                throw new Exception(
                    "QR Ticket can only be generated for confirmed bookings.");

            var existingTicket = await _context.QrTickets
                .AnyAsync(t => t.BookingId == dto.BookingId);

            if (existingTicket)
                throw new Exception(
                    "A QR Ticket already exists for this booking.");

            // Generate a secure random token
            var tokenBytes = RandomNumberGenerator.GetBytes(32);

            var token = Convert.ToBase64String(tokenBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');

            // Store only the token hash
            var tokenHash = GenerateTokenHash(token);

            var now = DateTime.UtcNow;

            var ticket = new QrTicket
            {
                BookingId = booking.Id,
                TicketCodeHash = tokenHash,
                Status = TicketStatus.Active,
                IssuedAt = now,
                CreatedAt = now,
                CreatedBy = _currentUser.UserId
            };

            await _context.QrTickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            // Return the original token only once
            return new QrTicketDto
            {
                Id = ticket.Id,
                BookingId = ticket.BookingId,
                Token = token,
                Status = ticket.Status.ToString(),
                IssuedAt = ticket.IssuedAt,
                ExpiresAt = ticket.ExpiresAt
            };
        }

        // 2. Validate an existing QR Ticket
        public async Task<bool> ValidateQrTicketAsync(
            ValidateQrTicketDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Token))
                return false;

            var tokenHash = GenerateTokenHash(dto.Token);

            var ticket = await _context.QrTickets
                .FirstOrDefaultAsync(t =>
                    t.TicketCodeHash == tokenHash);

            if (ticket == null)
                return false;

            if (ticket.Status != TicketStatus.Active)
                return false;

            if (ticket.ExpiresAt.HasValue &&
                ticket.ExpiresAt.Value <= DateTime.UtcNow)
                return false;

            return true;
        }

        // 3. Get QR Ticket by ID
        public async Task<QrTicketDto?> GetQrTicketByIdAsync(
            int ticketId)
        {
            var ticket = await _context.QrTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                return null;

            return new QrTicketDto
            {
                Id = ticket.Id,
                BookingId = ticket.BookingId,
                Token = string.Empty,
                Status = ticket.Status.ToString(),
                IssuedAt = ticket.IssuedAt,
                ExpiresAt = ticket.ExpiresAt
            };
        }

        // 4. Revoke QR Ticket
        public async Task<bool> RevokeQrTicketAsync(
            int ticketId)
        {
            var ticket = await _context.QrTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                return false;

            if (ticket.Status != TicketStatus.Active)
                return false;

            ticket.Status = TicketStatus.Revoked;
            ticket.UpdatedAt = DateTime.UtcNow;
            ticket.UpdatedBy = _currentUser.UserId;

            await _context.SaveChangesAsync();

            return true;
        }

        // Generate SHA256 hash for the token
        private static string GenerateTokenHash(string token)
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash);
        }
    }
}
