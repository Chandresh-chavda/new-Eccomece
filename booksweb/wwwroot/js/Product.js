var Datatable

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() { 
    Datatable = $('#myTable').DataTable({

        "ajax": {
            "url" : "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "Width": "15%" },
            { "data": "isbn", "Width": "15%" },
            { "data": "price", "Width": "15%" },
            { "data": "author", "Width": "15%" },
            { "data": "category.name", "Width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-75 btn-group" role="group">
                         <a href="/Admin/Product/Upset?id=${data}" class="btn btn-primary mx-1">
                                <i class="bi bi-pencil-square"></i>Edit
                         </a>
                         <a  onClick=Delete('/Admin/Product/Delete/${data}')
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
                        toster.success(data.message)
                    }
                    else {
                        toster.error(data.message)
                    }
                }
            })
        }
    })

}