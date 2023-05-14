let dataTable;


$(document).ready(function () {
  loadDataTable();
})

function loadDataTable() {
  dataTable = $('#deliveryMethodData').DataTable({
    ajax: {
      url: '/Admin/DeliveryMethod/GetAll'
    },
    responsive: true,
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "carrier", "width": "15%" },
      { "data": "deliveryTime", "width": "10%" },
      { "data": "paymentMethod.name", "width": "10%" },
      {
        "data": "cost", "width": "10%", "render":
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
              <a href="/Admin/DeliveryMethod/Upsert?id=${data}" class="btn btn-primary w-100 px-1">
                <i class="bi bi-pencil-square"></i><span class="d-none d-md-block">Edit</span>
              </a>
            </div>
            <div class="col-6 ps-1">
              <a onClick="Delete('/Admin/DeliveryMethod/Delete?id=${data}')" class="btn btn-danger w-100 px-1">
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