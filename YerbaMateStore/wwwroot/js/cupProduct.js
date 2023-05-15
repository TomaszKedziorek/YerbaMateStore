let dataTable;


$(document).ready(function () {
  loadDataTable();
})

function loadDataTable() {
  dataTable = $('#cupProductData').DataTable({
    ajax: {
      url: '/Admin/Cup/GetAll'
    },
    responsive: true,
    pageLength: 50,
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "15%" },
      { "data": "material", "width": "10%" },
      { "data": "volume", "width": "10%" },
      {
        "data": "null", "width": "5%", "render":
          function (data, type, row) {
            if (row.discountPrice == null) {
              return currency.format(row.price);
            } else {
              return currency.format(row.discountPrice);
            }
          }
      },
      {
        "data": "id",
        "width": "15%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <div class="col-6 pe-1">
              <a href="/Admin/Cup/Upsert?id=${data}" class="btn btn-primary w-100 px-1">
                <i class="bi bi-pencil-square"></i><span class="d-none d-md-block">Edit</span>
              </a>
            </div>
            <div class="col-6 ps-1">
              <a onClick="Delete('/Admin/Cup/Delete/${data}')" class="btn btn-danger w-100 px-1">
                <i class="bi bi-x-circle"></i><span class="d-none d-md-block">Delete</span>
              </a>
            </div>
          </div>
        `
        }
      }
    ]
  });
}