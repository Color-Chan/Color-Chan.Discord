using System;
using System.Collections.Generic;
using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Rest.Models.Embed;

namespace Color_Chan.Discord.Commands
{
    /// <summary>
    ///     Represents a builder class for creating <see cref="IDiscordEmbed" />s.
    /// </summary>
    public class DiscordEmbedBuilder
    {
        /// <summary>
        ///     The <see cref="IDiscordEmbedAuthor" /> used for the <see cref="DiscordEmbed.Author" />.
        /// </summary>
        private IDiscordEmbedAuthor? _author;

        /// <summary>
        ///     The <see cref="_color" /> used for the <see cref="DiscordEmbed.Color" />.
        /// </summary>
        private Color? _color;

        /// <summary>
        ///     The description used for the <see cref="DiscordEmbed.Description" />.
        /// </summary>
        private string? _description;

        /// <summary>
        ///     The <see cref="IDiscordEmbedField" />s used for the <see cref="DiscordEmbed.Fields" />.
        /// </summary>
        private List<IDiscordEmbedField>? _fields;

        /// <summary>
        ///     The <see cref="IDiscordEmbedFooter" /> used for the <see cref="DiscordEmbed.Footer" />.
        /// </summary>
        private IDiscordEmbedFooter? _footer;

        /// <summary>
        ///     The <see cref="IDiscordEmbedImage" /> used for the <see cref="DiscordEmbed.Image" />.
        /// </summary>
        private IDiscordEmbedImage? _image;

        /// <summary>
        ///     The <see cref="IDiscordEmbedProvider" /> used for the <see cref="DiscordEmbed.Provider" />.
        /// </summary>
        private IDiscordEmbedProvider? _provider;

        /// <summary>
        ///     The <see cref="IDiscordEmbedThumbnail" /> used for the <see cref="DiscordEmbed.Thumbnail" />.
        /// </summary>
        private IDiscordEmbedThumbnail? _thumbnail;

        /// <summary>
        ///     The <see cref="DateTimeOffset" /> used for the <see cref="DiscordEmbed.Timestamp" />.
        /// </summary>
        private DateTimeOffset? _timestamp;

        /// <summary>
        ///     The title used for the <see cref="DiscordEmbed.Title" />.
        /// </summary>
        private string? _title;

        /// <summary>
        ///     The type used for the <see cref="DiscordEmbed.Type" />.
        /// </summary>
        private DiscordEmbedType? _type;

        /// <summary>
        ///     The url used for the <see cref="DiscordEmbed.Url" />.
        /// </summary>
        private string? _url;

        /// <summary>
        ///     The <see cref="IDiscordEmbedVideo" /> used for the <see cref="DiscordEmbed.Video" />.
        /// </summary>
        private IDiscordEmbedVideo? _video;

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Title" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="title">The embed title.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Type" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="type">The embed type.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithType(DiscordEmbedType type)
        {
            _type = type;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Description" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="description">The embed description.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Url" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="url">The embed url.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Url" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="timeStamp">The embed timestamp.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithTimeStamp(DateTimeOffset timeStamp)
        {
            _timestamp = timeStamp;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Url" /> of the <see cref="DiscordEmbed" />.
        ///     With the current UTC time.
        /// </summary>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithTimeStamp()
        {
            _timestamp = DateTimeOffset.UtcNow;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Color" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="color">The embed color.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithColor(Color color)
        {
            _color = color;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Footer" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="text">The text of the footer</param>
        /// <param name="iconUrl">The icon url of the footer.</param>
        /// <param name="proxyIconUrl">The proxy icon url of the footer.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithFooter(string text, string? iconUrl = null, string? proxyIconUrl = null)
        {
            _footer = new DiscordEmbedFooter
            {
                Text = text,
                IconUrl = iconUrl,
                ProxyIconUrl = proxyIconUrl
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Footer" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="footer">The footer of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithFooter(DiscordEmbedFooter footer)
        {
            _footer = footer;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Image" /> of the <see cref="DiscordEmbed" />.
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
            _image = new DiscordEmbedImage
            {
                Url = imageUrl,
                ProxyUrl = imageProxyUrl,
                Width = width,
                Height = height
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Image" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="image">The image of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithImage(DiscordEmbedImage image)
        {
            _image = image;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Thumbnail" /> of the <see cref="DiscordEmbed" />.
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
            _thumbnail = new DiscordEmbedThumbnail
            {
                Url = imageUrl,
                ProxyUrl = imageProxyUrl,
                Width = width,
                Height = height
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Thumbnail" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="thumbnail">The thumbnail of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithThumbnail(DiscordEmbedThumbnail thumbnail)
        {
            _thumbnail = thumbnail;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Video" /> of the <see cref="DiscordEmbed" />.
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
            _video = new DiscordEmbedVideo
            {
                Url = imageUrl,
                ProxyUrl = imageProxyUrl,
                Width = width,
                Height = height
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Video" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="video">The video of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithVideo(DiscordEmbedVideo video)
        {
            _video = video;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Provider" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="name">The provider name.</param>
        /// <param name="url">The provider url. Default is null.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithProvider(string name, string? url = null)
        {
            _provider = new DiscordEmbedProvider
            {
                Name = name,
                Url = url
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Provider" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="provider">The provider of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithProvider(DiscordEmbedProvider provider)
        {
            _provider = provider;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Author" /> of the <see cref="DiscordEmbed" />.
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
            _author = new DiscordEmbedAuthor
            {
                Name = name,
                Url = url,
                IconUrl = iconUrl,
                ProxyIconUrl = proxyIconUrl
            };
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="DiscordEmbed.Author" /> of the <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="author">The author of the embed.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithAuthor(DiscordEmbedAuthor author)
        {
            _author = author;
            return this;
        }

        /// <summary>
        ///     Adds a <see cref="DiscordEmbedField" /> to the <see cref="DiscordEmbed.Fields" /> of the
        ///     <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="isInline">Whether or not the field is inline.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithField(string name, string value, bool? isInline = null)
        {
            _fields ??= new List<IDiscordEmbedField>();

            _fields.Add(new DiscordEmbedField
            {
                Name = name,
                Value = value,
                Inline = isInline
            });
            return this;
        }

        /// <summary>
        ///     Adds a <see cref="DiscordEmbedField" /> to the <see cref="DiscordEmbed.Fields" /> of the
        ///     <see cref="DiscordEmbed" />.
        /// </summary>
        /// <param name="field">The new field.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithField(DiscordEmbedField field)
        {
            _fields ??= new List<IDiscordEmbedField>();

            _fields.Add(field);
            return this;
        }

        /// <summary>
        ///     Adds <see cref="DiscordEmbedField" />s to the <see cref="DiscordEmbed.Fields" /> of the <see cref="DiscordEmbed" />
        ///     .
        /// </summary>
        /// <param name="fields">The new fields.</param>
        /// <returns>
        ///     The updated <see cref="DiscordEmbedBuilder" />.
        /// </returns>
        public DiscordEmbedBuilder WithFields(IEnumerable<DiscordEmbedField> fields)
        {
            _fields ??= new List<IDiscordEmbedField>();

            _fields.AddRange(fields);
            return this;
        }

        /// <summary>
        ///     Builds the <see cref="DiscordEmbed" /> with the current set values.
        /// </summary>
        /// <returns>
        ///     The generated <see cref="DiscordEmbed" />.
        /// </returns>
        public IDiscordEmbed Build()
        {
            return new DiscordEmbed
            {
                Title = _title,
                Type = _type,
                Description = _description,
                Url = _url,
                Timestamp = _timestamp,
                Color = _color,
                Footer = _footer,
                Image = _image,
                Thumbnail = _thumbnail,
                Video = _video,
                Provider = _provider,
                Author = _author,
                Fields = _fields
            };
        }
    }
}