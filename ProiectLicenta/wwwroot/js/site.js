// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
    function downloadTableAsHtml() {
        var table = document.getElementById("registruTable").outerHTML;
        var titlu = document.querySelector('h1');
        var raport = document.querySelector('h4');
    var htmlContent = `
    <!DOCTYPE html>
    <html>
        <head>
            <meta charset="UTF-8">
                <title>${raport.textContent}-${titlu.textContent}</title>
                <style>
                    .table {border - collapse: collapse; width: 100%; }
                    .table, .table th, .table td {border: 1px solid black; }
                    .table th, .table td {padding: 8px; text-align: left; }
                    .table-striped tbody tr:nth-of-type(odd) {background - color: #f9f9f9; }
                    .table-bordered {border: 1px solid #dee2e6; }
                </style>
        </head>
        <body>
            <h1>${raport.textContent}-${titlu.textContent}</h1>
            ${table}
        </body>
    </html>`;
    var blob = new Blob([htmlContent], {type: "text/html" });
    var url = URL.createObjectURL(blob);0
    var a = document.createElement("a");
    a.href = url;
    a.download = "Descarcare.html";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
        }
