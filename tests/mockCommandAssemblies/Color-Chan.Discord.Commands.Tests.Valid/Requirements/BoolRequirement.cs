using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core;

namespace Color_Chan.Discord.Commands.Tests.Valid.Requirements
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class BoolRequirement : SlashCommandRequirementAttribute
    {
        private readonly bool _value;

        public BoolRequirement(bool value)
        {
            _value = value;
        }

        /// <inheritdoc />
        public override Task<SlashCommandRequirementResult> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            return Task.FromResult(_value ? new SlashCommandRequirementResult(_value) : new SlashCommandRequirementResult(_value, "value was false."));
        }
    }
}