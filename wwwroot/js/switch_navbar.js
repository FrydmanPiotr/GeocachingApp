document.addEventListener("DOMContentLoaded", function () {
    const links = document.querySelectorAll('.nav-link');

    links.forEach(link => {
        link.addEventListener('click', function () {
            links.forEach(l => l.classList.remove('active'));
            link.classList.add('active');
        });
    });

    const currentPath = window.location.pathname.split('/').pop();
    const currentLink = [...links].find(link => link.href.includes(currentPath));
    if (currentLink) {
        currentLink.classList.add('active');
    }
});

