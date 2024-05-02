using GRCServices.Dto_s;
using GRCServices.Models;

namespace GRCServices.Interfaces
{
    public interface IActivityMaster
    {
        Task<ServiceResponse<List<GetActivityMasterDto>>> GetAllActivites(int CustomerId);
        //Task<ServiceResponse<List<GetRoleMasterDto>>> GetAllCustomersforsysadmin(int id);
        Task<ServiceResponse<List<GetActivityMasterDto>>> AddActivity(AddActivityMasterDto newActivity);
        Task<ServiceResponse<List<GetActivityMasterDto>>> UpdateActivity(UpdateActivityMasterDto updatedActivity, int ActivityId);
        Task<ServiceResponse<List<GetActivityMasterDto>>> DeleteActivity(int id, int CustomerId);
    }
}
