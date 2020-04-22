using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Commanding.Emums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.Services.Account.Abstractions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Account
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly JwtData _jwtData;
        private readonly EmailInformation _emailInformation;
        private readonly GeneralInformation _generalInformation;

        public AccountService(
            IUnitOfWork unitOfWork,
            ILogger<AccountService> logger,
            IMapper mapper,
            IEmailService emailService,
            JwtData jwtData,
            EmailInformation emailInformation,
            GeneralInformation generalInformation)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _emailService = emailService;
            _jwtData = jwtData;
            _emailInformation = emailInformation;
            _generalInformation = generalInformation;
        }

        public async Task<User> LoginAsync(SystemLogin command)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.UserName == command.UserName);

            if (user == null)
            {
                throw new DomainException("Username Or Password are invalid");
            }

            if (!AuthenticationHelper.VerifyPasswordHash(command.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new DomainException("Username Or Password are invalid");
            }

            var domainUser = _mapper.Map<User>(user);

            SetJWTToken(domainUser);

            return await Task.FromResult(domainUser);
        }

        public async Task ForgotPasswordAsync(ForgotPassword command)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.UserName == command.UserName);

            if (user == null)
            {
                return;
            }

            var from = _emailInformation.From;
            //var websiteUrl = _generalInformation.WebsiteURL;
            //TODO REPLACE FOR THE CORRECT ui URL
            var websiteUrl = "http://localhost:3000/";

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

            AuthenticationHelper.CreatePasswordHash(command.Password, out var hash, out var salt);
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

        public User ValidateAccount(int accountId)
        {
            var user = _unitOfWork.Users.FirstOrDefault(false, i => i.Id == accountId);

            if (user == null)
            {
                throw new DomainException($"No user with {nameof(accountId)} {accountId} found", DomainError.NotFound);
            }

            return _mapper.Map<User>(user);
        }

        private void SetJWTToken(User domainUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtData.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, domainUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            domainUser.Token = tokenHandler.WriteToken(token);
        }
    }
}
