namespace Api.Response
{
    public record LoginResponse(
        string AccessToken,
        string RefreshToken);
}
