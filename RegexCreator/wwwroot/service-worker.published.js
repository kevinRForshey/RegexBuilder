// Production service worker — caches all assets from the Blazor asset manifest.
const cacheName = 'regexcreator-v1';
const offlineAssetsInclude = [/\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff2$/, /\.png$/, /\.ico$/];
const offlineAssetsExclude = [/^service-worker\.js$/];

self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

async function onInstall(event) {
    self.skipWaiting();
    const assetsManifest = await (await fetch('service-worker-assets.js', { cache: 'no-cache' })).json();
    const assets = assetsManifest.assets
        .filter(a => offlineAssetsInclude.some(r => r.test(a.url)))
        .filter(a => !offlineAssetsExclude.some(r => r.test(a.url)))
        .map(a => new Request(a.url, { integrity: a.hash, cache: 'no-cache' }));
    const cache = await caches.open(cacheName);
    await cache.addAll(assets);
}

async function onActivate(event) {
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys.filter(k => k !== cacheName).map(k => caches.delete(k)));
    await clients.claim();
}

async function onFetch(event) {
    if (event.request.method !== 'GET') return fetch(event.request);
    const cached = await caches.match(event.request, { ignoreSearch: true });
    return cached ?? fetch(event.request);
}
