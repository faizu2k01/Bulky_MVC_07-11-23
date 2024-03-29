﻿var dataTable;


$(document).ready(function () {
    loadTable();
});


function loadTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/Company/getall' },
        "columns": [
            { data: 'name'},
            { data: 'phoneNumber'},
            { data: 'streetAddress' },
            { data: 'city' },
            { data: 'state' },
            {data:'postalCode'},
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group" >
                        <a href="/Admin/Company/upsert?id=${data}" class="btn btn-primary mx-4"> <i class="bi bi-pencil"></i>Edit</a>
                        <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-danger mx-4"> <i class="bi bi-trash2"></i>Delete</a>
                    </div>`
                },
                
            }
           
            
           
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}