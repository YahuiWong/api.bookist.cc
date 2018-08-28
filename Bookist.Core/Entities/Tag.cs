using Anet;

namespace Bookist.Core.Entities
{
    public class Tag : Entity
    {
        /// <summary>
        /// 标签名
        /// </summary>
        [Varchar(20)]
        public string Name { get; set; }
    }
}
