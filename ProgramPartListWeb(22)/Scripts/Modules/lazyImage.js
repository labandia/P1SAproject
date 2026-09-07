export function initLazyImages() {
    const lazyImages = document.querySelectorAll("img.lazy");

    if ("IntersectionObserver" in window) {
        const observer = new IntersectionObserver((entries, obs) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.src = img.dataset.src;

                    img.onload = () => img.classList.add("loaded");

                    img.removeAttribute("data-src");
                    obs.unobserve(img);
                }
            });
        });

        lazyImages.forEach(img => observer.observe(img));
    } else {
        lazyImages.forEach(img => {
            img.src = img.dataset.src;
            img.onload = () => img.classList.add("loaded");
            img.removeAttribute("data-src");
        });
    }
}