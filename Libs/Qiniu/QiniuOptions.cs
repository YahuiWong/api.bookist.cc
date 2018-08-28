using Qiniu.Storage;

namespace Qiniu
{
    public class QiniuOptions
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Bucket { get; set; }
        public ZoneType Zone { get; set; }

        /// <summary>
        /// 上传Token过期时长（单位：秒，默认：180秒）
        /// </summary>
        public int TokenExpires { get; set; } = 180;
    }
}
