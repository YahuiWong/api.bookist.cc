using Qiniu;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QiniuExtensions
    {
        public static void AddQiniu(this IServiceCollection services, Action<QiniuOptions> setupAction)
        {
            services.AddScoped<QiniuService>();
            services.Configure(setupAction);
        }
    }
}
