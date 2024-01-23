using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;

namespace Nover.Video.Core
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAutoMapperModule)
    )]
    public class NoverCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NoverCoreModule>();
            });
        }
    }

}


