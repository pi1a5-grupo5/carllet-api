﻿namespace Application.ViewModels.User
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}