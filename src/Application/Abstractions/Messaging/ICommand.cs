using EGG.CleanAspire.Domain.Common;

namespace EGG.CleanAspire.Application.Abstractions.Messaging;

public interface ICommand : ICommand<Result>;

public interface ICommand<TResponse>;
