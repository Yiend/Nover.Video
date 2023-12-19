using Nover.Video.Localization;
using Volo.Abp.Application.Services;

namespace Nover.Video;

public abstract class NoverAppService : ApplicationService
{
    protected NoverAppService()
    {
        LocalizationResource = typeof(NoverResource);
        ObjectMapperContext = typeof(NoverApplicationModule);
    }
}
