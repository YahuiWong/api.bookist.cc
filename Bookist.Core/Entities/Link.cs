using Anet;

namespace Bookist.Core.Entities
{
    public class Link : Entity
    {
        public long BookId { get; set; }

        /// <summary>
        /// 下载链接
        /// </summary>
        [Varchar(200)]
        public string Url { get; set; }

        /// <summary>
        /// 访问代码
        /// </summary>
        [Varchar(50)]
        public string Code { get; set; }

        /// <summary>
        /// 图书格式
        /// </summary>
        public BookFormat Format { get; set; }

        public Book Book { get; set; }
    }
}
