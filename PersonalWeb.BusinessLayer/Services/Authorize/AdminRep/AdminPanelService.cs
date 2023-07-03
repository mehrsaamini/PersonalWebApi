using PersonalWeb.BusinessLayer.DTO.ProjectDTO;
using PersonalWeb.BusinessLayer.DTO.ServiceDTO;
using PersonalWeb.BusinessLayer.DTO.SettingDTO;
using PersonalWeb.BusinessLayer.DTO.SkillDTO;
using PersonalWeb.BusinessLayer.HelperClass;
using PersonalWeb.DataLayer.Context;
using PersonalWeb.DataLayer.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using PersonalWeb.BusinessLayer.DTO.UserDTO;

namespace PersonalWeb.BusinessLayer.Services.Authorize.AdminRep
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly AuthorizeContext _context;
        private readonly IViewRenderService _viewRender;
        public AdminPanelService(AuthorizeContext context, IViewRenderService viewRender)
        {
            _context = context;
            _viewRender = viewRender;
        }

        #region User
        public (int StatusCode, string Message, UserDto UserDetails) GetUserDetailsById(int userId)
        {
            var result = new UserDto();

            try
            {
                var existUser = _context.Users.SingleOrDefault(s => s.UserId == userId);
                if (existUser == null)
                    return (404, "Not found user", result);

                result.UserId = userId;
                result.Name = existUser.Name;
                result.Email = existUser.Email;
                result.Family = existUser.Family;
                result.MobileNumber = existUser.MobileNumber;
                result.CreateDate = existUser.CreateDate;
                result.ProjectDescription = existUser.ProjectDescription;

                return (200, "Success to load user details", result);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result);
            }
        }

        public (int StatusCode, string Message, List<UserDto> UserList, int All) GetUsersList(int currentPage, int take, bool? ascending, string columnName)
        {
            var result = new List<UserDto>();
            try
            {
                var query = _context.Users.Where(s => !s.AccessAdminPanel).Select(s => new UserDto()
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    Family = s.Family,
                    CreateDate = s.CreateDate,
                    Email = s.Email,
                    MobileNumber = s.MobileNumber,
                    ProjectDescription =s.ProjectDescription
                });

                #region Sort
                if (ascending.HasValue)
                {
                    if (ascending == true)
                    {
                        //Ascending
                        query = query.OrderByProperty(columnName);
                    }
                    else
                    {
                        //Descending
                        query = query.OrderByPropertyDescending(columnName);
                    }
                }
                #endregion

                #region Pagination
                int all = query.Count();

                if (all == 0)
                    return (404, "Couldn't find any List", result, all);

                query = (from q in query
                         select q).Skip((currentPage - 1) * take)
                        .Take(take);
                #endregion

                return (200, "Success to load List", query.ToList(), all);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result, 0);
            }
        }

        public (int StatusCode, string Message) SendProjectRequest(SendRequestDto requestDto)
        {
            try
            {
                var newUser = new User()
                {
                    Name = requestDto.Name,
                    Family = requestDto.Family,
                    MobileNumber = requestDto.MobileNumber,
                    ProjectDescription = requestDto.ProjectDescription,
                    Email = requestDto.Email,
                    AccessAdminPanel = false,
                    CreateDate = DateTime.Now,
                    EmailCode = "default",
                    RefreshToken = "default",
                    RefreshTokenExpiryTime = DateTime.Now,
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();

                #region SendEmail
                var adminUser = _context.Users.SingleOrDefault(s => s.AccessAdminPanel);

                string body = _viewRender.RenderToStringAsync("RequestProject", newUser);
                SendingEmail.SendEmail(adminUser.Email, "RequestProject", body);
                #endregion

                return (200, "Success to send your request");
            }
            catch(Exception ex)
            {
                return (500, ex.Message);
            }
        }
        #endregion

        #region Skills
        public (int StatusCode, string Message) CreateNewSkill(CreateSkillDto skillDto)
        {
            try
            {
                var newSkill = new Skill()
                {
                    Name = skillDto.Name,
                    Percent = skillDto.Percent,
                    CreateDate = DateTime.Now,
                    IsDelete = false
                };

                _context.Skills.Add(newSkill);
                _context.SaveChanges();

                return (200, "Success to add new skill");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) UpdateSkillById(UpdateSkillDto skillDto)
        {
            try
            {
                var existSkill = _context.Skills.SingleOrDefault(s => s.SkillId == skillDto.SkillId);
                if (existSkill == null)
                    return (404, "Not found this skill");

                existSkill.Percent = skillDto.Percent;
                existSkill.Name = skillDto.Name;

                _context.Skills.Update(existSkill);
                _context.SaveChanges();

                return (200, "Success to update the skill.");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) DeleteSkillById(int skillId)
        {
            try
            {
                var existSkill = _context.Skills.SingleOrDefault(s => s.SkillId == skillId);
                if (existSkill == null)
                    return (404, "Not found any skill");

                existSkill.IsDelete = true;

                _context.Skills.Update(existSkill);
                _context.SaveChanges();

                return (200, "Success to delete skill");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message, SkillDto SkillInfo) GetSkillById(int skillId)
        {
            var result = new SkillDto();

            try
            {
                var existSkill = _context.Skills.SingleOrDefault(s => s.SkillId == skillId);
                if (existSkill == null)
                    return (404, "Not found skill", result);

                result.SkillId = existSkill.SkillId;
                result.Name = existSkill.Name;
                result.Percent = existSkill.Percent;
                result.CreateDate = existSkill.CreateDate;
                result.IsDelete = existSkill.IsDelete;

                return (200, "Success to load user details", result);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result);
            }
        }

        public (int StatusCode, string Message, List<SkillDto> SkillList, int All) GetSkillList(int currentPage, int take, bool? ascending, string columnName)
        {
            var result = new List<SkillDto>();
            try
            {
                var query = _context.Skills.Where(s=>!s.IsDelete).Select(s => new SkillDto()
                {
                    SkillId = s.SkillId,
                    Name = s.Name,
                    Percent = s.Percent,
                    CreateDate = s.CreateDate,
                    IsDelete = s.IsDelete,
                });

                #region Sort
                if (ascending.HasValue)
                {
                    if (ascending == true)
                    {
                        //Ascending
                        query = query.OrderByProperty(columnName);
                    }
                    else
                    {
                        //Descending
                        query = query.OrderByPropertyDescending(columnName);
                    }
                }
                #endregion

                #region Pagination
                int all = query.Count();

                if (all == 0)
                    return (404, "Couldn't find any List", result, all);

                query = (from q in query
                         select q).Skip((currentPage - 1) * take)
                        .Take(take);
                #endregion

                return (200, "Success to load List", query.ToList(), all);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result, 0);
            }
        }
        #endregion

        #region Services
        public (int StatusCode, string Message) CreateNewService(CreateService serviceDto)
        {
            try
            {                
                var newService = new Service()
                {
                    Name = serviceDto.Title,
                    Description = serviceDto.Description,
                    IconId = serviceDto.IconId,
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                };

                _context.Services.Add(newService);
                _context.SaveChanges();

                return (200, "Success to add new service");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) UpdateServiceById(UpdateServiceDto serviceDto)
        {
            try
            {

                var existService = _context.Services.SingleOrDefault(s => s.ServiceId == serviceDto.ServiceId);
                if (existService == null)
                    return (404, "Not found this skill");

                existService.Name = serviceDto.Title;
                existService.Description = serviceDto.Description;
                existService.IconId = serviceDto.IconId;

                _context.Services.Update(existService);
                _context.SaveChanges();

                return (200, "Success to update the skill.");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) DeleteServiceById(int serviceId)
        {
            try
            {
                var existService = _context.Services.SingleOrDefault(s => s.ServiceId == serviceId);
                if (existService == null)
                    return (404, "Not found any service");

                existService.IsDelete = true;

                _context.Services.Update(existService);
                _context.SaveChanges();

                return (200, "Success to delete service");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message, ServiceDto ServiceInfo) GetServiceById(int serviceId)
        {
            var result = new ServiceDto();
            try
            {
                var existService = _context.Services.SingleOrDefault(s => s.ServiceId == serviceId);
                if (existService == null)
                    return (404, "Not found any service", result);

                result.ServiceId = serviceId;
                result.Title = existService.Name;
                result.Description = existService.Description;
                result.IconId = existService.IconId;
                result.CreateDate = existService.CreateDate;
                result.IsDelete= existService.IsDelete;

                return (200, "Success to load service", result);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result);
            }
        }

        public (int StatusCode, string Message, List<ServiceDto> ServiceList, int All) GetServiceList(int currentPage, int take, bool? ascending, string columnName)
        {
            var result = new List<ServiceDto>();
            try
            {
                var query = _context.Services.Where(s => !s.IsDelete).Select(s => new ServiceDto()
                {
                    Description = s.Description,
                    IconId = s.IconId,
                    ServiceId = s.ServiceId,
                    Title = s.Name,
                    CreateDate = s.CreateDate,
                    IsDelete= s.IsDelete,
                });

                #region Sort
                if (ascending.HasValue)
                {
                    if (ascending == true)
                    {
                        //Ascending
                        query = query.OrderByProperty(columnName);
                    }
                    else
                    {
                        //Descending
                        query = query.OrderByPropertyDescending(columnName);
                    }
                }
                #endregion

                #region Pagination
                int all = query.Count();

                if (all == 0)
                    return (404, "Couldn't find any List", result, all);

                query = (from q in query
                         select q).Skip((currentPage - 1) * take)
                        .Take(take);
                #endregion

                return (200, "Success to load List", query.ToList(), all);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result, 0);
            }
        }
        #endregion

        #region Projects
        public (int StatusCode, string Message) CreateNewProject(CreateProject projectDto)
        {
            try
            {
                #region Add Picture

                #region url
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var url = MyConfig.GetValue<string>("UrlSetting:Url");
                #endregion

                string imgName = ToolService.GenerateUniqCode() + Path.GetExtension(projectDto.Picture.FileName);
                string folderName = "/Projects/Image/";
                string originalUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + folderName);

                #region add original File
                if (!Directory.Exists(originalUrl))
                {
                    Directory.CreateDirectory(originalUrl);
                }
                using (var stream = new FileStream(Path.Combine(originalUrl, imgName), FileMode.Create))
                {
                    projectDto.Picture.CopyTo(stream);
                }
                #endregion


                #endregion

                var newProject = new Project()
                {
                    Name = projectDto.Name,
                    Link = projectDto.Link,
                    Type = projectDto.Type,
                    Image = url + folderName + imgName,
                    CreateDate = DateTime.Now,
                    IsDelete= false,
                };

                _context.Projects.Add(newProject);
                _context.SaveChanges();

                return (200, "Success to add new project");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) UpdateProjectById(UpdateProjectDto projectDto)
        {
            try
            {
                var existProject = _context.Projects.SingleOrDefault(s => s.ProjectId == projectDto.ProjectId);
                if (existProject == null)
                    return (404, "Not found this project");

                if(projectDto.Picture!= null)
                {
                    #region Add Picture

                    #region url
                    var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var url = MyConfig.GetValue<string>("UrlSetting:Url");
                    #endregion

                    string imgName = ToolService.GenerateUniqCode() + Path.GetExtension(projectDto.Picture.FileName);
                    string foldername = "/Projects/Image/";
                    string originalUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + foldername);

                    #region add original File
                    if (!Directory.Exists(originalUrl))
                    {
                        Directory.CreateDirectory(originalUrl);
                    }
                    using (var stream = new FileStream(Path.Combine(originalUrl, imgName), FileMode.Create))
                    {
                        projectDto.Picture.CopyTo(stream);
                    }
                    #endregion

                    #endregion

                    //delete last picture
                    var s = originalUrl.Length;
                    var LastImage = existProject.Image.Remove(0, (url + foldername).Length);
                    if (File.Exists(originalUrl + LastImage))
                        File.Delete(originalUrl + LastImage);

                    existProject.Image = url + foldername + imgName;
                }

                existProject.Name = projectDto.Name;
                existProject.Link = projectDto.Link;
                existProject.Type = projectDto.Type;

                _context.Projects.Update(existProject);
                _context.SaveChanges();

                return (200, "Success to update the project.");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) DeleteProjectById(int projectId)
        {
            try
            {
                var existProject = _context.Projects.SingleOrDefault(s => s.ProjectId == projectId);
                if (existProject == null)
                    return (404, "Not found any project");

                existProject.IsDelete = true;

                _context.Projects.Update(existProject);
                _context.SaveChanges();

                return (200, "Success to delete project");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message, ProjectDto ProjectInfo) GetProjectById(int projectId)
        {
            var result = new ProjectDto();
            try
            {
                var existProject = _context.Projects.SingleOrDefault(s => s.ProjectId == projectId);
                if (existProject == null)
                    return (404, "Not found any project", result);

                result.ProjectId = projectId;
                result.Name = existProject.Name;
                result.Picture = existProject.Image;
                result.Link = existProject.Link;
                result.Type = existProject.Type;
                result.CreateDate = existProject.CreateDate;
                result.IsDelete = existProject.IsDelete;

                return (200, "Success to load service", result);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result);
            }
        }

        public (int StatusCode, string Message, List<ProjectDto> ProjectList, int All) GetProjectList(int currentPage, int take, bool? ascending, string columnName, string? type)
        {
            var result = new List<ProjectDto>();
            try
            {
                var query = _context.Projects.Where(s => !s.IsDelete).Select(s => new ProjectDto()
                {
                    Name = s.Name,
                    Type = s.Type,
                    Link = s.Link,
                    ProjectId = s.ProjectId,
                    Picture = s.Image,
                    CreateDate = s.CreateDate,
                    IsDelete = s.IsDelete,
                });

                if (type != null)
                    query = query.Where(s => s.Type == type);

                #region Sort
                if (ascending.HasValue)
                {
                    if (ascending == true)
                    {
                        //Ascending
                        query = query.OrderByProperty(columnName);
                    }
                    else
                    {
                        //Descending
                        query = query.OrderByPropertyDescending(columnName);
                    }
                }
                #endregion

                #region Pagination
                int all = query.Count();

                if (all == 0)
                    return (404, "Couldn't find any List", result, all);

                query = (from q in query
                         select q).Skip((currentPage - 1) * take)
                        .Take(take);
                #endregion

                return (200, "Success to load List", query.ToList(), all);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result, 0);
            }
        }

        #endregion

        #region Setting
        public (int StatusCode, string Message) CreateNewSetting(CreateGeneralSettingDto infoDto)
        {
            try
            {
                var existSetting = _context.GeneralSettings.SingleOrDefault(s => s.Type == infoDto.Type);
                if (existSetting != null)
                    return (400, "A Setting with this type is exist");

                var newSetting = new GeneralSetting()
                {
                    Type = infoDto.Type,
                    Name = infoDto.Name,
                    Value = infoDto.Value,
                    CreateDate = DateTime.Now,
                    IsDelete = false
                };

                _context.GeneralSettings.Add(newSetting);
                _context.SaveChanges();

                return (200, "Success to add a new setting");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) UpdateSettingById(UpdateGeneralSettingDto infoDto)
        {
            try
            {
                var existSetting = _context.GeneralSettings.SingleOrDefault(s => s.GeneralSettingId == infoDto.GeneralSettingId);
                if (existSetting == null)
                    return (404, "Can't find any setting");

                existSetting.Name = infoDto.Name;
                existSetting.Value = infoDto.Value; ;

                _context.GeneralSettings.Update(existSetting);
                _context.SaveChanges();

                return (200, "Success to update setting");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message) DeleteSettingById(int settingId)
        {
            try
            {
                var existSetting = _context.GeneralSettings.SingleOrDefault(s => s.GeneralSettingId == settingId);
                if (existSetting == null)
                    return (404, "Not found any setting");

                existSetting.IsDelete = true;

                _context.GeneralSettings.Update(existSetting);
                _context.SaveChanges();

                return (200, "Success to delete setting");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public (int StatusCode, string Message, GeneralSettingDto GeneralSettingInfo) GetSettingById(int settingId)
        {
            var result = new GeneralSettingDto();
            try
            {
                var existProject = _context.GeneralSettings.SingleOrDefault(s => s.GeneralSettingId == settingId);
                if (existProject == null)
                    return (404, "Not found any setting", result);

                result.GeneralSettingId = settingId;
                result.Value = existProject.Value;
                result.Name = existProject.Name;
                result.Type = existProject.Type;
                result.CreateDate = existProject.CreateDate;
                result.IsDelete= existProject.IsDelete;

                return (200, "Success to load service", result);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result);
            }
        }

        public (int StatusCode, string Message, GeneralSettingDto GeneralSettingInfo) GetSettingByType(string type)
        {
            var result = new GeneralSettingDto();
            try
            {
                var existProject = _context.GeneralSettings.SingleOrDefault(s => s.Type == type);
                if (existProject == null)
                    return (404, "Not found any setting", result);

                result.GeneralSettingId = existProject.GeneralSettingId;
                result.Value = existProject.Value;
                result.Name = existProject.Name;
                result.Type = existProject.Type;
                result.CreateDate = existProject.CreateDate;
                result.IsDelete = existProject.IsDelete;

                return (200, "Success to load service", result);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result);
            }
        }

        public (int StatusCode, string Message, List<GeneralSettingDto> GeneralSettingInfo, int All) GetSettingsList(int currentPage, int take, bool? ascending, string columnName)
        {
            var result = new List<GeneralSettingDto>();
            try
            {
                var query = _context.GeneralSettings.Where(s => !s.IsDelete).Select(s => new GeneralSettingDto()
                {
                    GeneralSettingId = s.GeneralSettingId,
                    Value = s.Value,
                    Name = s.Name,
                    Type = s.Type,
                    CreateDate = s.CreateDate,
                    IsDelete = s.IsDelete,
                });

                #region Sort
                if (ascending.HasValue)
                {
                    if (ascending == true)
                    {
                        //Ascending
                        query = query.OrderByProperty(columnName);
                    }
                    else
                    {
                        //Descending
                        query = query.OrderByPropertyDescending(columnName);
                    }
                }
                #endregion

                #region Pagination
                int all = query.Count();

                if (all == 0)
                    return (404, "Couldn't find any List", result, all);

                query = (from q in query
                         select q).Skip((currentPage - 1) * take)
                        .Take(take);
                #endregion

                return (200, "Success to load List", query.ToList(), all);
            }
            catch (Exception ex)
            {
                return (500, ex.Message, result, 0);
            }
        }
        #endregion
    }
}
