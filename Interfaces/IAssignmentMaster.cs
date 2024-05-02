using GRCServices.Dto_s;
using GRCServices.Models;

namespace GRCServices.Interfaces
{
    public interface IAssignmentMaster
    {
        Task<ServiceResponse<List<GetAssignmentMasterDto>>> GetAllAssignments(int CustomerId);
        //Task<ServiceResponse<List<GetRoleMasterDto>>> GetAllCustomersforsysadmin(int id);
        Task<ServiceResponse<List<GetAssignmentMasterDto>>> AddAssignment(AddAssignmentMasterDto newAssignment);
        Task<ServiceResponse<List<GetAssignmentMasterDto>>> UpdateAssignment(UpdateAssignmentMasterDto updatedAssignment, int AssignmentId);
        Task<ServiceResponse<List<GetAssignmentMasterDto>>> DeleteAssignment(int id, int CustomerId);
        Task<ServiceResponse<List<GetAssignmentMasterForUserDto>>> GetAllAssignmentsForUser(int CustomerId);
        //Task<ServiceResponse<List<GetAssignmentMasterForUserDto>>> UpdateAssignmentForUser(UpdateAssignmentMasterForUserDto updatedAssignmentforUser, int AssignmentId);

    }
}
