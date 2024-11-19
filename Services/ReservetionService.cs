using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Models;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;
using Microsoft.Extensions.Logging;
using System;

namespace Reservas.Services
{
	public class ReservetionService : IReservationService
	{
		private readonly IReservationRepository _reservationRepository;
		private readonly ILogger<ReservetionService> _logger;

		// Construtor com injeção de dependências
		public ReservetionService(IReservationRepository reservationRepository, ILogger<ReservetionService> logger)
		{
			_reservationRepository = reservationRepository;
			_logger = logger;
		}

		// Método privado para buscar a reserva ou lançar exceção caso não encontrada
		private async Task<Reservation> GetReservationOrThrow(string id_reservation)
		{
			var reservation = await _reservationRepository.GetByIdAsync(id_reservation);
			if (reservation == null)
			{
				throw new KeyNotFoundException($"Reservation with ID {id_reservation} not found.");
			}
			return reservation;
		}

		// Método para criar uma reserva
		public async Task<Reservation> CreateReservationAsync(ReservationDto reservationDto)
		{
			if (reservationDto == null)
			{
				throw new ArgumentNullException(nameof(reservationDto), "Reservation DTO cannot be null.");
			}

			var reservation = ReservationMapper.ToEntity(reservationDto);
			await _reservationRepository.CreateAsync(reservation);

			_logger.LogInformation($"Reservation created with ID {reservation.Id_reservation}.");

			return reservation;
		}

		// Método para deletar uma reserva
		public async Task DeleteReservationAsync(string id_reservation)
		{
			var reservation = await GetReservationOrThrow(id_reservation);

			await _reservationRepository.DeleteAsync(id_reservation);
			_logger.LogInformation($"Reservation with ID {id_reservation} has been deleted.");
		}

		// Método para buscar todas as reservas
		public async Task<List<ReservationDto>> GetAllReservationAsync()
		{
			var reservations = await _reservationRepository.GetAllAsync();
			_logger.LogInformation($"Fetched {reservations.Count} reservations.");

			return ReservationMapper.ToDtoList(reservations);
		}

		// Método para buscar uma reserva por ID
		public async Task<ReservationDto> GetReservationByIdAsync(string id_reservation)
		{
			var reservation = await _reservationRepository.GetByIdAsync(id_reservation);
			if (reservation == null)
			{
				return null; // Retorna null se não encontrar
			}

			return ReservationMapper.ToDto(reservation);
		}

		// Método para atualizar uma reserva
		public async Task UpdateReservationAsync(string id_reservation, ReservationDto reservationDto)
		{
			if (reservationDto == null)
			{
				throw new ArgumentNullException(nameof(reservationDto), "Reservation DTO cannot be null.");
			}

			var reservation = await GetReservationOrThrow(id_reservation);

			var updatedReservation = ReservationMapper.ToEntity(reservationDto);
			await _reservationRepository.UpdateAsync(id_reservation, updatedReservation);

			_logger.LogInformation($"Reservation with ID {id_reservation} has been updated.");
		}
	}
}
