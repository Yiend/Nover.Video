using Nover.Video.Localization;
using Volo.Abp.Application.Services;

namespace Nover.Video;

public abstract class NoverAppService : ApplicationService
{
    protected NoverAppService()
    {
        LocalizationResource = typeof(VideoResource);
        ObjectMapperContext = typeof(NoverApplicationModule);
    }
}
