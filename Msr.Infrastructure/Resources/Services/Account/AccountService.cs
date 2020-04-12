using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
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
        private readonly JwtData _jwtData;

        public AccountService(IUnitOfWork unitOfWork, ILogger<AccountService> logger, IMapper mapper, JwtData jwtData)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _jwtData = jwtData;
        }

        public async Task<User> LoginAsync(SystemLogin command)
        {
            var encryptedPassword = AuthenticationHelper.PasswordEncrypt(command.Password);
            var user = _unitOfWork.Users?.FirstOrDefault(false,i => i.UserName == command.UserName);

            if(user == null)
            {
                throw new DomainException("Username Or Password are invalid");
            }

            var domainUser = _mapper.Map<User>(user);

            SetJWTToken(domainUser);

            return await Task.FromResult(domainUser);
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
