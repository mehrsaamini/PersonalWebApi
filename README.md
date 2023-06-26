This project is a small .Net Core Web API Project using .Net Core 7.0, JWT, Swagger.<br />
APIs of this project have been used to introduce a person. Skills, services and projects this person has done so far.<br />
Also, the general settings that a person wants to have, such as the site title, footer content, etc., are also included in this project.<br />
Finally, an Api for other users to access you (such as completing the contact form or ordering the project) is also considered,<br />
and the site administrator, who will be you, can access the information of users who have filled out the form by logging in.<br />
Also, immediately after filling out the contact form (or project order), an email containing user information will be sent to you.<br />
We have authorization, Register, Login, Send Email, Refresh Token, Logout methods using JWT. This scenario(personal website) doesn't need Register Method,
but you can use at first for AccessToken & RefreshToken.

# PersonalWebApi
After Register yourself as an admin, for login you should call **SendEmail** Method, then in **Login** insert your email & code(receive from your email)<br />
**Remember that you should use token like this --> bearer token**<br />
Now you Access CRUD of Projects, Skills, Services, General Settings, Users List.<br />

# PersonalWeb.DataLayer
Entities & Context are defined in this layer, our entities are: **User** , **Project** , **Skill** , **Service** , **GeneralSetting**

# PersonalWeb.BusinessLayer
Logic of project, Helper Classes, DTO(Classes that are transferred between layers)  are defined in this layer.
