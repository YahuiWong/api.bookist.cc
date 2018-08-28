using Microsoft.Extensions.Options;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Net.Http;

namespace Qiniu
{
    public class QiniuService
    {
        private readonly QiniuOptions _options;
        private readonly Mac _mac;
        private readonly Zone _zone;

        public QiniuService(IOptions<QiniuOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _mac = new Mac(_options.AccessKey, _options.SecretKey);
            // _zone = Zone.Create(Enum.Parse<ZoneType>(_options.Zone, true));
            _zone = Zone.Create(_options.Zone);
        }

        public string GetToken()
        {
            PutPolicy policy = new PutPolicy { Scope = _options.Bucket };
            policy.SetExpires(60 * 3);
            policy.FsizeLimit = 5 * 1024 * 1024;
            policy.ReturnBody = "{\"key\":\"$(key)\",\"width\":\"$(imageInfo.width)\",\"height\":\"$(imageInfo.height)\"}";

            return Auth.CreateUploadToken(_mac, policy.ToJsonString());
        }

        public FetchInfo FetchUpload(string targetUrl, string key)
        {
            var config = new Config { Zone = _zone };

            var bm = new BucketManager(_mac, config);

            //var fetchUrl = string.Format("{0}{1}", 
            //    config.IovipHost(_mac.AccessKey, _options.Bucket), bm.FetchOp(targetUrl, _options.Bucket, key));

            var result = bm.Fetch(targetUrl, _options.Bucket, key);

            if (result.Code != 200)
            {
                throw new HttpRequestException("调用七牛云抓取并存储文件失败！" + result.Text ?? result.RefText);
            }
            return result.Result;
        }
    }
}
