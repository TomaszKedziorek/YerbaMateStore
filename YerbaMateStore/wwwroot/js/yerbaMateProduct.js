let dataTable;

$(document).ready(function () {
  loadDataTable();
})

function loadDataTable() {
  dataTable = $('#yerbaMateProductData').DataTable({
    ajax: {
      url: '/Admin/YerbaMate/GetAll'
    },
    responsive: true,
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "15%" },
      { "data": "brand", "width": "10%" },
      { "data": "weight", "width": "5%" },
      {
        "data": "price", "width": "5%", "render":
          function (data) {
            return data + " zł";
          }
      },
      {
        "data": "id",
        "width": "15%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <div class="col-6 pe-1">
              <a href="/Admin/YerbaMate/Upsert?id=${data}" class="btn btn-primary w-100 px-1">
                <i class="bi bi-pencil-square"></i><span class="d-none d-md-block">Edit</span>
              </a>
            </div>
            <div class="col-6 ps-1">
              <a onClick="Delete('/Admin/YerbaMate/Delete/${data}')" class="btn btn-danger w-100 px-1">
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