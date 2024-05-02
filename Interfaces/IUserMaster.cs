using GRCServices.Dto_s;
using GRCServices.Models;

namespace GRCServices.Interfaces
{
    public interface IUserMaster
    {
        Task<ServiceResponse<List<GetUserMasterDto>>> GetAllUsers(int CustomerId);
        //Task<ServiceResponse<List<GetRoleMasterDto>>> GetAllCustomersforsysadmin(int id);
        Task<ServiceResponse<List<GetUserMasterDto>>> AddUser(AddUserMasterDto newUser);
        Task<ServiceResponse<List<GetUserMasterDto>>> UpdateUser(UpdateUserMasterDto updatedUser, int Userid);
        Task<ServiceResponse<List<GetUserMasterDto>>> DeleteUser(int id,int CustomerId);
    }
}
