using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Services.Implementations;
using FluentAssertions;
using NUnit.Framework;
using Sodium;

namespace Color_Chan.Discord.Tests.Services.Implementations
{
    [TestFixture]
    public class DiscordInteractionAuthServiceTests
    {
        [TestCase(@"{""version"":""v4.0.0.0"",""utcBuildDate"":""2021-08-01T23:34:09.553Z""}")]
        [TestCase(@"{""id"":""9999999999999999"",""application_id"":""9999999999999999"",""type"":3,""data"":{""id"":""9999999999999999"",""name"":""test"",""options"":[{""name"":""test"",
                    ""type"":3,""value"":""test_test""}]},""guild_id"":""9999999999999999"",""channel_id"":""9999999999999999"",""member"":{""user"":{""id"":""9999999999999999"",""username"":
                    ""user"",""discriminator"":""0001"",""avatar"":""aaaaaaa"",""bot"":true,""system"":true,""mfa_enabled"":true,""locale"":""en-US"",""verified"":true,""email"":
                    ""color-chan@discord.com"",""flags"":0,""premium_type"":0,""public_flags"":0},""nick"":""testNick"",""roles"":[""9999999999999999"",""9999999999999999"",""9999999999999999""],
                    ""joined_at"":""1970-01-01T00:00:00+00:00"",""deaf"":true,""mute"":true,""permissions"":""274877906943""},""token"":""token"",""version"":1}")]
        [TestCase(@"{""id"":""999999999999"",""application_id"":""999999999999"",""type"":2,""data"":{""id"":""999999999999"",""name"":""roles"",""resolved"":{""users"":{""999999999999"":
                    {""id"":""999999999999"",""username"":""name"",""discriminator"":""9999"",""avatar"":""aaaaaaaaaaaaa"",""bot"":true,""public_flags"":0}},""members"":{""999999999999"":{""roles"":
                    [""999999999999"",""999999999999""],""joined_at"":""2020-01-21T23:01:24.057+00:00"",""deaf"":false,""mute"":false,""pending"":false,""permissions"":""248407129665""}},""channels"":
                    {""999999999999"":{""id"":""999999999999"",""type"":0,""name"":""all""}}},""options"":[{""name"":""add"",""type"":2,""options"":[{""name"":""empty"",""type"":1,""options"":[{""name"":
                    ""name"",""type"":3,""value"":""test role name""}]}]}]},""guild_id"":""999999999999"",""channel_id"":""999999999999"",""member"":{""user"":{""id"":""9999999999999999"",""username"":
                    ""user"",""discriminator"":""0001"",""avatar"":""aaaaaaa"",""bot"":true,""system"":true,""mfa_enabled"":true,""locale"":""en-US"",""verified"":true,""email"":""color-chan@discord.com"",
                    ""flags"":0,""premium_type"":0,""public_flags"":0},""nick"":""testNick"",""roles"":[""9999999999999999"",""9999999999999999"",""9999999999999999""],""joined_at"":""1970-01-01T00:00:00+00:00"",
                    ""deaf"":true,""mute"":true,""permissions"":""274877906943""},""token"":""TOKEN"",""version"":1}")]
        public async Task Should_verify_body_content(string bodyString)
        {
            await RunTestAsync(bodyString, true);
            await RunTestAsync(bodyString, false);
        }

        private async Task RunTestAsync(string bodyString, bool expected)
        {
            // Arrange
            var key = PublicKeyAuth.GenerateKeyPair();
            var timeStamp = (int)DateTimeOffset.UnixEpoch.Offset.TotalSeconds;
            var signed = PublicKeyAuth.SignDetached($"{timeStamp.ToString()}{bodyString}", key.PrivateKey);
            var hexEd25519String = Convert.ToHexString(signed);

            if (!expected) bodyString = bodyString.Replace("version", "fake_version");

            var byteArray = Encoding.UTF8.GetBytes(bodyString);
            var stream = new MemoryStream(byteArray);

            var service = new DiscordInteractionAuthService(new DiscordTokens("TOKEN", Convert.ToHexString(key.PublicKey), 0));

            // Act
            var syncResult = service.VerifySignature(hexEd25519String, timeStamp.ToString(), bodyString);
            var asyncResult = await service.VerifySignatureAsync(hexEd25519String, timeStamp.ToString(), stream).ConfigureAwait(false);

            // Assert
            syncResult.Should().Be(expected);
            asyncResult.Should().Be(expected);
        }
    }
}