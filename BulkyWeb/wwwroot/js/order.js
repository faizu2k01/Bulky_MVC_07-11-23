var dataTable;


$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadTable("inporcess");
    }
    else if (url.includes("pending")) {
        loadTable("pending");
    }
    else if (url.includes("completed")) {
        loadTable("completed");
    }
    else if (url.includes("approved")) {
        loadTable("approved");
    }
    else{
        loadTable("all");
    }
});


function loadTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/Order/Getall?status=' + status },
        "columns": [
            { data: 'id'},
            { data: 'name'},
            { data: 'phoneNumber' },
            { data: 'applicationUser.email' },
            { data: 'orderStatus' },
            {data:'orderTotal'},
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group" >
                        <a href="/Admin/Order/details?id=${data}" class="btn btn-primary mx-4"> <i class="bi bi-pencil"></i></a>
                    </div>`
                },
                
            }
           
            
           
        ]
    });
}


