using AutoMapper;
using MOP.Abstract;
using MOP.Abstract.Dto;
using MOP.DAL.Model;
using MOP.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOP.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationDto> AddReservationAsync(ReservationDto dto)
        {
            try
            {
                var entity = _mapper.Map<Reservation>(dto);
                var users = _mapper.Map<IEnumerable<User>>(dto?.InvitedUsers);
                var result = await _unitOfWork.ReservationRepo.InsertReservationWithUsers(entity, users)
                                                              .ConfigureAwait(false);
                if (result < 0)
                    throw new Exception("Failed to add Reservation");

                return dto;
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }

        public async Task<ReservationDto> DeleteReservationAsync(ReservationDto dto)
        {
            try
            {
                var entity = _mapper.Map<Reservation>(dto);
                var result = await _unitOfWork.ReservationRepo.Delete(entity).ConfigureAwait(false);
                if (result < 0)
                    throw new Exception("Failed to remove Reservation");

                return dto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            try
            {
                var reservations = (await _unitOfWork.ReservationRepo.GetAll().ConfigureAwait(false)).ToList();
                var resDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
                var index = 0;
                foreach (var dto in resDtos)
                {
                    var dtoUsers = _mapper.Map<IEnumerable<UserDto>>(reservations[index++].ReservationUsers.Select(x => x.User));
                    dto.InvitedUsers = dtoUsers;
                }
                return resDtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ReservationDto> GetById(Guid id)
        {
            try
            {
                var reservation = await _unitOfWork.ReservationRepo.GetById(id).ConfigureAwait(false);
                var reservationDto = _mapper.Map<ReservationDto>(reservation);

                var usersInReservation = reservation.ReservationUsers.Select(x => x.User);
                reservationDto.InvitedUsers = _mapper.Map<IEnumerable<UserDto>>(usersInReservation);
                return reservationDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ReservationDto>> GetByConferenceRoomId(Guid conferenceRoomId)
        {
            try
            {
                var reservations = (await _unitOfWork.ReservationRepo.GetByConferenceRoomId(conferenceRoomId).ConfigureAwait(false)).ToList();
                var dtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
                var index = 0;
                foreach (var dto in dtos)
                {
                    var dtoUsers = _mapper.Map<IEnumerable<UserDto>>(reservations[index++].ReservationUsers.Select(x => x.User));
                    dto.InvitedUsers = dtoUsers;
                }
                return dtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsForUserAsync(Guid userId)
        {
            try
            {
                var reservations = (await _unitOfWork.ReservationRepo.GetByUserId(userId).ConfigureAwait(false)).ToList();
                var dtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
                var index = 0;
                foreach (var dto in dtos)
                {
                    var dtoUsers = _mapper.Map<IEnumerable<UserDto>>(reservations[index++].ReservationUsers.Select(x => x.User));
                    dto.InvitedUsers = dtoUsers;
                }
                return dtos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ReservationDto> UpdateReservationAsync(ReservationDto dto)
        {
            try
            {
                var entity = _mapper.Map<Reservation>(dto);
                var result = await _unitOfWork.ReservationRepo.Update(entity, _mapper.Map<IEnumerable<User>>(dto.InvitedUsers)).ConfigureAwait(false);
                if (result < 0)
                    throw new Exception("Failed to modify Reservation");

                return dto;
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }
    }
}
