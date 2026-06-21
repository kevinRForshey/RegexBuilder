// Development service worker — pass-through, no caching.
// The published version (service-worker.published.js) handles offline caching.
self.addEventListener('fetch', () => {});
