using Microsoft.Extensions.DependencyInjection;
using Nover.Video.Application.Contracts.YouTube;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.SettingManagement;

namespace Nover.Video.Application.Contracts;

[DependsOn(
    typeof(NoverDomainSharedModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpObjectExtendingModule)
    )]
public class NoverApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<YouTubeOAuthOptions>(configuration.GetSection("YouTobeOAuth"));
    }
}
