var Datatable

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() { 
    Datatable = $('#myTable').DataTable({

        "ajax": {
            "url" : "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "Width": "15%" },
            { "data": "streetAddress", "Width": "15%" },
            { "data": "city", "Width": "15%" },
            { "data": "state", "Width": "15%" },
            { "data": "phoneNumber", "Width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                         <a href="/Admin/Company/Upset?id=${data}" class="btn btn-primary mx-1">
                                <i class="bi bi-pencil-square"></i>Edit
                         </a>
                         <a  onClick=Delete('/Admin/Company/Delete/${data}')
                            class="btn btn-danger mx-1">
                             <i class="bi bi-trash"></i>Delete
                         </a>
                         </div>
                           `
                },
                "Width": "15%"
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
                url:url,
                type : 'DELETE',
                success: function (data) {
                    if (data.success) {
                        Datatable.ajax.reload();
                        toster.success(data.Message)
                    }
                    else {
                        toster.error(data.Message)
                    }
                }
            })
        }
    })

}