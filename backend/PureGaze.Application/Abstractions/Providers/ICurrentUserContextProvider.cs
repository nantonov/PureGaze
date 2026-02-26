namespace PureGaze.Application.Abstractions.Providers;

public interface ICurrentUserContextProvider
{
    string GetUserEmail();
}
