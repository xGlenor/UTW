using System.Security.Claims;
using BaseLibrary.GenericModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Site.Auth;

public class AuthStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            string stringToken = await localStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrWhiteSpace(stringToken))
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claims = Generics.GetClaimsFromToken(stringToken);

            var claimsPrincipal = Generics.SetClaimPrincipal(claims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(string? token)
    {
        ClaimsPrincipal claimsPrincipal;
        if (!string.IsNullOrWhiteSpace(token))
        {
            var userSession = Generics.GetClaimsFromToken(token);
            claimsPrincipal = Generics.SetClaimPrincipal(userSession);
            await localStorageService.SetItemAsStringAsync("token", token);
        }
        else
        {
            claimsPrincipal = _anonymous;
            await localStorageService.RemoveItemAsync("token");
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}