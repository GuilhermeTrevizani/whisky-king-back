using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Services;

public class ShiftService(IUnitOfWork uow) : IShiftService
{
    public async Task End()
    {
        var currentShift = await uow.ShiftRepository.GetCurrent()
            ?? throw new ArgumentException(Globalization.Resources.ShiftIsClosed);
        uow.ShiftRepository.Update(currentShift);
        await uow.Commit();
    }

    public async Task<IEnumerable<ShiftResponse>> GetAll()
    {
        var shifts = await uow.ShiftRepository.GetAll();
        return shifts.Select(x => new ShiftResponse
        {
            Id = x.Id,
            RegisterDate = x.RegisterDate
        });
    }

    public async Task<ShiftResponse?> GetCurrent()
    {
        var currentShift = await uow.ShiftRepository.GetCurrent();
        return currentShift is null ? null : new()
        {
            Id = currentShift!.Id,
            RegisterDate = currentShift.RegisterDate
        };
    }

    public async Task<ShiftResponse> Start()
    {
        var currentShift = await uow.ShiftRepository.GetCurrent();
        if (currentShift is null)
        {
            currentShift = new();

            await uow.ShiftRepository.AddAsync(currentShift);
            await uow.Commit();
        }

        return new()
        {
            Id = currentShift!.Id,
            RegisterDate = currentShift.RegisterDate
        };
    }
}