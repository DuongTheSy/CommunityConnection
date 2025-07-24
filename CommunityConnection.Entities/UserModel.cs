using System.ComponentModel.DataAnnotations;

namespace CommunityConnection.Entities
{
    public class UserModel
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên người dùng là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên người dùng không được vượt quá 50 ký tự.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Đường dẫn avatar không được vượt quá 255 ký tự.")]
        public string? AvatarUrl { get; set; }

        [StringLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự.")]
        public string? Description { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(1000, ErrorMessage = "Mô tả kỹ năng không được vượt quá 1000 ký tự.")]
        public string? DescriptionSkill { get; set; }

        public long? RoleId { get; set; }
    }
}
