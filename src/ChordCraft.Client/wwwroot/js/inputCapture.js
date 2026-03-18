window.ChordCraftInput = {
    _dotNetRef: null,

    register: function (dotNetRef) {
        this._dotNetRef = dotNetRef;
        this._onKeyDown = (e) => {
            if (e.key.length === 1 || e.key === 'Backspace' || e.key === 'Tab' || e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                dotNetRef.invokeMethodAsync('OnKeyDown', e.key, performance.now());
            }
        };
        document.addEventListener('keydown', this._onKeyDown);
    },

    unregister: function () {
        if (this._onKeyDown) {
            document.removeEventListener('keydown', this._onKeyDown);
            this._onKeyDown = null;
        }
        this._dotNetRef = null;
    }
};
