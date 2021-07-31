using System;
using System.Collections.Generic;
using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Rest.Models.Embed;

namespace Color_Chan.Discord
{
    /// <summary>
    ///     Represents a builder class for creating <see cref="DiscordEmbed"/>s.
    /// </summary>
    public class DiscordEmbedBuilder
    {
        /// <summary>
        ///     The title used for the <see cref="DiscordEmbed.Title"/>.
        /// </summary>
        public string? Title;
        
        /// <summary>
        ///     The type used for the <see cref="DiscordEmbed.Type"/>.
        /// </summary>
        public DiscordEmbedType? Type;
        
        /// <summary>
        ///     The description used for the <see cref="DiscordEmbed.Description"/>.
        /// </summary>
        public string? Description;
        
        /// <summary>
        ///     The url used for the <see cref="DiscordEmbed.Url"/>.
        /// </summary>
        public string? Url;
        
        /// <summary>
        ///     The <see cref="DateTimeOffset"/> used for the <see cref="DiscordEmbed.Timestamp"/>.
        /// </summary>
        public DateTimeOffset? Timestamp;
        
        /// <summary>
        ///     The <see cref="Color"/> used for the <see cref="DiscordEmbed.Color"/>.
        /// </summary>
        public Color? Color;
        
        /// <summary>
        ///     The <see cref="IDiscordEmbedFooter"/> used for the <see cref="DiscordEmbed.Footer"/>.
        /// </summary>
        public IDiscordEmbedFooter? Footer;
        
        /// <summary>
        ///     The <see cref="IDiscordEmbedImage"/> used for the <see cref="DiscordEmbed.Image"/>.
        /// </summary>
        public IDiscordEmbedImage? Image;
        
        /// <summary>
        ///     The <see cref="IDiscordEmbedThumbnail"/> used for the <see cref="DiscordEmbed.Thumbnail"/>.
        /// </summary>
        public IDiscordEmbedThumbnail? Thumbnail;
        
        /// <summary>
        ///     The <see cref="IDiscordEmbedVideo"/> used for the <see cref="DiscordEmbed.Video"/>.
        /// </summary>
        public IDiscordEmbedVideo? Video;
        
        /// <summary>
        ///     The <see cref="IDiscordEmbedProvider"/> used for the <see cref="DiscordEmbed.Provider"/>.
        /// </summary>
        public IDiscordEmbedProvider? Provider;
        
        /// <summary>
        ///     The <see cref="IDiscordEmbedAuthor"/> used for the <see cref="DiscordEmbed.Author"/>.
        /// </summary>
        public IDiscordEmbedAuthor? Author;
                
        /// <summary>
        ///     The <see cref="IDiscordEmbedField"/>s used for the <see cref="DiscordEmbed.Fields"/>.
        /// </summary>
        public List<IDiscordEmbedField>? Fields;

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Title" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="title">The embed title.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Type" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="type">The embed type.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithType(DiscordEmbedType type)
        {
            Type = type;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Description" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="description">The embed description.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Url" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="url">The embed url.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithUrl(string url)
        {
            Url = url;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Url" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="timeStamp">The embed timestamp.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithTimeStamp(DateTimeOffset timeStamp)
        {
            Timestamp = timeStamp;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Url" /> of the <see cref="DiscordEmbed"/>.
        ///     With the current UTC time.
        /// </summary>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithTimeStamp()
        {
            Timestamp = DateTimeOffset.UtcNow;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Color" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="color">The embed color.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithColor(Color color)
        {
            Color = color;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Footer" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="text">The text of the footer</param>
        /// <param name="iconUrl">The icon url of the footer.</param>
        /// <param name="proxyIconUrl">The proxy icon url of the footer.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithFooter(string text, string? iconUrl = null, string? proxyIconUrl = null)
        {
            Footer = new DiscordEmbedFooter
            {
                Text = text,
                IconUrl = iconUrl,
                ProxyIconUrl = proxyIconUrl
            };
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Footer" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="footer">The footer of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithFooter(DiscordEmbedFooter footer)
        {
            Footer = footer;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Image" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageProxyUrl">The proxy image URL. Default is null.</param>
        /// <param name="width">The width of the image. Default is null.</param>
        /// <param name="height">The height of the image. Default is null.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithImage(string imageUrl, string? imageProxyUrl = null, int? width = null, int? height = null)
        {
            Image = new DiscordEmbedImage
            {
                Url = imageUrl,
                ProxyUrl = imageProxyUrl,
                Width = width,
                Height = height
            };
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Image" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="image">The image of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithImage(DiscordEmbedImage image)
        {
            Image = image;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Thumbnail" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageProxyUrl">The proxy image URL. Default is null.</param>
        /// <param name="width">The width of the image. Default is null.</param>
        /// <param name="height">The height of the image. Default is null.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithThumbnail(string imageUrl, string? imageProxyUrl = null, int? width = null, int? height = null)
        {
            Thumbnail = new DiscordEmbedThumbnail
            {
                Url = imageUrl,
                ProxyUrl = imageProxyUrl,
                Width = width,
                Height = height
            };
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Thumbnail" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="thumbnail">The thumbnail of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithThumbnail(DiscordEmbedThumbnail thumbnail)
        {
            Thumbnail = thumbnail;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Video" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="imageProxyUrl">The proxy image URL. Default is null.</param>
        /// <param name="width">The width of the image. Default is null.</param>
        /// <param name="height">The height of the image. Default is null.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithVideo(string imageUrl, string? imageProxyUrl = null, int? width = null, int? height = null)
        {
            Video = new DiscordEmbedVideo
            {
                Url = imageUrl,
                ProxyUrl = imageProxyUrl,
                Width = width,
                Height = height
            };
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Video" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="video">The video of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithVideo(DiscordEmbedVideo video)
        {
            Video = video;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Provider" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="name">The provider name.</param>
        /// <param name="url">The provider url. Default is null.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithProvider(string name, string? url = null)
        {
            Provider = new DiscordEmbedProvider
            {
                Name = name,
                Url = url
            };
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Provider" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="provider">The provider of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithProvider(DiscordEmbedProvider provider)
        {
            Provider = provider;
            return this;
        }
        
        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Author" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="name">The name of the author.</param>
        /// <param name="url">The url of the author. Default is null.</param>
        /// <param name="iconUrl">The icon url of the author. only supports http(s) and attachments. Default is null.</param>
        /// <param name="proxyIconUrl">The proxied icon url of the author. Default is null.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithAuthor(string name, string? url = null, string? iconUrl = null, string? proxyIconUrl = null)
        {
            Author = new DiscordEmbedAuthor
            {
                Name = name,
                Url = url,
                IconUrl = iconUrl,
                ProxyIconUrl = proxyIconUrl
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Author" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="author">The author of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithAuthor(DiscordEmbedAuthor author)
        {
            Author = author;
            return this;
        }
        
        /// <summary>
        ///     Adds a <see cref="DiscordEmbedField"/> to the <see cref="DiscordEmbed.Fields" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="isInline">Whether or not the field is inline.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithField(string name, string value, bool? isInline = null)
        {
            Fields ??= new List<IDiscordEmbedField>();
            
            Fields.Add(new DiscordEmbedField
            {
                Name = name,
                Value = value,
                Inline = isInline
            });
            return this;
        }
        
        /// <summary>
        ///     Adds a <see cref="DiscordEmbedField"/> to the <see cref="DiscordEmbed.Fields" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="field">The new field.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithField(DiscordEmbedField field)
        {
            Fields ??= new List<IDiscordEmbedField>();
            
            Fields.Add(field);
            return this;
        }
        
        /// <summary>
        ///     Adds <see cref="DiscordEmbedField"/>s to the <see cref="DiscordEmbed.Fields" /> of the <see cref="DiscordEmbed"/>.
        /// </summary>
        /// <param name="fields">The new fields.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithFields(IEnumerable<DiscordEmbedField> fields)
        {
            Fields ??= new List<IDiscordEmbedField>();
            
            Fields.AddRange(fields);
            return this;
        }
        
        /// <summary>
        ///     Builds the <see cref="DiscordEmbed"/> with the current set values.
        /// </summary>
        /// <returns>
        ///     The generated <see cref="DiscordEmbed"/>.
        /// </returns>
        public DiscordEmbed Build()
        {
            return new()
            {
                Title = Title,
                Type = Type,
                Description = Description,
                Url = Url,
                Timestamp = Timestamp,
                Color = Color,
                Footer = Footer,
                Image = Image,
                Thumbnail = Thumbnail,
                Video = Video,
                Provider = Provider,
                Author = Author,
                Fields = Fields
            };
        }
    }
}