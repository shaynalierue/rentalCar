// Get the elements
const sidebar = document.getElementById("sidebar");
const hamburger = document.getElementById("hamburger-button");
const closeIcon = document.getElementById("close-button");

// Toggle Sidebar
hamburger.addEventListener("click", () => {
    sidebar.classList.toggle("show");
});

// Hide Sidebar
closeIcon.addEventListener("click", () => {
    sidebar.classList.remove("show");
});


