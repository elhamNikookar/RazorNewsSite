function OnDocumentInit() {
    jQuery('[data-toggle="tooltip"]').tooltip();

    jQuery('.main-slider').owlCarousel({
        rtl: true,
        loop: true,
        margin: 8,
        nav: true,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });
}

function initSlider() {
    let currentSlide = 0;
    const slides = document.querySelectorAll('.slides li');

    function showSlide(index) {
        slides.forEach(slide => slide.classList.remove('active'));
        slides[index].classList.add('active');
    }

    window.changeSlide = function (direction) {
        currentSlide = (currentSlide + direction + slides.length) % slides.length;
        showSlide(currentSlide);
    };

    setInterval(() => window.changeSlide(1), 5000);

    showSlide(currentSlide);
}
