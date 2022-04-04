using System.Globalization;
using System.Resources;

namespace Sample.Identity.App.Extensions
{
    public static class ResourceExtension
    {
        private static ResourceSet GetResources(string culture)
        {
            CultureInfo info;

            try
            {
                info = new CultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                info = Thread.CurrentThread.CurrentUICulture;
            }

            ResourceManager resources = new ResourceManager("Sample.Identity.App.Resources.Resource", typeof(ResourceExtension).Assembly);

            return resources.GetResourceSet(CultureInfo.CreateSpecificCulture(info.Name), true, false);
        }

        public static string Get(string key, string culture = "en-US")
        {
            ResourceSet set = GetResources(culture);

            return set.GetString(key);
        }
    }
}