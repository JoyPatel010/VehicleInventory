using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.Services
{
    public class JPVehicleService
    {
        private readonly JPIVehicleRepository _repository;

        public JPVehicleService(JPIVehicleRepository repository)
        {
            _repository = repository;
        }

        
        public async Task<Guid> CreateVehicleAsync(JPCreateVehicleDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var vehicle = new JPVehicle(
                dto.VehicleCode,
                dto.LocationId,
                dto.VehicleType
            );

            await _repository.AddAsync(vehicle);

            return vehicle.Id;
        }

        public async Task<JPVehicleDto> GetVehicleByIdAsync(Guid id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

            return MapToDto(vehicle);
        }

        
        public async Task<List<JPVehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _repository.GetAllAsync();

            return vehicles
                .Select(v => MapToDto(v))
                .ToList();
        }

      
        public async Task UpdateVehicleStatusAsync(Guid id, JPUpdateVehicleStatusDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new Exception("Vehicle not found.");

            // IMPORTANT:
            // We call domain methods here — we do NOT change Status directly.
            switch (dto.Status)
            {
                case JPVehicleStatus.Available:
                    vehicle.MarkAvailable();
                    break;

                case JPVehicleStatus.Rented:
                    vehicle.MarkRented();
                    break;

                case JPVehicleStatus.Reserved:
                    vehicle.MarkReserved();
                    break;

                case JPVehicleStatus.UnderService:
                    vehicle.MarkServiced();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dto.Status));
            }

            await _repository.UpdateAsync(vehicle);
        }

       
        public async Task DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new Exception("Vehicle not found.");

            await _repository.DeleteAsync(vehicle);
        }

        
        private JPVehicleDto MapToDto(JPVehicle vehicle)
        {
            return new JPVehicleDto
            {
                Id = vehicle.Id,
                VehicleCode = vehicle.VehicleCode,
                LocationId = vehicle.LocationId,
                VehicleType = vehicle.VehicleType,
                Status = vehicle.Status
            };
        }
    }
}
