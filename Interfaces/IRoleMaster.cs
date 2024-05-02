using GRCServices.Dto_s;
using GRCServices.Models;

namespace GRCServices.Interfaces
{
    public interface IRoleMaster
    {
        Task<ServiceResponse<List<GetRoleMasterDto>>> GetAllRoles(int CustomerId);
        //Task<ServiceResponse<List<GetRoleMasterDto>>> GetAllCustomersforsysadmin(int id);
        Task<ServiceResponse<List<GetRoleMasterDto>>> AddRole(AddRoleMasterDto newRole);
        Task<ServiceResponse<List<GetRoleMasterDto>>> UpdateRole(UpdateRoleMasterDto updatedRole, int Roleid);
        Task<ServiceResponse<List<GetRoleMasterDto>>> DeleteRole(int id ,int CustomerId);
    }
}
