let dataTable;


$(document).ready(function () {
  loadDataTable();
})

function loadDataTable() {
  dataTable = $('#bombillaProductData').DataTable({
    ajax: {
      url: '/Admin/Bombilla/GetAll'
    },
    responsive: true,
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "15%" },
      { "data": "material", "width": "10%" },
      { "data": "length", "width": "10%" },
      { "data": "isUnscrewed", "width": "5%" },
      {
        "data": "price", "width": "10%", "render":
          function (data) {
            return currency.format(data);
          }
      },
      {
        "data": "id",
        "width": "15%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <div class="col-6 pe-1">
              <a href="/Admin/Bombilla/Upsert?id=${data}" class="btn btn-primary w-100 px-1">
                <i class="bi bi-pencil-square"></i><span class="d-none d-md-block">Edit</span>
              </a>
            </div>
            <div class="col-6 ps-1">
              <a onClick="Delete('/Admin/Bombilla/Delete/${data}')" class="btn btn-danger w-100 px-1">
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