using SmartVenue.Business.DTOs.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVenue.Business.Interfaces
{
    public interface IQrTicketService
    {
        Task<QrTicketDto> GenerateQrTicketAsync(
            GenerateQrTicketDto dto);

        Task<bool> ValidateQrTicketAsync(
            ValidateQrTicketDto dto);

        Task<QrTicketDto?> GetQrTicketByIdAsync(
            int ticketId);

        Task<bool> RevokeQrTicketAsync(
            int ticketId);
    }
}
