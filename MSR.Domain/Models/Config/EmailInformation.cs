namespace MSR.Domain.Models.Config
{
    public class EmailInformation
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string PasswordReminderEmailsTo { get; set; }
        public string From { get; set; }
        public string SupportEmail { get; set; }
        public string SendEmailsTo { get; set; }
        public string ConfigSet { get; set; }
    }
}
