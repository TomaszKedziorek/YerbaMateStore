let dataTable;

$(document).ready(function () {
  loadDataTable();
})

function loadDataTable() {
  dataTable = $('#paymentMethodData').DataTable({
    ajax: {
      url: '/Admin/PaymentMethod/GetAll'
    },
    responsive: true,
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "40%" },
      { "data": "isTransfer", "width": "5%" },
      {
        "data": "id",
        "width": "15%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <div class="col-6 pe-1">
              <a href="/Admin/PaymentMethod/Upsert?id=${data}" class="btn btn-primary w-100 px-1">
                <i class="bi bi-pencil-square"></i><span class="d-none d-md-block">Edit</span>
              </a>
            </div>
            <div class="col-6 ps-1">
              <a onClick="Delete('/Admin/PaymentMethod/Delete?id=${data}')" class="btn btn-danger w-100 px-1">
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