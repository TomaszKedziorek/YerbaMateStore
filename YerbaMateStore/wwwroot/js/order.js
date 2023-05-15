let dataTable;


$(document).ready(function () {
  FilterButtonsActions();
})

function FilterButtonsActions() {
  var url = window.location.search;
  if (url.includes("inprocess")) {
    loadDataTable("inprocess");
  }
  else if (url.includes("completed")) {
    loadDataTable("completed");
  }
  else if (url.includes("pending")) {
    loadDataTable("pending");
  }
  else if (url.includes("approved")) {
    loadDataTable("approved");
  }
  else {
    loadDataTable("all");
  }
}

function loadDataTable(status) {
  dataTable = $('#orderData').DataTable({
    ajax: {
      url: '/Admin/Order/GetAll?status=' + status,
    },
    responsive: true,
    pageLength: 50,
    columns: [
      { "data": "id", "width": "5%" },
      { "data": "name", "width": "10%" },
      { "data": "phoneNumber", "width": "10%" },
      { "data": "applicationUser.email", "width": "15%" },
      { "data": "orderStatus", "width": "10%" },
      { "data": "paymentStatus", "width": "10%" },
      { "data": "deliveryMethod.carrier", "width": "8%" },
      {
        "data": "null", "width": "10%", "render": function (data, type, row) {
          var number = currency.format(row.orderTotal);

          if (row.paymentStatus == 'Approved') {
            return '<span class="text-success fw-bold">' + number + '</span>';
          } else if (row.paymentStatus.includes('Approved')) {
            return '<span class="text-primary fw-bold">' + number + '</span>';
          } else {
            return '<span class="text-danger fw-bold">' + number + '</span>';
          }
        },
      },
      {
        "data": "id",
        "width": "20%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <div class="col-6 pe-1">
              <a href="/Admin/Order/Details?orderId=${data}" class="btn btn-primary w-100 ">
                <i class="bi bi-pencil-square"></i>&nbsp; Details</a>
            </div>
          </div>
        `
        }
      }
    ],
    rowCallback: function (row, data, index) {
      console.log(row.orderStatus);
      if (data.orderStatus === 'Pending') {
        $('td', row).addClass("text-secondary");
      }
      if (data.orderStatus === 'Processing' || data.orderStatus === 'Approved') {
        $('td', row).addClass("text-dark fw-bolder");
      }
      if (data.orderStatus === 'Shipped') {
        $('td', row).addClass("text-success fw-bold");
      }
      if (data.orderStatus === 'Cancelled' || data.PaymentStatusRejected === 'Rejected') {
        $('td', row).addClass("text-danger fw-bolder");
      }
      if (data.orderStatus === 'Refunded') {
        $('td', row).addClass("text-primary");
      }
    }
  });
}