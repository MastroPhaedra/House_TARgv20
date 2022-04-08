using System;
using System.Threading.Tasks;
using House_TARgv20.Core.Domain;
using House_TARgv20.Core.Dtos;

namespace House_TARgv20.Core.ServiceInterface
{
    public interface IHouseService : IApplicationService
    {
        Task<HouseDomain> Add(HouseDto dto);
        Task<HouseDomain> Edit(Guid id);
        Task<HouseDomain> Update(HouseDto dto);
        Task<HouseDomain> Delete(Guid id);

        // For test
        Task<HouseDomain> GetAsync(Guid id);
    }
}
