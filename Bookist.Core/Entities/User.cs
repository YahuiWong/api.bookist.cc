using Anet;

namespace Bookist.Core.Entities
{
    public class User : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Varchar(20)]
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Varchar(50)]
        public string Email { get; set; }

        /// <summary>
        /// 密码Hash
        /// </summary>
        [Varchar(200)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Varchar(100)]
        public string Avatar { get; set; }
    }
}
