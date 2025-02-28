namespace WhiskyKing.Core.Models.Requests;

public class ChangeUserPasswordRequest
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string RepeatNewPassword { get; set; } = string.Empty;
}