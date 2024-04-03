document.addEventListener("DOMContentLoaded", function () {
    const sidebarCollapse = document.getElementById('sidebarCollapse');
    const sidebar = document.getElementById('sidebar');
    const overlay = document.querySelector('.overlay'); // Verwijzing naar bestaande overlay

    sidebarCollapse.addEventListener('click', function () {
        sidebar.classList.toggle('show'); // Verander 'active' naar 'show'
        overlay.classList.toggle('active');
    });

    overlay.addEventListener('click', function () {
        sidebar.classList.remove('show'); // Verwijder 'show'
        overlay.classList.remove('active');
    });
});
