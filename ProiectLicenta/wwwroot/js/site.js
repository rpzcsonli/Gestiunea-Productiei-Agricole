// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    document.addEventListener("DOMContentLoaded", function () {
            if (localStorage.getItem('scrollPosition') !== null) {
        window.scrollTo(0, localStorage.getItem('scrollPosition'));
    localStorage.removeItem('scrollPosition');
            }
        });

    window.addEventListener("beforeunload", function () {
        localStorage.setItem('scrollPosition', window.scrollY);
        });
</script>