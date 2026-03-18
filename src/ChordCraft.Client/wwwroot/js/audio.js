window.ChordCraftAudio = {
    _ctx: null,
    _buffers: {},
    _enabled: true,

    init: async function () {
        try {
            this._ctx = new AudioContext();
        } catch (e) {
            console.warn('Web Audio not available');
        }
    },

    toggle: function () {
        this._enabled = !this._enabled;
        return this._enabled;
    },

    playTone: function (frequency, duration, type) {
        if (!this._ctx || !this._enabled) return;
        var osc = this._ctx.createOscillator();
        var gain = this._ctx.createGain();
        osc.type = type || 'sine';
        osc.frequency.value = frequency;
        gain.gain.value = 0.1;
        gain.gain.exponentialRampToValueAtTime(0.001, this._ctx.currentTime + duration);
        osc.connect(gain);
        gain.connect(this._ctx.destination);
        osc.start();
        osc.stop(this._ctx.currentTime + duration);
    },

    playKeypress: function () { this.playTone(800, 0.05, 'sine'); },
    playError: function () { this.playTone(200, 0.15, 'square'); },
    playSuccess: function () {
        this.playTone(523, 0.1, 'sine');
        setTimeout(() => this.playTone(659, 0.1, 'sine'), 100);
        setTimeout(() => this.playTone(784, 0.2, 'sine'), 200);
    }
};
