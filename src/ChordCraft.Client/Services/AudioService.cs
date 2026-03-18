using Microsoft.JSInterop;

namespace ChordCraft.Client.Services;

public class AudioService
{
    private readonly IJSRuntime _js;
    private bool _initialized;

    public AudioService(IJSRuntime js) => _js = js;

    public async Task InitAsync()
    {
        if (!_initialized)
        {
            await _js.InvokeVoidAsync("ChordCraftAudio.init");
            _initialized = true;
        }
    }

    public async Task PlayKeypressAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.playKeypress");
    public async Task PlayErrorAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.playError");
    public async Task PlaySuccessAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.playSuccess");
    public async Task<bool> ToggleAsync() => await _js.InvokeAsync<bool>("ChordCraftAudio.toggle");
}
