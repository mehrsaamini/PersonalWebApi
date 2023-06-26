using PersonalWeb.BusinessLayer.DTO.ProjectDTO;
using PersonalWeb.BusinessLayer.DTO.ServiceDTO;
using PersonalWeb.BusinessLayer.DTO.SettingDTO;
using PersonalWeb.BusinessLayer.DTO.SkillDTO;
using PersonalWeb.BusinessLayer.DTO.UserDTO;
using PersonalWeb.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWeb.BusinessLayer.Services.Authorize.AdminRep
{
    public interface IAdminPanelService
    {
        
        #region User
        (int StatusCode, string Message, List<UserDto> UserList, int All) GetUsersList(int currentPage, int take, bool? ascending, string columnName);
        (int StatusCode, string Message, UserDto UserDetails) GetUserDetailsById(int userId);
        (int StatusCode, string Message) SendProjectRequest(SendRequestDto requestDto);
        #endregion

        #region Skills
        (int StatusCode, string Message) CreateNewSkill(CreateSkillDto skillDto);
        (int StatusCode, string Message) UpdateSkillById(UpdateSkillDto skillDto);
        (int StatusCode, string Message) DeleteSkillById(int skillId);
        (int StatusCode, string Message, SkillDto SkillInfo) GetSkillById(int skillId);
        (int StatusCode, string Message, List<SkillDto> SkillList, int All) GetSkillList(int currentPage, int take, bool? ascending, string columnName);
        #endregion

        #region Services
        (int StatusCode, string Message) CreateNewService(CreateService serviceDto);
        (int StatusCode, string Message) UpdateServiceById(UpdateServiceDto serviceDto);
        (int StatusCode, string Message) DeleteServiceById(int serviceId);
        (int StatusCode, string Message, ServiceDto ServiceInfo) GetServiceById(int serviceId);
        (int StatusCode, string Message, List<ServiceDto> ServiceList, int All) GetServiceList(int currentPage, int take, bool? ascending, string columnName);
        #endregion

        #region Projects
        (int StatusCode, string Message) CreateNewProject(CreateProject projectDto);
        (int StatusCode, string Message) UpdateProjectById(UpdateProjectDto projectDto);
        (int StatusCode, string Message) DeleteProjectById(int projectId);
        (int StatusCode, string Message, ProjectDto ProjectInfo) GetProjectById(int projectId);
        (int StatusCode, string Message, List<ProjectDto> ProjectList, int All) GetProjectList(int currentPage, int take, bool? ascending, string columnName, string? type);
        #endregion

        #region Setting
        (int StatusCode, string Message) CreateNewSetting(CreateGeneralSettingDto infoDto);
        (int StatusCode, string Message) UpdateSettingById(UpdateGeneralSettingDto infoDto);
        (int StatusCode, string Message) DeleteSettingById(int settingId);
        (int StatusCode, string Message, GeneralSettingDto GeneralSettingInfo) GetSettingById(int settingId);
        (int StatusCode, string Message, GeneralSettingDto GeneralSettingInfo) GetSettingByType(string type);
        (int StatusCode, string Message, List<GeneralSettingDto> GeneralSettingInfo, int All) GetSettingsList(int currentPage, int take, bool? ascending, string columnName);
        #endregion
    }
}
