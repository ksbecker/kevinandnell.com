export function onLoad() {
    init();

    const mobileNavToggleBtnSelector = ".mobile-nav-toggle",
        navMenuAnchorsSelector = "#navmenu a";

    let mobileNavToggleBtn = void 0,
        navMenuAnchors = void 0;

    const _queryElements = () => {
        mobileNavToggleBtn = document.querySelector(mobileNavToggleBtnSelector);
        navMenuAnchors = document.querySelectorAll(navMenuAnchorsSelector);
    };

    const _mobileNavToggle = () => {
        document.querySelector("body").classList.toggle("mobile-nav-active");
        mobileNavToggleBtn.classList.toggle("bi-list");
        mobileNavToggleBtn.classList.toggle("bi-x");
    };

    _queryElements();

    if (mobileNavToggleBtn) {
        mobileNavToggleBtn.addEventListener("click", _mobileNavToggle);
    }

    /**
     * Hide mobile nav on same-page/hash links
     */
    if (navMenuAnchors) {
        navMenuAnchors.forEach((navmenu) => {
            navmenu.addEventListener("click", () => {
                if (document.querySelector(".mobile-nav-active")) {
                    _mobileNavToggle();
                }
            });
        });
    }

    /**
     * Toggle mobile nav dropdowns
     */
    document.querySelectorAll(".navmenu .toggle-dropdown").forEach((navmenu) => {
        navmenu.addEventListener("click", function (e) {
            e.preventDefault();
            this.parentNode.classList.toggle("active");
            this.parentNode.nextElementSibling.classList.toggle("dropdown-active");
            e.stopImmediatePropagation();
        });
    });

  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'G-6YNME1EDCL');
}

export function onUpdate() {
    init();
}

export function onDispose() {
}

function init() {
    AOS.init({
        duration: 600,
        easing: "ease-in-out",
        once: true,
        mirror: false,
    });
}