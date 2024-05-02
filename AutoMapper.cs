using AutoMapper;
using GRCServices.Dto_s;
//using GRCServices.Models;
using ConsoleApp1.Models;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using GRCServices.Models;

namespace GRCServices
{
    //Comment: Rename the file to AutoMapperProfile.cs 
    //              or AutoMapper
    //copy the generated code in the constructor to  Projects AutoMapperProfile.cs  

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConsoleApp1.Models.ClientRoleMaster, GetRoleMasterDto>();
            CreateMap<AddRoleMasterDto, ConsoleApp1.Models.ClientRoleMaster>();
            CreateMap<UpdateRoleMasterDto, ConsoleApp1.Models.ClientRoleMaster>();

            //CreateMap<ProjectSetupTbl, GetProjectDto>()
            //    .ForMember(dest => dest.StartDate, source => source.MapFrom(source => source.FormattedStartDate))
            //    .ForMember(dest => dest.EndDate, source => source.MapFrom(source => source.FormattedEndDate));

            CreateMap<ConsoleApp1.Models.ActivityMaster, GetActivityMasterDto>();
            CreateMap<AddActivityMasterDto, ConsoleApp1.Models.ActivityMaster>();
            CreateMap<UpdateActivityMasterDto, ConsoleApp1.Models.ActivityMaster>();

            CreateMap<ConsoleApp1.Models.ClientUserInfo, GetUserMasterDto>();
            CreateMap<AddUserMasterDto, ConsoleApp1.Models.ClientUserInfo>();
            CreateMap<UpdateUserMasterDto, ConsoleApp1.Models.ClientUserInfo>();

            CreateMap<ConsoleApp1.Models.AssignmentMaster, GetAssignmentMasterDto>();
            CreateMap<AddAssignmentMasterDto, ConsoleApp1.Models.AssignmentMaster>();
            CreateMap<UpdateAssignmentMasterDto, ConsoleApp1.Models.AssignmentMaster>();

            CreateMap<SysCustomerInfo, GetCustomerMasterDto>();
            CreateMap<AddCustomerMasterDto, SysCustomerInfo>();
            CreateMap<UpdateCustomerMasterDto, SysCustomerInfo>();

            CreateMap<SysLicenseMaster, GetLicenseDto>();
            CreateMap<AddLicenseDto, SysLicenseMaster>();
            CreateMap<UpdateLicenseDto, SysLicenseMaster>();
        }
    }
}
