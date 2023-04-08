let dataTable;

$(document).ready(function () {
  loadDataTable();
})

function loadDataTable() {
  dataTable = $('#yerbaMateProductData').DataTable({
    ajax: {
      url: '/Admin/YerbaMate/GetAll'
    },
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "15%" },
      { "data": "brand", "width": "15%" },
      { "data": "weight", "width": "10%" },
      { "data": "price", "width": "10%" },
      {
        "data": "id",
        "width": "15%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <div class="col-6 pe-1">
              <a href="/Admin/YerbaMate/Upsert?id=${data}" class="btn btn-primary w-100 ">
                <i class="bi bi-pencil-square"></i>&nbsp; Edit</a>
            </div>
            <div class="col-6 ps-1">
              <a class="btn btn-danger w-100" onClick="Delete('/Admin/YerbaMate/Delete/${data}')">
                <i class="bi bi-x-circle"></i>&nbsp; Delete</a>
            </div>
          </div>
        `
        }
      }
    ]
  });
}

function Delete(url) {
  Swal.fire({
    title: 'Are you sure?',
    text: "You won't be able to revert this!",
    icon: 'warning',
    iconColor: '#e74c3c',
    showCancelButton: true,
    confirmButtonColor: "#375a7f",
    cancelButtonColor: '#e74c3c',
    confirmButtonText: 'Yes, delete it!'
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
          if (data.success) {
            dataTable.ajax.reload();
            toastr.success(data.message);
          }
          else {
            toastr.error(data.message);
          }
        }
      })
    }
  })
}