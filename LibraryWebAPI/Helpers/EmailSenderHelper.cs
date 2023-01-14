using LibraryWebAPI.Controllers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Security.Authentication;

namespace LibraryWebAPI.Helpers
{
    public class EmailSenderHelper
    {
        /// <summary>
        /// 郵件伺服器網址
        /// </summary>
        public string HostUrl { get; set; }
        /// <summary>
        /// 連接埠
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        ///  是否使用 SSL
        /// </summary>
        public bool UseSsl { get; set; }
        /// <summary>
        /// 驗證使用者帳號
        /// </summary>
        public string AuthAccount { get; set; }
        /// <summary>
        /// 驗證密碼
        /// </summary>
        public string AuthPassword { get; set; }


        private readonly IConfiguration _configuration;
        private readonly ILogger<TestController> _logger;
        public EmailSenderHelper(IConfiguration configuration, ILogger<TestController> logger)
        {
            _configuration = configuration;
            _logger = logger;

            HostUrl = _configuration["SMTP:HostUrl"];
            Port = int.Parse(_configuration["SMTP:Port"]);
            UseSsl = bool.Parse(_configuration["SMTP:UseSsl"]);
            AuthAccount = _configuration["SMTP:AuthAccount"];
            AuthPassword = _configuration["SMTP:AuthPassword"];
        }

        public EmailSenderHelper()
        {

        }

        /// <summary>
        /// 寄信
        /// </summary>
        /// <param name="subject">郵件標題</param>
        /// <param name="body">郵件內容</param>
        /// <param name="receiver">收件人</param>
        /// <param name="receiverEmail">收件人Email</param>
        /// <returns></returns>
        public bool Send(string subject, string body, string receiver, string receiverEmail)
        {
            var message = new MimeMessage();
            //寄件人
            message.From.Add(new MailboxAddress("Library系統自動寄信", "noreply@library.com"));
            //收件人
            message.To.Add(new MailboxAddress(receiver, receiverEmail));
            //郵件標題
            message.Subject = subject;
            //郵件內容
            message.Body = new TextPart(TextFormat.Text)
            {
                Text = body
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;

                    client.Connect(HostUrl, Port, UseSsl);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(AuthAccount, AuthPassword);

                    // 寄出郵件
                    client.Send(message);
                    // 中斷連線
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }
    }
}
