// Array gambar mobil di Page rentCar
let slideIndex = 0;
const slides = ["car1.jpg", "car2.jpg", "car3.jpg"];

function changeSlide(n) {
    slideIndex = (slideIndex + n + slides.length) % slides.length;
    document.getElementById("currentSlide").src = slides[slideIndex];
}