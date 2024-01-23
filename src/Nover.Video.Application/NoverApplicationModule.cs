using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Threading;
using Hangfire;
using Hangfire.SQLite;
using Nover.Video.Application.Contracts;
using Nover.Video.Workers;

namespace Nover.Video;

[DependsOn(
    typeof(NoverDomainModule),
    typeof(NoverApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpBackgroundJobsHangfireModule),
    typeof(AbpBackgroundWorkersHangfireModule)
    )]
public class NoverApplicationModule : AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => context.AddBackgroundWorkerAsync<YouTubeBackgroundWorker>());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        PreConfigure<IGlobalConfiguration>(hangfireConfiguration =>
        {
            hangfireConfiguration.UseSQLiteStorage(configuration.GetConnectionString("Default"));
        });

        context.Services.AddAutoMapperObjectMapper<NoverApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<NoverApplicationModule>(validate: true);
        });

        
    }
}
