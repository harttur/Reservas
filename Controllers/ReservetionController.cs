﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services.Contract;

namespace Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetAllUsers()
        {
            var reservations = await _reservationService.GetAllReservationAsync();
            return reservations;
        }

        [HttpGet("{Id_reservation}")]
        public async Task<ActionResult<ReservationDto>> GetReservetionById(string Id_reservation)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(Id_reservation);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
        {

            Reservation reservation = await _reservationService.CreateReservationAsync(reservationDto);

            return CreatedAtAction(nameof(GetReservetionById), new { Id_reservation = reservation.Id_reservation }, reservationDto);
        }

        [HttpPut("{Id_reservation}")]
        public async Task<ActionResult> UpdateReservation(string Id_reservation, ReservationDto reservationDto)
        {
            await _reservationService.UpdateReservationAsync(Id_reservation, reservationDto);
            return NoContent();

        }

        [HttpDelete("{Id_reservation}")]
        public async Task<ActionResult> DeleteReservation(string Id_reservation)
        {
            await _reservationService.DeleteReservationAsync(Id_reservation);
            return NoContent();
        }
    }
}
