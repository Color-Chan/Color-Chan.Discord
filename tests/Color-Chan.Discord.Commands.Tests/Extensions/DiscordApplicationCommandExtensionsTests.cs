using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Rest.Models.Application;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Extensions
{
    [TestFixture]
    public class DiscordApplicationCommandExtensionsTests
    {
        [SetUp]
        public void SetUp()
        {
            NewCommands = new List<DiscordCreateApplicationCommand>
            {
                new()
                {
                    Description = "Command1",
                    Name = "commandName",
                    DefaultPermission = false,
                    Options = new List<DiscordApplicationCommandOptionData>
                    {
                        new()
                        {
                            Name = "choice1",
                            Description = "choiceDesc1",
                            Type = DiscordApplicationCommandOptionType.Channel,
                            IsRequired = true,
                            Choices = new List<DiscordApplicationCommandOptionChoiceData>
                            {
                                new()
                                {
                                    Name = "name1",
                                    Value = "value1"
                                },
                                new()
                                {
                                    Name = "name2",
                                    Value = "value2"
                                },
                                new()
                                {
                                    Name = "name3",
                                    Value = "value3"
                                },
                                new()
                                {
                                    Name = "name4",
                                    Value = "value4"
                                }
                            }
                        },
                        new()
                        {
                            Name = "choice2",
                            Description = "choiceDesc2",
                            Type = DiscordApplicationCommandOptionType.Channel,
                            IsRequired = true,
                            Choices = new List<DiscordApplicationCommandOptionChoiceData>
                            {
                                new()
                                {
                                    Name = "name1",
                                    Value = "value1"
                                },
                                new()
                                {
                                    Name = "name2",
                                    Value = "value2"
                                },
                                new()
                                {
                                    Name = "name3",
                                    Value = "value3"
                                },
                                new()
                                {
                                    Name = "name4",
                                    Value = "value4"
                                }
                            }
                        }
                    }
                }
            };

            ExistingCommands = new List<DiscordApplicationCommandData>
            {
                new()
                {
                    Description = "Command1",
                    Name = "commandName",
                    DefaultPermission = false,
                    Options = new List<DiscordApplicationCommandOptionData>
                    {
                        new()
                        {
                            Name = "choice1",
                            Description = "choiceDesc1",
                            Type = DiscordApplicationCommandOptionType.Channel,
                            IsRequired = true,
                            Choices = new List<DiscordApplicationCommandOptionChoiceData>
                            {
                                new()
                                {
                                    Name = "name1",
                                    Value = "value1"
                                },
                                new()
                                {
                                    Name = "name2",
                                    Value = "value2"
                                },
                                new()
                                {
                                    Name = "name3",
                                    Value = "value3"
                                },
                                new()
                                {
                                    Name = "name4",
                                    Value = "value4"
                                }
                            }
                        },
                        new()
                        {
                            Name = "choice2",
                            Description = "choiceDesc2",
                            Type = DiscordApplicationCommandOptionType.Channel,
                            IsRequired = true,
                            Choices = new List<DiscordApplicationCommandOptionChoiceData>
                            {
                                new()
                                {
                                    Name = "name1",
                                    Value = "value1"
                                },
                                new()
                                {
                                    Name = "name2",
                                    Value = "value2"
                                },
                                new()
                                {
                                    Name = "name3",
                                    Value = "value3"
                                },
                                new()
                                {
                                    Name = "name4",
                                    Value = "value4"
                                }
                            }
                        }
                    }
                }
            };
        }

        private List<DiscordCreateApplicationCommand> NewCommands { get; set; } = null!;

        private static List<DiscordApplicationCommandData> ExistingCommands { get; set; } = null!;

        [Test]
        public void Should_not_detect_any_new_commands()
        {
            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void Should_detect_new_choices()
        {
            // Arrange
            NewCommands[0]!.Options!.FirstOrDefault()!.Choices = NewCommands[0]!.Options!.FirstOrDefault()!.Choices!.Append(new DiscordApplicationCommandOptionChoiceData
            {
                Name = "choice5",
                Value = "choiceValue5"
            });

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_option()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Options = new List<DiscordApplicationCommandOptionData>(NewCommands[0].Options!)
                {
                    new()
                    {
                        Name = "test",
                        Description = "test"
                    }
                }
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_deleted_options()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Options = new List<DiscordApplicationCommandOptionData>()
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options()
        {
            // Arrange
            ExistingCommands[0] = ExistingCommands[0] with
            {
                Options = null
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_command_with_name()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Name = "newName"
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_command_with_desc()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Description = "new desc"
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_command_with_defaultPerm()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                DefaultPermission = true
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options_with_name()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Options = new List<DiscordApplicationCommandOptionData>
                {
                    new()
                    {
                        Name = NewCommands[0].Options!.FirstOrDefault()!.Name,
                        Description = NewCommands[0].Options!.FirstOrDefault()!.Description,
                        Choices = NewCommands[0].Options!.FirstOrDefault()!.Choices,
                        Type = NewCommands[0].Options!.FirstOrDefault()!.Type,
                        IsRequired = NewCommands[0].Options!.FirstOrDefault()!.IsRequired,
                        SubOptions = NewCommands[0].Options!.FirstOrDefault()!.SubOptions
                    },
                    new()
                    {
                        Name = "test",
                        Description = "test"
                    }
                }
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options_with_desc()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Options = new List<DiscordApplicationCommandOptionData>
                {
                    new()
                    {
                        Name = NewCommands[0].Options!.FirstOrDefault()!.Name,
                        Description = NewCommands[0].Options!.FirstOrDefault()!.Description,
                        Choices = NewCommands[0].Options!.FirstOrDefault()!.Choices,
                        Type = NewCommands[0].Options!.FirstOrDefault()!.Type,
                        IsRequired = NewCommands[0].Options!.FirstOrDefault()!.IsRequired,
                        SubOptions = NewCommands[0].Options!.FirstOrDefault()!.SubOptions
                    },
                    new()
                    {
                        Name = NewCommands[0].Options!.LastOrDefault()!.Name,
                        Description = "test"
                    }
                }
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options_with_IsRequired()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Options = new List<DiscordApplicationCommandOptionData>
                {
                    new()
                    {
                        Name = NewCommands[0].Options!.FirstOrDefault()!.Name,
                        Description = NewCommands[0].Options!.FirstOrDefault()!.Description,
                        Choices = NewCommands[0].Options!.FirstOrDefault()!.Choices,
                        Type = NewCommands[0].Options!.FirstOrDefault()!.Type,
                        IsRequired = NewCommands[0].Options!.FirstOrDefault()!.IsRequired,
                        SubOptions = NewCommands[0].Options!.FirstOrDefault()!.SubOptions
                    },
                    new()
                    {
                        Name = NewCommands[0].Options!.LastOrDefault()!.Name,
                        Description = NewCommands[0].Options!.LastOrDefault()!.Description,
                        IsRequired = false
                    }
                }
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options_with_Type()
        {
            // Arrange
            NewCommands[0] = NewCommands[0] with
            {
                Options = new List<DiscordApplicationCommandOptionData>
                {
                    new()
                    {
                        Name = NewCommands[0].Options!.FirstOrDefault()!.Name,
                        Description = NewCommands[0].Options!.FirstOrDefault()!.Description,
                        Choices = NewCommands[0].Options!.FirstOrDefault()!.Choices,
                        Type = NewCommands[0].Options!.FirstOrDefault()!.Type,
                        IsRequired = NewCommands[0].Options!.FirstOrDefault()!.IsRequired,
                        SubOptions = NewCommands[0].Options!.FirstOrDefault()!.SubOptions
                    },
                    new()
                    {
                        Name = NewCommands[0].Options!.LastOrDefault()!.Name,
                        Description = NewCommands[0].Options!.LastOrDefault()!.Description,
                        IsRequired = NewCommands[0].Options!.LastOrDefault()!.IsRequired,
                        Type = DiscordApplicationCommandOptionType.Role
                    }
                }
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options_with_new_choice_name()
        {
            // Arrange
            NewCommands[0].Options!.FirstOrDefault()!.Choices = new List<DiscordApplicationCommandOptionChoiceData>
            {
                new()
                {
                    Name = "new choice"
                },
                new(),
                new(),
                new()
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Should_detect_new_options_with_new_choice_value()
        {
            // Arrange
            NewCommands[0].Options!.FirstOrDefault()!.Choices = new List<DiscordApplicationCommandOptionChoiceData>
            {
                new()
                {
                    Name = NewCommands[0].Options!.FirstOrDefault()!.Choices!.FirstOrDefault()!.Name,
                    Value = "new value"
                },
                new(),
                new(),
                new()
            };

            // Act
            var result = NewCommands.GetUpdatedOrNewCommands(ExistingCommands.Select(x => new DiscordApplicationCommand(x)).ToList());

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}