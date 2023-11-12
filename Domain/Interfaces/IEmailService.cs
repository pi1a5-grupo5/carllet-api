﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        void SendResetPasswordEmail(User user);
        void SendConfirmationEmail(User user);
        public void SendEmail(string html, string to, string subject);
    }
}
