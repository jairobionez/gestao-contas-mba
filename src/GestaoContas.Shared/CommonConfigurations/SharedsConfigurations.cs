using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace GestaoContas.Shared.CommonConfigurations
{
    public static class SharedsConfigurations
    {
        public static WebApplicationBuilder AddSharedsConfiguration(this WebApplicationBuilder builder)
        {
            var sharedSettingsPath = builder.Configuration["SharedSettingsPath"];
            if (!string.IsNullOrEmpty(sharedSettingsPath))
            {
                var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..","..", sharedSettingsPath);

                if (!string.IsNullOrEmpty(sharedSettingsPath))
                {
                    builder.Configuration.AddJsonFile(sharedFolder, optional: false, reloadOnChange: true);
                }
            }
            return builder;
        }
    }
}
