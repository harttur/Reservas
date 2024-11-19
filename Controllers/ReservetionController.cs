using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services.Contract;

namespace Reservas.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize]
		[HttpGet]
		public async Task<ActionResult<List<ReservationDto>>> GetAllReservations() // Renomeado para refletir a ação correta
		{
			var reservations = await _reservationService.GetAllReservationAsync();
			return reservations;
		}

		[Authorize]
		[HttpGet("{Id_reservation}")]
		public async Task<ActionResult<ReservationDto>> GetReservationById(string Id_reservation) // Corrigido nome do método e digitação
		{
			var reservation = await _reservationService.GetReservationByIdAsync(Id_reservation);

			if (reservation == null)
			{
				return NotFound();
			}

			return reservation;
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
		{
			Reservation reservation = await _reservationService.CreateReservationAsync(reservationDto);

			return CreatedAtAction(nameof(GetReservationById), new { Id_reservation = reservation.Id_reservation }, reservationDto); // Já retorna com 201
		}

		[Authorize]
		[HttpPut("{Id_reservation}")]
		public async Task<ActionResult> UpdateReservation(string Id_reservation, ReservationDto reservationDto)
		{
			var existingReservation = await _reservationService.GetReservationByIdAsync(Id_reservation);
			if (existingReservation == null)
			{
				return NotFound();
			}

			await _reservationService.UpdateReservationAsync(Id_reservation, reservationDto);
			return NoContent();  // Retorna 204 quando bem-sucedido
		}

		[Authorize]
		[HttpDelete("{Id_reservation}")]
		public async Task<ActionResult> DeleteReservation(string Id_reservation)
		{
			var reservation = await _reservationService.GetReservationByIdAsync(Id_reservation);
			if (reservation == null)
			{
				return NotFound();
			}

			await _reservationService.DeleteReservationAsync(Id_reservation);
			return NoContent();  // Retorna 204 quando bem-sucedido
		}
	}
}
