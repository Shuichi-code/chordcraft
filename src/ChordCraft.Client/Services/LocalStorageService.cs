using Microsoft.JSInterop;

namespace ChordCraft.Client.Services;

public class LocalStorageService
{
    private readonly IJSRuntime _js;
    public LocalStorageService(IJSRuntime js) => _js = js;

    public async Task<string?> GetAsync(string key)
        => await _js.InvokeAsync<string?>("ChordCraftStorage.get", key);

    public async Task SetAsync(string key, string value)
        => await _js.InvokeVoidAsync("ChordCraftStorage.set", key, value);

    public async Task RemoveAsync(string key)
        => await _js.InvokeVoidAsync("ChordCraftStorage.remove", key);
}
