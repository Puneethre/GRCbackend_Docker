using GRCServices.Dto_s;
using GRCServices.Models;

namespace GRCServices.Interfaces
{
    public interface ICustomerMaster
    {
        Task<ServiceResponse<List<GetCustomerMasterDto>>> GetAllCustomers();
        //Task<ServiceResponse<List<GetCustomerMasterDto>>> GetAllCustomersforsysadmin(int id);
        Task<ServiceResponse<List<GetCustomerMasterDto>>> AddCustomer(AddCustomerMasterDto newCustomer);
        Task<ServiceResponse<List<GetCustomerMasterDto>>> UpdateCustomer(UpdateCustomerMasterDto updatedCustomer, int Customerid);
        Task<ServiceResponse<List<GetCustomerMasterDto>>> DeleteCustomer(int id);
    }
}
