using Reservas.Dtos;
using Reservas.Mappers;
using Reservas.Models;
using Reservas.Repository.Contract;
using Reservas.Services.Contract;

namespace Reservas.Services
{
    public class ReservetionService : IReservationService
    
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservetionService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task CreateReservationAsync(ReservationDto reservationDto)
        {
            var reservation = ReservationMapper.ToEntity(reservationDto);
            await _reservationRepository.CreateAsync(reservation);
        }

        public async Task DeleteReservationAsync(string id_reservation)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id_reservation);
            if (reservation == null)
            {
                return;
            }
            await _reservationRepository.DeleteAsync(id_reservation);
        }

        public async Task<List<ReservationDto>> GetAllReservationAsync()
        {
            var reservation = await _reservationRepository.GetAllAsync();
            return ReservationMapper.ToDtoList(reservation);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(string Id_reservation)
        {
            var reservation = await _reservationRepository.GetByIdAsync(Id_reservation);
            if (reservation == null)
            {
                return null;
            }

            return ReservationMapper.ToDto(reservation);

        }

        public async Task UpdateReservationAsync(string id_reservation, ReservationDto reservationDto)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id_reservation);
            if (reservation == null)
            {
                return;
            }

            var updatedReservation = ReservationMapper.ToEntity(reservationDto);
            await _reservationRepository.UpdateAsync(id_reservation, updatedReservation);
        }
    }
}
