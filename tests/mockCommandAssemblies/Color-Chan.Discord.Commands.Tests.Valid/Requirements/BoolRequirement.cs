using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Results;

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
        public override Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            return Task.FromResult(_value ? Result.FromSuccess() : Result.FromError(new ErrorResult("Input was false")));
        }
    }
}