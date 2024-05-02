using GRCServices.Models;
using GRCServices.Dto_s;

namespace GRCServices.Interfaces
{
    public interface ILicenseManagement
    {
        Task<ServiceResponse<List<GetLicenseDto>>> AddLicense(AddLicenseDto newForm);
        Task<ServiceResponse<List<GetLicenseDto>>> GetAllLicense(int? CustomerId);
        Task<ServiceResponse<List<GetLicenseDto>>> UpdateLicense(UpdateLicenseDto updateLicense);

    }
}
