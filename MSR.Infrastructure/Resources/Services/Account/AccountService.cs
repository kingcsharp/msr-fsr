using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using Newtonsoft.Json;
using MSR.Infrastructure.Helpers.Abstractions;
using MSR.Domain.Abstractions.Services.Workflow;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MSR.Infrastructure.Resources.Services.Account
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly JwtData _jwtData;
        private readonly EmailInformation _emailInformation;
        private readonly GeneralInformation _generalInformation;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IWorkflowService _workflowService;

        public AccountService(
            IUnitOfWork unitOfWork,
            ILogger<AccountService> logger,
            IMapper mapper,
            IEmailService emailService,
            JwtData jwtData,
            EmailInformation emailInformation,
            GeneralInformation generalInformation,
            IAuthenticationHelper authenticationHelper,
            IWorkflowService workflowService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
            _jwtData = jwtData;
            _emailInformation = emailInformation;
            _generalInformation = generalInformation;
            _authenticationHelper = authenticationHelper;
            _workflowService = workflowService;
        }

        public async Task<string> LoginAsync(SystemLogin command)
        {
            var user = await _unitOfWork.Users.Query()
                .Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.Menus).ThenInclude(x => x.MenuRolePermission)
                .Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.Menus).ThenInclude(x => x.MenuItem).ThenInclude(i => i.MenuGroup)
                .Where(x => x.UserName == command.UserName)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new DomainException("Username Or Password are invalid", DomainError.NotFound);
            }

            if (!_authenticationHelper.VerifyPasswordHash(command.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new DomainException("Username Or Password are invalid", DomainError.NotFound);
            }

            return await GetJWTToken(user);
        }

        public async Task ForgotPasswordAsync(ForgotPassword command)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.UserName == command.UserName);

            if (user == null)
            {
                return;
            }

            var from = _emailInformation.From;
            var websiteUrl = _generalInformation.WebsiteURL;

            var encryptedText = EncryptionHelper.Encrypt(command.UserName).Replace('/', '*');
            var encodedText = System.Net.WebUtility.UrlEncode(encryptedText);

            var lnkHref = $"<a href='{websiteUrl}/#/resetpassword/{encodedText}'>Reset Password</a>";

            var body = $@"<div>
               <p>Hello ANSWER user,<br/></p>
               <p>This email is being sent to you due to a password reset request from the MSR-FSR Answer system.<br/></p>
               <p><b> Please reset your password by clicking : </ b ><br/> </p>
               <p>{lnkHref}</p>
                        </div>";

            var subject = "ANSWER - Reset password";
            try
            {
                await _emailService.SendEmailAsync(from, user.Email, subject, body, null, true);
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }
        }

        public async Task ResetPasswordAsync(ResetPassword command)
        {
            var userName = EncryptionHelper.Decrypt(command.Token);
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.UserName == userName);

            if (user == null)
            {
                throw new DomainException("User not found", DomainError.NotFound);
            }

            if (!ValidatePassword(command.Password, out string error))
            {
                throw new DomainException(error, DomainError.Conflict);
            }

            Domain.Helpers.CurrentUser.GetId = () => user.Id;

            _authenticationHelper.CreatePasswordHash(command.Password, out var hash, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ForgotUserNameAsync(ForgotUserName command)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.Email == command.Email);

            if (user == null)
            {
                throw new DomainException($"No user with {nameof(command.Email)} {command.Email} found", DomainError.NotFound);
            }

            var from = _emailInformation.From;

            var body = $"Hello ANSWER user, <br/><br/> <b>Your username is : {user.UserName}</b>";

            var subject = "ANSWER - Forgot Username";

            try
            {
                await _emailService.SendEmailAsync(from, command.Email, subject, body, null, true);
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message, DomainError.InternalServerError);
            }
        }

        public bool ValidateAccount(int accountId)
        {
            var exists = _unitOfWork.Users.Exist(accountId);

            if (!exists)
            {
                throw new DomainException($"No user with {nameof(accountId)} {accountId} found", DomainError.NotFound);
            }

            return exists;
        }

        public async Task ResetMyPasswordAsync(ResetMyPassword command)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == CurrentUser.GetId());

            if (user == null)
            {
                throw new DomainException("User not found", DomainError.NotFound);
            }

            if (!_authenticationHelper.VerifyPasswordHash(command.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new DomainException("Current password is invalid", DomainError.Conflict);
            }

            if (!ValidatePassword(command.NewPassword, out string error))
            {
                throw new DomainException(error, DomainError.Conflict);
            }

            _authenticationHelper.CreatePasswordHash(command.NewPassword, out var hash, out var salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<string> GetJWTToken(EntityFramework.Entities.User efUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtData.Secret);
            var userPrivileges = JsonConvert.SerializeObject(GetTokenUserRoles(efUser));
            
            var approvalPrivileges = JsonConvert.SerializeObject(await GetTokenUserActivityRoles(efUser));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, efUser.Id.ToString()),
                    new Claim("Privileges",userPrivileges),
                    new Claim("ApprovalPrivileges",approvalPrivileges)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Index 0 of the Array is for CanRead
        /// Index 1 of the Array is for CanApprove Activities
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<Dictionary<int, int[]>> GetTokenUserActivityRoles(User user)
        {
            var allMyActivitiesPrivileges = await _workflowService.GetAllMyActivitiesPrivileges(user.Id, user.Roles.Select(x => x.RoleId).ToList());
            var canApproveMenuItemRoles = await _unitOfWork.MenuRoles.Query()
                .Select(x => new { x.MenuItemId, x.RoleId, x.MenuRolePermission }).Distinct().ToListAsync();

            var result = new Dictionary<int, int[]>();

            foreach (var workflowLinkModel in allMyActivitiesPrivileges)
            {
                foreach (var item in canApproveMenuItemRoles)
                {
                    if (item.MenuItemId == workflowLinkModel.MenuItemId && workflowLinkModel.RoleIds != null && workflowLinkModel.RoleIds.Contains(item.RoleId))
                    {
                        var key = (int)EnumUtils.GetValueFromDescription<EnumApprovalTables>(workflowLinkModel.ApprovalTableName);
                        var privileges = GetListEnumPrivileges(item.MenuRolePermission);
                        if (result.ContainsKey(key))
                        {
                            result.TryGetValue(key, out int[] arrVal);
                            result[key] = privileges.Union(arrVal).ToArray();
                        }
                        else
                        {
                            result.Add(key, privileges);
                        }

                    }
                }
            }

            return result;
        }

        private static int[][] GetTokenUserRoles(User user)
        {
            var totalMenuItems = Enum.GetNames(typeof(EnumMenuItem)).Length;
            var jaggedArray = new int[totalMenuItems][];
            foreach (var role in user.Roles ?? new List<UserRole>())
            {
                var efRole = role.Role;
                if (efRole != null)
                {
                    foreach (var menuItem in efRole.Menus)
                    {
                        if (menuItem.MenuItem == null) continue;
                        var efMenuItem = menuItem.MenuItem;

                        var menuItemNum = (int)EnumUtils.ParseMenuType(efMenuItem.Name);
                        var permissions = GetListEnumPrivileges(menuItem.MenuRolePermission);
                        if (jaggedArray[menuItemNum] == null)
                        {
                            jaggedArray[menuItemNum] = permissions;
                        }
                        else
                        {
                            jaggedArray[menuItemNum] = jaggedArray[menuItemNum].Union(permissions).ToArray();
                        }

                    }
                }
            }

            return jaggedArray;
        }

        private static int[] GetListEnumPrivileges(MenuRolePermission menuRolePermission)
        {
            var listEnumPrivilege = new List<int>();
            if (menuRolePermission != null)
            {
                if (menuRolePermission.CanActivate)
                {
                    listEnumPrivilege.Add((int)EnumPrivilege.CanActivate);
                }
                if (menuRolePermission.CanApprove)
                {
                    listEnumPrivilege.Add((int)EnumPrivilege.CanApprove);
                }
                if (menuRolePermission.CanCreate)
                {
                    listEnumPrivilege.Add((int)EnumPrivilege.CanCreate);
                }
                if (menuRolePermission.CanDelete)
                {
                    listEnumPrivilege.Add((int)EnumPrivilege.CanDelete);
                }
                if (menuRolePermission.CanEdit)
                {
                    listEnumPrivilege.Add((int)EnumPrivilege.CanEdit);
                }
                if (menuRolePermission.CanRead)
                {
                    listEnumPrivilege.Add((int)EnumPrivilege.CanRead);
                }
            }

            return listEnumPrivilege.ToArray();
        }

        private bool ValidatePassword(string passwordText, out string error, int minimumLength = 8, int maximumLength = 12,
            int minimumNumbers = 1, int minimumSpecialCharacters = 1, int minLetters = 1, int minLowerCase = 1, int minUpperCase = 1)
        {
            //Assumes that special characters are anything except upper and lower case letters and digits
            //Assumes that ASCII is being used (not suitable for many languages)
            error = "";
            int letters = 0;
            int digits = 0;
            int lowerCase = 0;
            int upperCase = 0;
            int specialCharacters = 0;
            //var error = "";
            var allowPassword = true;

            //Make sure there are enough total characters
            if (passwordText.Length < minimumLength)
            {
                error += "You must have at least " + minimumLength + " characters in your password. ";
                allowPassword = false;
            }

            //Make sure there are enough total characters
            if (passwordText.Length > maximumLength)
            {
                error += "You must have no more than " + maximumLength + " characters in your password. ";
                allowPassword = false;
            }

            foreach (var ch in passwordText)
            {
                if (char.IsLetter(ch)) letters++; //increment letters
                if (char.IsDigit(ch)) digits++; //increment digits
                if (char.IsLower(ch)) lowerCase++; //increment digits
                if (char.IsUpper(ch)) upperCase++; //increment digits

                //Test for only letters and numbers...
                if (!((ch > 47 && ch < 58) || (ch > 64 && ch < 91) || (ch > 96 && ch < 123)))
                {
                    specialCharacters++;
                }
            }
            if (lowerCase < minLowerCase)
            {
                error += "You must have at least " + minLetters + " lower case in your password.";
                allowPassword = false;
            }

            if (upperCase < minUpperCase)
            {
                error += "You must have at least " + minLetters + " upper case in your password.";
                allowPassword = false;
            }

            if (letters < minLetters)
            {
                error += "You must have at least " + minLetters + " letters in your password.";
                allowPassword = false;
            }

            //Make sure there are enough digits
            if (digits < minimumNumbers)
            {
                error += "You must have at least " + minimumNumbers + " numbers in your password.";
                allowPassword = false;
            }

            //Make sure there are enough special characters -- !(a-zA-Z0-9)
            if (specialCharacters < minimumSpecialCharacters)
            {
                error += "You must have at least " + minimumSpecialCharacters + " special characters (like @,$,%,#) in your password.";
                allowPassword = false;
            }

            return allowPassword;
        }
    }
}
