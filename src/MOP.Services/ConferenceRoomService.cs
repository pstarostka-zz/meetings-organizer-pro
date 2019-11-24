using AutoMapper;
using MOP.Abstract;
using MOP.Abstract.Dto;
using MOP.DAL.Model;
using MOP.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOP.Services
{
    public class ConferenceRoomService : IConferenceRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConferenceRoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ConferenceRoomDto> AddConferenceRoomAsync(ConferenceRoomDto dto)
        {
            try
            {
                var entity = _mapper.Map<ConferenceRoom>(dto);
                await _unitOfWork.ConferenceRoomRepo.Add(entity).ConfigureAwait(false);
                return dto;
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }

        public async Task<bool> DeleteConferenceRoomAsync(ConferenceRoomDto dto)
        {
            try
            {
                var entity = _mapper.Map<ConferenceRoom>(dto);
                var result = await _unitOfWork.ConferenceRoomRepo.Delete(entity).ConfigureAwait(false);

                return result > 0;
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }

        public async Task<IEnumerable<ConferenceRoomDto>> GetAllConferenceRoomsAsync()
        {
            try
            {
                var entities = await _unitOfWork.ConferenceRoomRepo.GetAll().ConfigureAwait(false);
                return _mapper.Map<IEnumerable<ConferenceRoom>, IEnumerable<ConferenceRoomDto>>(entities);
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }

        public async Task<ConferenceRoomDto> GetConferenceRoomByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.ConferenceRoomRepo.GetById(id).ConfigureAwait(false);
                return _mapper.Map<ConferenceRoomDto>(entity);
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }
    }
}
