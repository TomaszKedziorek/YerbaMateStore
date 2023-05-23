let usersData;


$(document).ready(function () {
  FilterButtonsActions();
})

function FilterButtonsActions() {
  var url = window.location.search;
  if (url.includes("admins")) {
    loadDataTable("admins");
  }
  else if (url.includes("employees")) {
    loadDataTable("employees");
  }
  else if (url.includes("individuals")) {
    loadDataTable("individuals");
  }
  else if (url.includes("suspended")) {
    loadDataTable("suspended");
  }
  else if (url.includes("inactive")) {
    loadDataTable("inactive");
  }
  else {
    loadDataTable("all");
  }
}

function loadDataTable(status) {
  dataTable = $('#usersData').DataTable({
    ajax: {
      url: '/Admin/UsersManager/GetAll?status=' + status,
    },
    responsive: true,
    pageLength: 50,
    columns: [
      { "data": "role", "width": "5%" },
      { "data": "name", "width": "10%" },
      { "data": "email", "width": "15%" },
      {
        "data": "null", "width": "10%", "render": function (data, type, row) {
          return `
          <div class="form-check">
              <input type="checkbox" id="${"confirmEmailCheckbox" + row.id}" ${row.emailConfirmed ? "checked" : ""} 
              onClick="confirmEmail('${row.id}')" class="form-check-input">
              <label for="${"confirmEmailCheckbox" + row.id}" class="form-check-label">${row.emailConfirmed}</label>
          </div>
        `
        }
      },
      {
        "data": "null", "width": "8%", "render": function (data, type, row) {
          return `
          <div class="form-check">
              <input type="checkbox" id="${"suspendCheckbox" + row.id}" ${row.accountSuspended ? "checked" : ""} 
              onClick="suspendForever('${row.id}')" class="form-check-input" style="{width: 2rem; height: 30px;}">
              <label for="${"suspendCheckbox" + row.id}" class="form-check-label">${row.accountSuspended}</label>
          </div>
        `
        }
      },
      {
        "data": "null", "width": "8%", "render": function name(data, type, row) {
          return `
          <span>${new Date(row.lockoutEnd).toLocaleString("Locales")}</span>
          <select class="form-select" id="${"suspendSelect" + row.id}" onChange="suspendForTime('${row.id}')">
            <option selected>-Select suspension time-</option>
            <option value="${1 / 60}">1 miunte</option>
            <option value="${1}">1 hour</option>
            <option value="${3}">3 hours</option>
            <option value="${24}">1 day</option>
            <option value="${24 * 3}">3 days</option>
            <option value="${24 * 7}">7 days</option>
            <option value="${24 * 30}">30 days</option>
            <option value="${24 * 365}">1 year</option> 
          </select>
          `
        }
      },
      {
        "data": "id",
        "width": "8%",
        "render": function (data) {
          return `
          <div class="row justify-content-center">
            <a onClick="Delete('/Admin/UsersManager/Delete?userId=${data}')" class="btn btn-danger">
              <i class="bi bi-x-circle"></i>&nbsp; Delete
            </a>
          </div>
        `
        }
      }
    ]
  }
  );
}

function confirmEmail(userId) {
  let confirm = document.getElementById("confirmEmailCheckbox" + userId).checked;
  $.ajax({
    url: `/Admin/UsersManager/ConfirmUserEmail?userId=${userId}&confirm=${confirm}`,
    type: 'POST',
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

function suspendForever(userId) {
  let suspend = document.getElementById("suspendCheckbox" + userId).checked;
  $.ajax({
    url: `/Admin/UsersManager/SuspendUserForever?userId=${userId}&suspend=${suspend}`,
    type: 'POST',
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

function suspendForTime(userId) {
  let selectedOption = document.getElementById("suspendSelect" + userId).value;
  $.ajax({
    url: `/Admin/UsersManager/SuspendUserForTime?userId=${userId}&hours=${selectedOption}`,
    type: 'POST',
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
