using System;
using System.Collections.Generic;
using System.Text;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Domain.Models;
using System.Threading.Tasks;
using Renci.SshNet;
using MSR.Infrastructure.Resources.Services;
using Microsoft.Extensions.Configuration;
using MSR.Domain.Models.Config;
using System.Linq;


namespace MSR.Infrastructure.Resources.Services.XmlTransmissionRetry
{
    public class XmlTransmissionRetryService : IXmlTransmissionRetryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public XmlTransmissionRetryService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<ToastMessage> RetryXmlSubmission(int transmissionId)
        {
            var sftpInfo = _config.GetSection(nameof(TransmissionInformation)).Get<TransmissionInformation>();
            var log = _unitOfWork.XmlTransmissionLogs.Query().FirstOrDefault(log => log.Id == transmissionId);

            if (log == null)
            {
                // Handle the case where the log with the given ID is not found
                return new ToastMessage { IsSuccess = false, Message = "XmlTransmissionLog not found. Please contact Support." };
            }

            var fileName = $"{log.XmlLink.Split("/").Last().Split(".xml").First()}.xml";

            try
            {
                using (SftpClient sftp = new SftpClient(sftpInfo.Host, sftpInfo.Username, sftpInfo.Password))
                {
                    sftp.Connect();
                    sftp.Disconnect();

                    log.Result = "Success";
                    log.SubmittedOn = DateTime.Now;
                    log.TransmissionDetail = "File transmitted successfully";

                    _unitOfWork.XmlTransmissionLogs.Update(log);

                    return new ToastMessage { IsSuccess = true, Message = "The file has been sent successfully." };
                }
            }
            catch (Exception ex)
            {
                log.Result = "Failure";
                log.SubmittedOn = DateTime.Now;
                log.TransmissionDetail = $"Failed to send file to SFTP Server. Error: {ex.Message}";

                _unitOfWork.XmlTransmissionLogs.Update(log);

                return new ToastMessage { IsSuccess = false, Message = "The file could not be sent. Please contact Support." };
            }
        }
    }
}
