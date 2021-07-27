using Color_Chan.Discord.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Extensions
{
    public static class MvcBuilderExtensions
    {
        /// <summary>
        ///     Adds the required JSON options to the <see cref="IMvcBuilder" />.
        /// </summary>
        /// <param name="mvcBuilder">The <see cref="IMvcBuilder" />.</param>
        /// <returns>
        ///     The <see cref="IMvcBuilder" /> with new JSON options.
        /// </returns>
        public static IMvcBuilder AddColorChanJson(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(options => { options.JsonSerializerOptions.RegisterJsonOptions(); });

            return mvcBuilder;
        }
    }
}